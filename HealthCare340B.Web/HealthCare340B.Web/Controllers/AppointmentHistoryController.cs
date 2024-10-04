using System.Drawing;
using System.Net;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using HealthCare340B.Web.AddOns;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;
using SelectPdf;

namespace HealthCare340B.Web.Controllers
{
    public class AppointmentHistoryController : Controller
    {
        private readonly AppointmentHistoryModel _appointmentHistoryModel;
        private readonly string _imageFolder;

        private string? _userId;
        private string? _roleCode;

        private readonly int _pageSize;

        public AppointmentHistoryController(IConfiguration configuration)
        {
            _appointmentHistoryModel = new AppointmentHistoryModel(configuration);
            _imageFolder = configuration["ImageFolder"];

            _pageSize = int.Parse(configuration["PageSize"]);
        }

        private bool isInSession()
        {
            _userId = HttpContext.Session.GetString("userId") ?? null;

            return _userId != null;
        }

        private bool isInRole()
        {
            _roleCode = HttpContext.Session.GetString("userRoleCode") ?? null;

            return _roleCode == "ROLE_PASIEN";
        }

        public async Task<IActionResult> Index(
            string? filter,
            int? pageNumber,
            int? currPageSize,
            string? orderBy = "appointment_date",
            string? orderDirection = "asc"
        )
        {
            if (!isInSession())
            {
                HttpContext.Session.SetString("errMsg", "Please login first!");
                return RedirectToAction("Index", "Auth");
            }
            if (!isInRole())
            {
                HttpContext.Session.SetString("errMsg", "You are not authorized!");
                return RedirectToAction("Index", "Home");
            }

            List<VMTAppointmentDone>? data = new List<VMTAppointmentDone>();

            try
            {
                data = await _appointmentHistoryModel.GetAppointmentDoneByFilter(
                    string.IsNullOrEmpty(filter) ? "" : filter,
                    (long)HttpContext.Session.GetInt32("userBiodataId")!
                );
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            //Process data Order
            if (!string.IsNullOrEmpty(orderBy))
            {
                if (orderBy == "appointment_date")
                {
                    data =
                        orderDirection == "asc"
                            ? data?.OrderBy(d => d.AppointmentDate).ToList()
                            : data?.OrderByDescending(d => d.AppointmentDate).ToList();
                }
                else if (orderBy == "name")
                {
                    data =
                        orderDirection == "asc"
                            ? data?.OrderBy(d => d.CustomerFullname).ToList()
                            : data?.OrderByDescending(d => d.CustomerFullname).ToList();
                }
                else if (orderBy == "created_on")
                {
                    data =
                        orderDirection == "asc"
                            ? data?.OrderBy(d => d.CreatedOn).ToList()
                            : data?.OrderByDescending(d => d.CreatedOn).ToList();
                }
            }

            ViewBag.Title = "Riwayat Kedatangan";
            ViewBag.imgFolder = _imageFolder;
            ViewBag.Filter = filter;
            ViewBag.PageSize = currPageSize ?? _pageSize;
            ViewBag.OrderBy = orderBy;
            ViewBag.OrderDirection = orderDirection;

            ViewBag.Breadcrumb = new List<BreadcrumbItem>
            {
                new BreadcrumbItem
                {
                    Name = "Beranda",
                    Controller = "Home",
                    Action = "Index",
                },
                new BreadcrumbItem
                {
                    Name = "Profile",
                    Controller = "Profile",
                    Action = "Index",
                },
                new BreadcrumbItem { Name = "Riwayat Kedatangan", IsActive = true },
            };

            return View(
                Pagination<VMTAppointmentDone>.Create(
                    data ?? new List<VMTAppointmentDone>(),
                    pageNumber ?? 1,
                    currPageSize ?? _pageSize
                )
            );
        }

        public IActionResult Print(long appointmentId)
        {
            if (!isInSession())
            {
                HttpContext.Session.SetString("errMsg", "Please login first!");
                return RedirectToAction("Index", "Auth");
            }
            if (!isInRole())
            {
                HttpContext.Session.SetString("errMsg", "You are not authorized!");
                return RedirectToAction("Index", "Home");
            }

            long data = appointmentId;

            ViewBag.Title = "Cetak Resep";

            return View(data);
        }

        public async Task<IActionResult> PrescriptionPdf(long appointmentId)
        {
            VMTAppointmentDone? data = new VMTAppointmentDone();

            try
            {
                data = await _appointmentHistoryModel.GetAppointmentDoneByAppointmentId(
                    appointmentId
                );

                // Set up the PDF converter with Blink rendering engine
                HtmlToPdf converter = new HtmlToPdf
                {
                    Options =
                    {
                        RenderingEngine = RenderingEngine.Blink,
                        PdfPageSize = PdfPageSize.Custom,
                        PdfPageOrientation = PdfPageOrientation.Portrait,
                        WebPageWidth = 302, // 80mm in pixels
                        AutoFitWidth = HtmlToPdfPageFitMode.ShrinkOnly,
                    },
                };

                // Set the custom page size dynamically based on prescription count
                float pageWidth = 80 * 2.83465f; // 80mm converted to points
                float baseHeight = 77 * 2.83465f; // Base height for 1 prescription (77mm)
                float additionalHeight = (96 - 77) * 2.83465f; // Additional height per prescription

                int numPrescriptions = data.Prescriptions?.Count ?? 0;
                float pageHeight =
                    baseHeight + (additionalHeight * Math.Max(0, numPrescriptions - 1));

                converter.Options.PdfPageCustomSize = new SizeF(pageWidth, pageHeight);

                // Render the HTML content
                string htmlContent = await Render.ViewToStringAsync(
                    this,
                    "PrescriptionPdf",
                    data,
                    true
                );

                // Convert the HTML content to PDF
                PdfDocument doc = converter.ConvertHtmlString(htmlContent);

                // Save the PDF to a byte array
                byte[] pdf = doc.Save();

                // Close the PDF document
                doc.Close();

                // Return the PDF file as a download
                return File(pdf, "application/pdf", "ResepDigital.pdf");
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "An error occurred while generating the PDF."
                );
            }
        }

        [HttpPost]
        public async Task<VMResponse<List<VMTPrescription>>?> UpdatePrintAttemptAsync(long appointmentId)
        {
            VMResponse<List<VMTPrescription>>? response = null;

            try
            {
                // Fetch Prescription
                List<VMTPrescription> data = await _appointmentHistoryModel.GetPrescriptionByAppointmentId(appointmentId);

                // Update print attempt for each prescription
                if (data != null)
                {
                    long userId = long.Parse(HttpContext.Session.GetString("userId")!);
                    foreach (VMTPrescription prescription in data)
                    {
                        if (DateTime.Now.Date < prescription.CreatedOn.Date.AddDays(2))
                        {
                            if (prescription.PrintAttempt == null || prescription.PrintAttempt < 2)
                            {
                                prescription.ModifiedBy = userId;
                            }
                            else
                            {
                                response = new VMResponse<List<VMTPrescription>>
                                {
                                    StatusCode = HttpStatusCode.BadRequest,
                                    Message = "Prescription print attempt has reached the limit.",
                                };

                                throw new Exception(response.Message);
                            }
                        }
                        else
                        {
                            response = new VMResponse<List<VMTPrescription>>
                            {
                                StatusCode = HttpStatusCode.BadRequest,
                                Message = "Prescription is no longer valid after 2 days from creation.",
                            };

                            throw new Exception(response.Message);
                        }
                    }
                }
                else
                {
                    throw new Exception("No prescription data found");
                }

                response = await _appointmentHistoryModel.UpdatePrintAttemptAsync(data);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    HttpContext.Session.SetString("successMsg", response.Message);
                }
                else
                {
                    HttpContext.Session.SetString("errMsg", response.Message);
                }
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            return response;
        }
    }
}

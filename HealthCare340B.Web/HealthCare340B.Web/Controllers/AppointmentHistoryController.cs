using HealthCare340B.ViewModel;
using HealthCare340B.Web.AddOns;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;
using SelectPdf;
using System.Drawing;

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
            string? orderBy,
            string? orderDirection
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
                data = await _appointmentHistoryModel.GetByFilter(
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
            ViewBag.OrderBy = orderBy ?? "appointment_date";
            ViewBag.OrderDirection = orderDirection ?? "asc";

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

            VMTAppointmentDone? data = new VMTAppointmentDone();

            try
            {
                data = await _appointmentHistoryModel.GetByAppointmentId(appointmentId);
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            //return View(data);

            // Set up the PDF converter with Blink rendering engine
            HtmlToPdf converter = new HtmlToPdf();
            converter.Options.RenderingEngine = RenderingEngine.Blink;

            // Set converter options
            converter.Options.PdfPageSize = PdfPageSize.Custom;

            // Set lebar tetap (80mm), dan hitung tinggi berdasarkan jumlah obat
            float pageWidth = 80 * 2.83465f;
            float baseHeight = 77 * 2.83465f; // Tinggi awal untuk 1 obat
            float additionalHeight = (96 - 77) * 2.83465f; // Tambahan tinggi per obat

            // Jumlah obat
            int numObat = data.Prescriptions!.Count;

            // Hitung tinggi halaman dinamis
            float pageHeight = baseHeight + (additionalHeight * (numObat - 1));

            // Set ukuran halaman dinamis
            converter.Options.PdfPageCustomSize = new SizeF(pageWidth, pageHeight);

            // Tetap menggunakan orientasi portrait
            converter.Options.PdfPageOrientation = PdfPageOrientation.Portrait;

            // Sesuaikan lebar halaman web agar sesuai dengan lebar 80mm (302px)
            converter.Options.WebPageWidth = 302;

            // Tinggi halaman web biarkan otomatis untuk menyesuaikan konten
            converter.Options.WebPageHeight = 0; // Biarkan SelectPdf menghitung tinggi konten
            converter.Options.WebPageFixedSize = false;

            // Atur supaya konten pas secara horizontal (mengecilkan jika perlu)
            converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.ShrinkOnly;

            // Tidak menyesuaikan tinggi konten secara vertikal
            converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.NoAdjustment;

            // Render the view as HTML string
            string htmlContent = await Render.ViewToStringAsync(this, "PrescriptionPdf", data, true);

            // Convert HTML string to PDF
            PdfDocument doc = converter.ConvertHtmlString(htmlContent);

            // Save PDF to a byte array
            byte[] pdf = doc.Save();

            // Close the PDF document
            doc.Close();

            // Return the PDF file to the browser
            return File(pdf, "application/pdf", "ResepDigital.pdf");
        }
    }
}

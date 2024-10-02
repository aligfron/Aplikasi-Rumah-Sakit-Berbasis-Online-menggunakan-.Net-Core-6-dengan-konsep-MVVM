using DinkToPdf;
using DinkToPdf.Contracts;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using HealthCare340B.Web.AddOns;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;

namespace HealthCare340B.Web.Controllers
{
    public class AppointmentHistoryController : Controller
    {
        private readonly AppointmentHistoryModel _appointmentHistoryModel;
        private readonly string _imageFolder;

        private string? _userId;
        private string? _roleCode;

        private readonly int _pageSize;

        private readonly IConverter _converter;
        private IWebHostEnvironment _webHostEnvironment;

        public AppointmentHistoryController(IConfiguration configuration, IConverter converter, IWebHostEnvironment environment)
        {
            _appointmentHistoryModel = new AppointmentHistoryModel(configuration);
            _imageFolder = configuration["ImageFolder"];

            _pageSize = int.Parse(configuration["PageSize"]);

            _converter = converter;
            _webHostEnvironment = environment;
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

            //// Render Partial View ke string
            //string htmlContent = await Render.ViewToStringAsync(this, "PrescriptionPdf", data, true);

            //// Konfigurasi untuk konversi ke PDF
            //var pdf = new HtmlToPdfDocument()
            //{
            //    GlobalSettings =
            //    {
            //        ColorMode = ColorMode.Color,
            //        Orientation = Orientation.Portrait,
            //        PaperSize = new PechkinPaperSize("80", "200"),
            //        Margins = new MarginSettings { Top = 10 },
            //    },
            //    Objects =
            //    {
            //        new ObjectSettings()
            //        {
            //            PagesCount = true,
            //            HtmlContent = htmlContent,
            //            WebSettings = { DefaultEncoding = "utf-8" }
            //        },
            //    },
            //};

            //var file = _converter.Convert(pdf);

            //return File(file, "application/pdf", "resep-digital.pdf");

            return View(data);
        }
    }
}

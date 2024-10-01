using HealthCare340B.ViewModel;
using HealthCare340B.Web.AddOns;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;

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
                    data = orderDirection == "asc"
                        ? data?.OrderBy(d => d.AppointmentDate).ToList()
                        : data?.OrderByDescending(d => d.AppointmentDate).ToList();
                }
                else if (orderBy == "name")
                {
                    data = orderDirection == "asc"
                        ? data?.OrderBy(d => d.CustomerFullname).ToList()
                        : data?.OrderByDescending(d => d.CustomerFullname).ToList();
                }
                else if (orderBy == "created_on")
                {
                    data = orderDirection == "asc"
                        ? data?.OrderBy(d => d.CreatedOn).ToList()
                        : data?.OrderByDescending(d => d.CreatedOn).ToList();
                }
            }

            ViewBag.Title = "Riwayat Kedatangan";
            ViewBag.Role = HttpContext.Session.GetString("userRoleCode");
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
    }
}

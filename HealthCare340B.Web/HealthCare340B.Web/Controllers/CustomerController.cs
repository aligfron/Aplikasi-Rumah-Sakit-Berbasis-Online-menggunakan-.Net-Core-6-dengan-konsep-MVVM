using HealthCare340B.Web.AddOns;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.Web.Controllers
{
    [Route("Profile/Pasien")]
    public class CustomerController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Daftar Pasien";
            ViewBag.Role = "ROLE_PASIEN";

            ViewBag.Breadcrumb = new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Name = "Beranda", Controller = "Home", Action = "Index" },
                new BreadcrumbItem { Name = "Profile", Controller = "Profile", Action = "Index" },
                new BreadcrumbItem { Name = "Pasien", IsActive = true }
            };

            return View();
        }
    }
}

using HealthCare340B.Web.AddOns;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.Web.Controllers
{
    [Route("Profil/Pasien")]
    public class PasienController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Title = "Daftar Pasien";
            ViewBag.Role = "ROLE_PASIEN";

            ViewBag.Breadcrumb = new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Name = "Beranda", Controller = "Home", Action = "Index" },
                new BreadcrumbItem { Name = "Profile", Controller = "Profil", Action = "Index" },
                new BreadcrumbItem { Name = "Pasien", IsActive = true }
            };

            return View();
        }
    }
}

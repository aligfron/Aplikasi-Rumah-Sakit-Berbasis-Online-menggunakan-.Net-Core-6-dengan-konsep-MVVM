using HealthCare340B.Web.AddOns;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.Web.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            // Mengirim role ke view
            ViewBag.Title = "Profil";

            ViewBag.Role = "ROLE_DOKTER";
            //ViewBag.Role = "ROLE_PASIEN";

            ViewBag.Breadcrumb = new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Name = "Beranda", Controller = "Home", Action = "Index" },
                new BreadcrumbItem { Name = "Profile", IsActive = true }
            };

            return View();
        }
    }
}

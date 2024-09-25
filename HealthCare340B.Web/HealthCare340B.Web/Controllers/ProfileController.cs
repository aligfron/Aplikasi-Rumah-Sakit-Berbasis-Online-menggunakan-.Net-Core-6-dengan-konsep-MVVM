using HealthCare340B.ViewModel;
using HealthCare340B.Web.AddOns;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly string imageFolder;
        private ProfileModel profile;
        public ProfileController(IConfiguration _config)
        {
            profile = new ProfileModel(_config);
            imageFolder = _config["ImageFolder"];
        }
        public async Task<IActionResult> Index()
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
            VMMDoctor? data = await profile.GetByIdProfilDokter(1);
            return View(data);
        }
    }
}

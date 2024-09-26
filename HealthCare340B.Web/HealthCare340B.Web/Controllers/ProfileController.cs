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
        public ProfileController(IConfiguration _config, IWebHostEnvironment _webHostEnv)
        {
            profile = new ProfileModel(_config, _webHostEnv);
            imageFolder = _config["ImageFolder"];
        }

        public async Task<IActionResult> Index()
        {
            // Mengirim role ke view
            ViewBag.Title = "Profil";
            ViewBag.imgFolder = imageFolder;

            //string Role = "ROLE_DOKTER";
            string role = "ROLE_DOKTER";

            ViewBag.Breadcrumb = new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Name = "Beranda", Controller = "Home", Action = "Index" },
                new BreadcrumbItem { Name = "Profile", IsActive = true }
            };

            switch (role)
            {
                case "ROLE_PASIEN":
                    ViewBag.Url = "/Profile/IndexPasienProfile";
                    break;

                case "ROLE_DOKTER":
                    ViewBag.Url = "/Profile/IndexDoctorProfile";
                    break;

                case "ROLE_ADMIN":
                    ViewBag.Url = "/Profile/IndexAdminProfile";
                    break;

                default:
                    ViewBag.Url = "#";
                    break;
            }

            return View();
        }

        public async Task<IActionResult> IndexDoctorProfile()
        {
            ViewBag.Title = "Profil";
            ViewBag.imgFolder = imageFolder;
            ViewBag.Role = "ROLE_DOKTER";

            ViewBag.Breadcrumb = new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Name = "Beranda", Controller = "Home", Action = "Index" },
                new BreadcrumbItem { Name = "Profile", IsActive = true }
            };

            VMMDoctor? data = await profile.GetByIdProfilDokter(1);
            return View(data);
        }

        public async Task<IActionResult> IndexPasienProfile()
        {
            ViewBag.Title = "Profil";
            ViewBag.imgFolder = imageFolder;
            ViewBag.Role = "ROLE_PASIEN";

            ViewBag.Breadcrumb = new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Name = "Beranda", Controller = "Home", Action = "Index" },
                new BreadcrumbItem { Name = "Profile", IsActive = true }
            };

            return View();
        }

        [HttpPost]
        public async Task<VMResponse<VMMBiodatum>> EditAsync(VMMBiodatum data)
        {
            return (await profile.UpdateAsync(data));
        }
        public async Task<IActionResult> DeleteAlamat()
        {
            ViewBag.Title = "Hapus Alamat";
            return View();
        }

        public async Task<IActionResult> CreateAlamat()
        {
            ViewBag.Title = "Tambah Alamat";
            return View();
        }

    }
}

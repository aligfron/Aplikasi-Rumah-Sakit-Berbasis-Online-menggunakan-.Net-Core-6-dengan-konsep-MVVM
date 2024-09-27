using HealthCare340B.ViewModel;
using HealthCare340B.Web.AddOns;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using Microsoft.AspNetCore.Http;

namespace HealthCare340B.Web.Controllers
{
    public class ProfileController : Controller
    {
        private readonly string imageFolder;
        private ProfileModel profile;
        private SpecializationModel specialization;
        private string? _userId;
        private string? _roleCode;

        public ProfileController(IConfiguration _config, IWebHostEnvironment _webHostEnv)
        {
            profile = new ProfileModel(_config, _webHostEnv);
            specialization = new SpecializationModel(_config);
            imageFolder = _config["ImageFolder"];
        }

        private bool isInSession()
        {
            _userId = HttpContext.Session.GetString("userId") ?? null;

            return _userId != null;
        }
        private bool isInRolePasien()
        {
            _roleCode = HttpContext.Session.GetString("userRoleCode") ?? null;

            return _roleCode == "ROLE_PASIEN";
        }
        private bool isInRoleDokter()
        {
            _roleCode = HttpContext.Session.GetString("userRoleCode") ?? null;

            return _roleCode == "ROLE_DOKTER";
        }

        public async Task<IActionResult> Index()
        {
            if (!isInSession())
            {
                HttpContext.Session.SetString("errMsg", "Please login first!");
            }

            ViewBag.Title = "Profil";
            ViewBag.imgFolder = imageFolder;

            string role = HttpContext.Session.GetString("userRoleCode")!;
            
            ViewBag.Breadcrumb = new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Name = "Beranda", Controller = "Home", Action = "Index" },
                new BreadcrumbItem { Name = "Profile", IsActive = true }
            };

            switch (role)
            {
                case "ROLE_PASIEN":
                    return RedirectToAction("IndexCustomerProfile");

                case "ROLE_DOKTER":
                    return RedirectToAction("IndexDoctorProfile");

                case "ROLE_ADMIN":
                    break;
            }

            return View();
        }

        public async Task<IActionResult> IndexDoctorProfile()
        {
            if (!isInSession())
            {
                HttpContext.Session.SetString("errMsg", "Please login first!");
            }
            if (!isInRoleDokter())
            {
                HttpContext.Session.SetString("errMsg", "You are not authorized!");
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Title = "Profil";
            ViewBag.imgFolder = imageFolder;
            ViewBag.Role = HttpContext.Session.GetString("userRoleCode")!;

            ViewBag.Breadcrumb = new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Name = "Beranda", Controller = "Home", Action = "Index" },
                new BreadcrumbItem { Name = "Profile", IsActive = true }
            };
            int bioId = HttpContext.Session.GetInt32("userBiodataId") ?? 0;
            VMMDoctor? GetDoctorByBiodataId = await profile.GetDoctorByBiodataId(bioId);
            VMMDoctor? data = await profile.GetByIdProfilDokter(GetDoctorByBiodataId!.Id);
            return View(data);
        }

        public async Task<IActionResult> IndexCustomerProfile()
        {
            if (!isInSession())
            {
                HttpContext.Session.SetString("errMsg", "Please login first!");
            }
            if (!isInRolePasien())
            {
                HttpContext.Session.SetString("errMsg", "You are not authorized!");
                return RedirectToAction("Index", "Home");
            }
            ViewBag.Title = "Profil";
            ViewBag.imgFolder = imageFolder;
            ViewBag.Role = HttpContext.Session.GetString("userRoleCode")!;

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
            VMResponse<VMMBiodatum>? response = null;

            try
            {
                data.Id = (long)HttpContext.Session.GetInt32("userBiodataId")!;
                data.ModifiedBy = long.Parse(HttpContext.Session.GetString("userId")!);

                response = await profile.UpdateAsync(data);

                HttpContext.Session.SetString("userImagePath", response.Data.ImagePath);
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

            //return (await profile.UpdateAsync(data));
        }
        public async Task<IActionResult> DeleteAlamat()
        {
            ViewBag.Title = "Hapus Alamat";
            return View();
        }

        public async Task<IActionResult> CreateAlamat()
        {

            ViewBag.Title = "Tambah Alamat";
            var location = await profile.GetAllLocation();
            ViewBag.Location = location ?? new List<VMMLocation>();

            return View();
        }
        [HttpPost]
        public async Task<VMResponse<VMMBiodataAddress>?> CreateAsync(VMMBiodataAddress data)
           => await profile.CreateAsync(data);

        public async Task<IActionResult> EditAlamat(int id)
        {
            VMMBiodataAddress? data = await profile.GetByIdAlamat(id);

            ViewBag.Location = await profile.GetAllLocation() ?? new List<VMMLocation>();
            ViewBag.Title = "Edit Alamat";

            return View(data);
        }

        public async Task<IActionResult> TabAlamat()
        {
            ViewBag.Title = "Daftar Alamat";

            var bioAddress = new List<VMMBiodataAddress>();
            try
            {
                bioAddress = await profile.GetAllBioAddress();
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            if (bioAddress == null || !bioAddress.Any())
            {
                ViewBag.Message = "Tidak ada Biodata Address ditemukan berdasarkan pencarian Anda.";
                bioAddress = new List<VMMBiodataAddress>();
            }

            return View(bioAddress);
        }


        //ali
        public async Task<IActionResult> CreateSpeDoctor(int id)
        {
            ViewBag.Specialization = await specialization.getByFilter("");
            ViewBag.Title = "Tambah Spesialisasi Dokter";
            ViewBag.Doctor = await profile.GetByIdProfilDokter(id);
            return View();
        }
        [HttpPost]
        public async Task<VMResponse<VMTCurrentDoctorSpecialization>?> CreateSpecializationDoctorAsync(VMTCurrentDoctorSpecialization data)
        {
            VMResponse<VMTCurrentDoctorSpecialization>? response = null;

            try
            {
                data.CreatedBy = long.Parse(HttpContext.Session.GetString("userId")!);
                response = await profile.CreateSpecializationDoctorAsync(data);
                if (response.StatusCode == HttpStatusCode.Created)
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
            return (response);
        }
        public async Task<IActionResult> EditSpecializationDoctor(int id)
        {
            VMTCurrentDoctorSpecialization? data = await profile.GetByIdSpecializationDoctor(id);
            ViewBag.Specialization = await specialization.getByFilter("");
            ViewBag.Title = "Edit Spesialisasi Dokter";
            return View(data);
        }
        [HttpPost]
        public async Task<VMResponse<VMTCurrentDoctorSpecialization>?> EditSpecializationDoctorAsync(VMTCurrentDoctorSpecialization data)
        {
            VMResponse<VMTCurrentDoctorSpecialization>? response = null;

            try
            {
                data.ModifiedBy = long.Parse(HttpContext.Session.GetString("userId")!);
                response = await profile.EditSpecializationDoctorAsync(data);
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
            return (response);
        }
    }
}

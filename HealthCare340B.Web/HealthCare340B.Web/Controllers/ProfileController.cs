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
        private readonly int pageSize;

        public ProfileController(IConfiguration _config, IWebHostEnvironment _webHostEnv)
        {
            profile = new ProfileModel(_config, _webHostEnv);
            specialization = new SpecializationModel(_config);
            imageFolder = _config["ImageFolder"];
            pageSize = int.Parse(_config["PageSize"]);
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
            ViewBag.Specialization = await specialization.getByFilter("");
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
        [HttpGet("/Profile/DeleteAlamat/{id}")]
        public async Task<IActionResult> DeleteAlamat(int id)
        {
            ViewBag.Title = "Hapus Alamat";
            return View(id);
        }
        [HttpPost]
        public async Task<VMResponse<VMMBiodataAddress>?> DeleteAsync(int id, int userId)
            => (await profile.DeleteAsync(id, userId));


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

        [HttpGet("/Profile/EditAlamat/{id}")]
        public async Task<IActionResult> EditAlamat(int id)
        {
            VMMBiodataAddress? data = await profile.GetByIdAlamat(id);

            ViewBag.Location = await profile.GetAllLocation() ?? new List<VMMLocation>();
            ViewBag.Title = "Edit Alamat";

            return View(data);
        }
        [HttpPost]
        public async Task<VMResponse<VMMBiodataAddress>?> EditAlamatAsync(VMMBiodataAddress data)
            => (await profile.UpdateAsync(data));

        [HttpGet("/Profile/TabAlamat")]
        public async Task<IActionResult> TabAlamat(string? filter, int? pageNumber, int? currentPageSize, string? orderBy)
        {
            ViewBag.Title = "Daftar Alamat";

            List<VMMBiodataAddress>? data = new List<VMMBiodataAddress>();
            try
            {
                data = await profile.GetByFilter(filter);
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            switch (orderBy)
            {
                case "label_desc":
                    data = data?.OrderByDescending(p => p.Label).ToList();
                    break;
                case "label":
                    data = data?.OrderBy(p => p.Label).ToList();
                    break;
                case "recipient_desc":
                    data = data?.OrderByDescending(p => p.Recipient).ToList();
                    break;
                case "recipient":
                    data = data?.OrderBy(p => p.Recipient).ToList();
                    break;
                case "id_desc":
                    data = data?.OrderByDescending(p => p.Id).ToList();
                    break;
                default:
                    data = data?.OrderBy(p => p.Id).ToList();
                    break;
            }
            if (data == null || !data.Any())
            {
                ViewBag.Message = "Tidak ada Biodata Address ditemukan berdasarkan pencarian Anda.";
                data = new List<VMMBiodataAddress>();
            }

            ViewBag.BioAddressId = string.IsNullOrEmpty(orderBy) ? "id_desc" : "";
            ViewBag.OrderRecipient = (orderBy == "recipient") ? "recipient_desc" : "recipient";
            ViewBag.OrderLabel = (orderBy == "label") ? "label_desc" : "label";
            ViewBag.PageSize = currentPageSize ?? 10;
            ViewBag.OrderBy = orderBy;
            ViewBag.Filter = filter;

            return View(Pagination<VMMBiodataAddress>.Create(data ?? new List<VMMBiodataAddress>(), pageNumber ?? 1, ViewBag.PageSize));
        }

        [HttpGet("/Profile/MultipleDeleteAlamat/{id}")]
        public async Task<IActionResult> MultipleDeleteAlamat(string id)
        {
            ViewBag.Title = "Hapus Pasien";

            List<string> IdsStr = id.Split(",").ToList();
            List<long> memberId = new List<long>();

            foreach(var item in IdsStr)
            {
                memberId.Add(long.Parse(item));
            }

            List<VMMBiodataAddress> data = new List<VMMBiodataAddress>();
            foreach(var item in memberId)
            {
                VMMBiodataAddress bioAddress = await profile.GetByIdAlamat((int)item);
                data.Add(bioAddress);
            }


            return View(data);
        }


        [HttpPost]
        public async Task<VMResponse<List<VMMBiodataAddress>>?> MultipleDeleteAsync(string ids)
        {
            List<string> IdsStr = ids.Split(",").ToList();
            List<long> memberId = new List<long>();

            foreach (var item in IdsStr)
            {
                memberId.Add(long.Parse(item));
            }


            int bioUserId = HttpContext.Session.GetInt32("userBiodataId") ?? 0;

            if (bioUserId == 0)
            {
                throw new Exception("Biodata User ID is not found");
            }

            var request = new VMMBiodataAddress.MultipleDeleteRequest
            {
                Ids = memberId,
                UserId = bioUserId
            };

            return await profile.MultipleDeleteAsync(request);
        }


        [HttpGet("/Profile/TabProfile")]
        public async Task<IActionResult> TabProfile(int id)
        {
            ViewBag.Title = "Profile Pasien";
            VMMCustomer? data = new VMMCustomer();
            try
            {
                int bioId = HttpContext.Session.GetInt32("userBiodataId") ?? 0;
                data = await profile.GetCustomerByBioId(bioId);
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            if (data == null )
            {
                ViewBag.Message = "Tidak ada Profil Customer ditemukan berdasarkan pencarian Anda.";
                data = new VMMCustomer();
            }
            return View(data);
        }

        public async Task<IActionResult> EditProfile()
        {
            ViewBag.Title = "Edit Profile";
            VMMCustomer? data = new VMMCustomer();
            try
            {
                int bioId = HttpContext.Session.GetInt32("userBiodataId") ?? 0;
                data = await profile.GetCustomerByBioId(bioId);
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            if (data == null)
            {
                ViewBag.Message = "Tidak ada Profil Customer ditemukan berdasarkan pencarian Anda.";
                data = new VMMCustomer();
            }

            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse<VMMCustomer>?> EditProfileAsync(VMMCustomer data)
            => (await profile.UpdateProfileAsync(data));

        public async Task<IActionResult> EditEmail()
        {
            ViewBag.Title = "Edit Email";        
            VMMCustomer? data = new VMMCustomer();
            try
            {
                int bioId = HttpContext.Session.GetInt32("userBiodataId") ?? 0;
                data = await profile.GetCustomerByBioId(bioId);
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            if (data == null)
            {
                ViewBag.Message = "Tidak ada Profil Customer ditemukan berdasarkan pencarian Anda.";
                data = new VMMCustomer();
            }
            return View(data);
        }


        public async Task<IActionResult> EditPassword()
        {
            ViewBag.Title = "Edit Password";
            VMMCustomer? data = new VMMCustomer();
            try
            {
                int bioId = HttpContext.Session.GetInt32("userBiodataId") ?? 0;
                data = await profile.GetCustomerByBioId(bioId);
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            if (data == null)
            {
                ViewBag.Message = "Tidak ada Profil Customer ditemukan berdasarkan pencarian Anda.";
                data = new VMMCustomer();
            }
            return View(data);
        }
        public async Task<IActionResult> OTPEmail()
        {
            ViewBag.Title = "Kirim OTP";
            return View();
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
                ViewBag.Specialization = await specialization.getByFilter("");
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

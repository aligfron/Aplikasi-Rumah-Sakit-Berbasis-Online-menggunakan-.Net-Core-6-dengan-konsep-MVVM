using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using HealthCare340B.Web.AddOns;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;

namespace HealthCare340B.Web.Controllers
{
    public class SpecializationController : Controller
    {
        private readonly SpecializationModel spesialisasi;
        private readonly int pageSize;
        private string? _userId;
        private string? _roleCode;
        private readonly string imageFolder;
        public SpecializationController(IConfiguration _config)
        {
            spesialisasi = new SpecializationModel(_config);
            pageSize = int.Parse(_config["PageSize"]);
            imageFolder = _config["ImageFolder"];
        }

        private bool isInSession()
        {
            _userId = HttpContext.Session.GetString("userId") ?? null;

            return _userId != null;
        }

        private bool isInRole()
        {
            _roleCode = HttpContext.Session.GetString("userRoleCode") ?? null;

            return _roleCode == "ROLE_ADMIN";
        }
        public async Task<IActionResult> Index(string? filter, int? pageNumber, int? currPageSize)
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
            List<VMMSpecialization>? data = new List<VMMSpecialization>();
            
                data = (string.IsNullOrEmpty(filter)) ? await spesialisasi.getByFilter("") : await spesialisasi.getByFilter(filter);

            ViewBag.imgFolder = imageFolder;
            ViewBag.Title = "Spesialisasi Index";
            ViewBag.filter = filter;
            ViewBag.PageSize = (currPageSize ?? pageSize);

            return View(Pagination<VMMSpecialization>.Create(data ?? new List<VMMSpecialization>(), pageNumber ?? 1, ViewBag.PageSize));
        }
        public async Task<IActionResult> Create()
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
            ViewBag.Title = "New Spesialisasi";
            List<VMMSpecialization>? data = new List<VMMSpecialization>();

            data = await spesialisasi.getByFilter("");
            return View(data);
        }
        [HttpPost]
        public async Task<VMResponse<VMMSpecialization>?> CreateAsync(VMMSpecialization data)
        {
            VMResponse<VMMSpecialization>? response = null;

            try
            {
                data.CreatedBy = long.Parse(HttpContext.Session.GetString("userId")!);
                response = await spesialisasi.CreateAsync(data);
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
        public async Task<IActionResult> Edit(int id)
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
            VMMSpecialization? data = await spesialisasi.getById(id);
            ViewBag.dataid = data!.Id;
            ViewBag.dataname = data.Name;
            ViewBag.Title = "Spesialisasi Edit";

            List<VMMSpecialization>? data2 = new List<VMMSpecialization>();

            data2 = await spesialisasi.getByFilter("");
            return View(data2);
        }
        [HttpPost]
        public async Task<VMResponse<VMMSpecialization>?> EditAsync(VMMSpecialization data)
        {
            VMResponse<VMMSpecialization>? response = null;

            try
            {
                data.ModifiedBy = long.Parse(HttpContext.Session.GetString("userId")!);
                response = await spesialisasi.UpdateAsync(data);
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
        public async Task<IActionResult> Delete(int id, int pageNumber, int currPageSize)
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
            VMMSpecialization? data = await spesialisasi.getById(id);
            ViewBag.Title = "Delete Spesialisasi";
            ViewBag.PageNumber = pageNumber;
            ViewBag.PageSize = currPageSize;

            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse<VMMSpecialization>?> DeleteAsync(int id)
        {
            VMResponse<VMMSpecialization>? response = null;

            try
            {
                long userId = long.Parse(HttpContext.Session.GetString("userId")!);
                response = await spesialisasi.DeleteAsync(id, userId);
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

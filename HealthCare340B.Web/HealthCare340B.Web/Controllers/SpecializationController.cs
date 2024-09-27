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
        public SpecializationController(IConfiguration _config)
        {
            spesialisasi = new SpecializationModel(_config);
            pageSize = int.Parse(_config["PageSize"]);
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
           
            ViewBag.Title = "Spesialisasi Index";
            ViewBag.filter = filter;
            ViewBag.PageSize = (currPageSize ?? pageSize);

            return View(Pagination<VMMSpecialization>.Create(data ?? new List<VMMSpecialization>(), pageNumber ?? 1, ViewBag.PageSize));
        }
        public IActionResult Create()
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

            return View();
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

            ViewBag.Title = "Spesialisasi Edit";
            return View(data);
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
        public IActionResult Delete(int id)
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

            ViewBag.Title = "Delete Spesialisasi";

            return View(id);
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

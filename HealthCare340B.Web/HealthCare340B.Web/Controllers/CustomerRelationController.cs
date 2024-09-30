using HealthCare340B.ViewModel;
using HealthCare340B.Web.AddOns;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HealthCare340B.Web.Controllers
{
    public class CustomerRelationController : Controller
    {
        private readonly CustomerRelationModel _customerRelationModel;

        private string? _userId;
        private string? _roleCode;

        public CustomerRelationController(IConfiguration configuration)
        {
            _customerRelationModel = new CustomerRelationModel(configuration);
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

        public async Task<IActionResult> Index(string? filter)
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

            List<VMMCustomerRelation>? data = new List<VMMCustomerRelation>();

            try
            {
                data = await _customerRelationModel.GetByFilter(string.IsNullOrEmpty(filter) ? "" : filter);
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            ViewBag.Title = "Hubungan Pasien";
            ViewBag.Filter = filter;

            ViewBag.Breadcrumb = new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Name = "Beranda", Controller = "Home", Action = "Index" },
                new BreadcrumbItem { Name = "Hubungan Pasien", IsActive = true }
            };

            return View(data);
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

            ViewBag.Title = "Tambah Hubungan Pasien";

            return View();
        }

        [HttpPost]
        public async Task<VMResponse<VMMCustomerRelation>?> CreateAsync(VMMCustomerRelation data)
        {
            VMResponse<VMMCustomerRelation>? response = new VMResponse<VMMCustomerRelation>();

            try
            {
                data.CreatedBy = long.Parse(HttpContext.Session.GetString("userId")!);

                bool isNameExists = await _customerRelationModel.CheckNameExistsAsync(data.Name);

                if (isNameExists)
                {
                    response.StatusCode = HttpStatusCode.Conflict;
                    response.Message = "Nama sudah digunakan";
                }
                else
                {
                    response = await _customerRelationModel.CreateAsync(data);
                }

                if (response.StatusCode == HttpStatusCode.Created)
                {
                    HttpContext.Session.SetString("successMsg", response.Message);
                }
                else if (response.StatusCode != HttpStatusCode.Conflict)
                {
                    HttpContext.Session.SetString("errMsg", response.Message);
                }
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            return response;
        }

        public async Task<IActionResult> Edit(long id)
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

            VMMCustomerRelation? data = new VMMCustomerRelation();

            try
            {
                data = await _customerRelationModel.GetById(id);
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            ViewBag.Title = "Edit Hubungan Pasien";

            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse<VMMCustomerRelation>?> EditAsync(VMMCustomerRelation data)
        {
            VMResponse<VMMCustomerRelation>? response = new VMResponse<VMMCustomerRelation>();

            try
            {
                data.ModifiedBy = long.Parse(HttpContext.Session.GetString("userId")!);

                bool isNameExists = await _customerRelationModel.CheckNameExistsAsync(data.Name);

                if (isNameExists)
                {
                    response.StatusCode = HttpStatusCode.Conflict;
                    response.Message = "Nama sudah digunakan";
                }
                else
                {
                    response = await _customerRelationModel.UpdateAsync(data);
                }

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    HttpContext.Session.SetString("successMsg", response.Message);
                }
                else if (response.StatusCode != HttpStatusCode.Conflict)
                {
                    HttpContext.Session.SetString("errMsg", response.Message);
                }
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            return response;
        }

        public async Task<IActionResult> Delete(long id)
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

            VMMCustomerRelation? data = new VMMCustomerRelation();

            try
            {
                data = await _customerRelationModel.GetById(id);
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            ViewBag.Title = "Hapus Hubungan Pasien";

            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse<VMMCustomerRelation>?> DeleteAsync(long id)
        {
            VMResponse<VMMCustomerRelation>? response = null;

            try
            {
                long userId = long.Parse(HttpContext.Session.GetString("userId")!);

                response = await _customerRelationModel.DeleteAsync(id, userId);

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
        }

    }
}

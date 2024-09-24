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

        public CustomerRelationController(IConfiguration configuration)
        {
            _customerRelationModel = new CustomerRelationModel(configuration);
        }

        public async Task<IActionResult> Index(string? filter)
        {

            List<VMMCustomerRelation>? data = new List<VMMCustomerRelation>();

            try
            {
                data = await _customerRelationModel.GetByFilter(string.IsNullOrEmpty(filter) ? "" : filter);
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            ViewBag.Title = "Hubungan Pelanggan";
            ViewBag.Filter = filter;

            ViewBag.Breadcrumb = new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Name = "Beranda", Controller = "Home", Action = "Index" },
                new BreadcrumbItem { Name = "Hubungan Pasien", IsActive = true }
            };

            return View(data);
        }

        public async Task<IActionResult> Details(long id)
        {
            VMMCustomerRelation? data = new VMMCustomerRelation();

            try
            {
                data = await _customerRelationModel.GetById(id);
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            ViewBag.Title = "Detail Hubungan Pelanggan";

            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.Title = "Tambah Hubungan Pelanggan";

            return View();
        }

        [HttpPost]
        public async Task<VMResponse<VMMCustomerRelation>?> CreateAsync(VMMCustomerRelation data)
        {
            VMResponse<VMMCustomerRelation>? response = null;

            try
            {
                response = await _customerRelationModel.CreateAsync(data);
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            return response;
        }

        public async Task<IActionResult> Edit(long id)
        {
            VMMCustomerRelation? data = new VMMCustomerRelation();

            try
            {
                data = await _customerRelationModel.GetById(id);
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            ViewBag.Title = "Edit Hubungan Pelanggan";

            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse<VMMCustomerRelation>?> EditAsync(VMMCustomerRelation data)
        {
            VMResponse<VMMCustomerRelation>? response = null;

            try
            {
                response = await _customerRelationModel.UpdateAsync(data);
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            return response;
        }

        public async Task<IActionResult> Delete(long id)
        {
            VMMCustomerRelation? data = new VMMCustomerRelation();

            try
            {
                data = await _customerRelationModel.GetById(id);
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            ViewBag.Title = "Hapus Hubungan Pelanggan";

            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse<VMMCustomerRelation>?> DeleteAsync(long id, long userId)
        {
            VMResponse<VMMCustomerRelation>? response = null;

            try
            {
                response = await _customerRelationModel.DeleteAsync(id, userId);
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            return response;
        }

    }
}

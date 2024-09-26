using HealthCare340B.ViewModel;
using HealthCare340B.Web.AddOns;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.CompilerServices;

namespace HealthCare340B.Web.Controllers
{
    public class WalletDefaultNominalController : Controller
    {
        private WalletDefaultNominalModel walletDefaultNominal;
        private string? userId;
        private string? roleName;

        public WalletDefaultNominalController(IConfiguration _config)
        {
            walletDefaultNominal = new WalletDefaultNominalModel(_config);
        }
        private bool isInSession()
        {
            userId = HttpContext.Session.GetString("userId");

            if (userId == null)
            {
                HttpContext.Session.SetString("warnMsg", "Please Login First!");
                return false;
            }
            return true;
        }

        private bool isAdmin()
        {
            roleName = HttpContext.Session.GetString("userRole");

            if (roleName != "Role Admin")
            {
                HttpContext.Session.SetString("errMsg", "You Are Not Authorized!");
                return false;
            }
            return true;
        }

        public async Task<IActionResult> Index(int? filter = null)
        {
            if (!isInSession())
            {
                return RedirectToAction("Index", "Auth");
            }

            if (!isAdmin())
            {
                return RedirectToAction("Index", "Home");
            }
            List<VMMWalletDefaultNominal>? data = new List<VMMWalletDefaultNominal>();
            try
            {
                data = await walletDefaultNominal.GetByFilter(filter);
            }
            catch (Exception e)
            {
                HttpContext.Session.SetString("errMsg", e.Message);
            }

            ViewBag.Breadcrumb = new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Name = "Beranda", Controller = "Home", Action = "Index" },
                new BreadcrumbItem { Name = "Wallet Default Nominal", IsActive = true }
            };
            ViewBag.Title = "Wallet Default Nominal";
            ViewBag.Filter = filter;
            return View(data);
        }

        public IActionResult Create()
        {
            if (!isInSession())
            {
                return RedirectToAction("Index", "Auth");
            }

            if (!isAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Title = "Create New Wallet Default Nominal";
            return View();
        }

        [HttpPost]
        public async Task<VMResponse<VMMWalletDefaultNominal>?> CreateAsync(VMMWalletDefaultNominal data)
        {
            if (!isInSession() || !isAdmin())
                return new VMResponse<VMMWalletDefaultNominal>()
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Message = $"{HttpStatusCode.Forbidden} - You are not authorized!"
                };

            List<VMMWalletDefaultNominal>? dataSearch = new List<VMMWalletDefaultNominal>();
            try
            {
                dataSearch = await walletDefaultNominal.GetByFilter(data.Nominal);
                if (dataSearch != null && dataSearch.Count > 0)
                    throw new Exception("Duplicate Data!");
            }
            catch (Exception e)
            {
                if (dataSearch != null && dataSearch.Count > 0)
                {
                    VMResponse<VMMWalletDefaultNominal> response = new VMResponse<VMMWalletDefaultNominal>();
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = $"{HttpStatusCode.BadRequest} - Duplicate Data!";
                    return response;
                }
            }

            data.CreatedBy = long.Parse(userId!);
            VMResponse<VMMWalletDefaultNominal>? dataApi = new VMResponse<VMMWalletDefaultNominal>();
            try
            { 
                dataApi = await walletDefaultNominal.Create(data);
            }
            catch (Exception e)
            {
                HttpContext.Session.SetString("errMsg", e.Message);
                return dataApi;
            }
            HttpContext.Session.SetString("successMsg", "Data Successfully Created!");
            return dataApi;

        }

        public async Task<IActionResult> Edit(long id)
        {
            if (!isInSession())
            {
                return RedirectToAction("Index", "Auth");
            }

            if (!isAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            VMMWalletDefaultNominal? data = await walletDefaultNominal.GetById(id);
            ViewBag.Title = "Edit Wallet Default Nominal";
            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse<VMMWalletDefaultNominal>?> UpdateAsync(VMMWalletDefaultNominal data)
        {
            if (!isInSession() || !isAdmin())
                return new VMResponse<VMMWalletDefaultNominal>()
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Message = $"{HttpStatusCode.Forbidden} - You are not authorized!"
                };
            data.ModifiedBy = long.Parse(userId!);
            VMResponse<VMMWalletDefaultNominal>? dataApi = new VMResponse<VMMWalletDefaultNominal>();
            try
            {
                dataApi = await walletDefaultNominal.Update(data);

            }
            catch (Exception e)
            {
                HttpContext.Session.SetString("errMsg", e.Message);
                return dataApi;
            }
            HttpContext.Session.SetString("successMsg", "Data Successfully Edited!");
            return dataApi;
        }

        public async Task<IActionResult> Delete(long id)
        {
            if (!isInSession())
            {
                return RedirectToAction("Index", "Auth");
            }

            if (!isAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            VMMWalletDefaultNominal? data = await walletDefaultNominal.GetById(id);
            ViewBag.Title = "Delete Wallet Default Nominal";
            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse<VMMWalletDefaultNominal>?> DeleteAsync(long id)
        {
            if (!isInSession() || !isAdmin())
                return new VMResponse<VMMWalletDefaultNominal>()
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Message = $"{HttpStatusCode.Forbidden} - You are not authorized!"
                };

            VMResponse<VMMWalletDefaultNominal>? dataApi = new VMResponse<VMMWalletDefaultNominal>();
            try
            {
                dataApi = await walletDefaultNominal.Delete(id, long.Parse(userId!));
            }
            catch (Exception e)
            {
                HttpContext.Session.SetString("errMsg", e.Message);
                return dataApi;
            }
            HttpContext.Session.SetString("successMsg", "Data Successfully Deleted!");
            return dataApi;
        }
    }
}

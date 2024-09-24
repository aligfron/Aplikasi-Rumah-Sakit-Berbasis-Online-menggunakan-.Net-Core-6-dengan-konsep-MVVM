using HealthCare340B.ViewModel;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.CompilerServices;

namespace HealthCare340B.Web.Controllers
{
    public class WalletDefaultNominalController : Controller
    {
        private WalletDefaultNominalModel walletDefaultNominal;
        private string? custId;
        private int? roleId;

        public WalletDefaultNominalController(IConfiguration _config)
        {
            walletDefaultNominal = new WalletDefaultNominalModel(_config);
        }
        private bool isInSession()
        {
            custId = HttpContext.Session.GetString("userId");

            if (custId == null)
            {
                HttpContext.Session.SetString("warnMsg", "Please Login First!");
                return false;
            }
            return true;
        }

        private bool isAdmin()
        {
            roleId = HttpContext.Session.GetInt32("userRoleId");

            if (roleId != 1)
            {
                HttpContext.Session.SetString("errMsg", "You Are Not Authorized!");
                return false;
            }
            return true;
        }

        public async Task<IActionResult> Index(int? nominal = null)
        {
            if (!isInSession())
            {
                return RedirectToAction("Index", "Auth");
            }

            if (!isAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            List<VMMWalletDefaultNominal>? data = await walletDefaultNominal.GetByFilter(nominal);
            ViewBag.Title = "Wallet Default Nominal";
            ViewBag.Filter = nominal;
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

            data.CreatedBy = long.Parse(HttpContext.Session.GetString("userId")!);
            return await walletDefaultNominal.Create(data);
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
            data.ModifiedBy = long.Parse(HttpContext.Session.GetString("userId")!);
            return await walletDefaultNominal.Update(data);
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
            return await walletDefaultNominal.Delete(id, long.Parse(HttpContext.Session.GetString("userId")!));
        }
    }
}

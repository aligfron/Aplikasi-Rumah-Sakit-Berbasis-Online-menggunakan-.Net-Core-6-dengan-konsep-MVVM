using HealthCare340B.ViewModel;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace HealthCare340B.Web.Controllers
{
    public class WalletDefaultNominalController : Controller
    {
        private WalletDefaultNominalModel walletDefaultNominal;

        public WalletDefaultNominalController(IConfiguration _config)
        {
            walletDefaultNominal = new WalletDefaultNominalModel(_config);
        }

        public async Task<IActionResult> Index(int? nominal = null)
        {
            List<VMMWalletDefaultNominal>? data = await walletDefaultNominal.GetByFilter(nominal);
            ViewBag.Title = "Wallet Default Nominal";
            ViewBag.Filter = nominal;
            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.Title = "Create New Wallet Default Nominal";
            return View();
        }

        [HttpPost]
        public async Task<VMResponse<VMMWalletDefaultNominal>?> CreateAsync(VMMWalletDefaultNominal data)
        {
            data.CreatedBy = 1;
            return await walletDefaultNominal.Create(data);
        }

        public async Task<IActionResult> Edit(long id)
        {
            VMMWalletDefaultNominal? data = await walletDefaultNominal.GetById(id);
            ViewBag.Title = "Edit Wallet Default Nominal";
            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse<VMMWalletDefaultNominal>?> UpdateAsync(VMMWalletDefaultNominal data)
        {
            data.ModifiedBy = 1;
            return await walletDefaultNominal.Update(data);
        }

        public async Task<IActionResult> Delete(long id)
        {
            VMMWalletDefaultNominal? data = await walletDefaultNominal.GetById(id);
            ViewBag.Title = "Delete Wallet Default Nominal";
            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse<VMMWalletDefaultNominal>?> DeleteAsync(long id)
        {
            return await walletDefaultNominal.Delete(id, 1);
        }
    }
}

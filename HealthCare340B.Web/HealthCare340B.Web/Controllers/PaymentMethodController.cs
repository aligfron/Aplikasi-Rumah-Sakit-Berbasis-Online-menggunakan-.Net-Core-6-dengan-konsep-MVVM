using HealthCare340B.ViewModel;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.Web.Controllers
{
    public class PaymentMethodController : Controller
    {
        private readonly PaymentMethodModel paymentMethod;

        private readonly int pageSize;

        public PaymentMethodController(IConfiguration _config)
        {
            paymentMethod = new PaymentMethodModel(_config);
        }

        public async Task<IActionResult> Index()
        {
            List<VMMPaymentMethod>? data = await paymentMethod.GetByFilter("");
            ViewBag.Title = "Payment Method";
            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.Title = "Create New Payment Method";
            return View();
        }

        [HttpPost]
        public async Task<VMResponse<VMMPaymentMethod>?> CreateAsync(VMMPaymentMethod data)
        {
            return await paymentMethod.Create(data);
        }

        public async Task<IActionResult> Edit(long id)
        {
            VMMPaymentMethod? data = await paymentMethod.GetById(id);
            ViewBag.Title = "Edit Payment Method";
            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse<VMMPaymentMethod>?> UpdateAsync(VMMPaymentMethod data)
        {
            return await paymentMethod.Update(data);
        }

        public IActionResult Delete(long id)
        {
            ViewBag.Title = "Delete Payment Method";
            return View();
        }

        [HttpPost]
        public async Task<VMResponse<VMMPaymentMethod>?> DeleteAsync(long id)
        {
            long userId = 1;
            return await paymentMethod.Delete(id, userId);
        }
    }
}

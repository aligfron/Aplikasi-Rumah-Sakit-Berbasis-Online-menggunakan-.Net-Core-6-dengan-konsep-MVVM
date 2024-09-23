using HealthCare340B.ViewModel;
using HealthCare340B.Web.AddOns;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.Web.Controllers
{
    public class PaymentMethodController : Controller
    {
        private readonly PaymentMethodModel paymentMethod;

        private readonly int pageSize = 5;

        public PaymentMethodController(IConfiguration _config)
        {
            paymentMethod = new PaymentMethodModel(_config);
        }

        public async Task<IActionResult> Index(string? filter, int? pageNumber, int? currPageSize, string? orderBy)
        {
            List<VMMPaymentMethod>? data = await paymentMethod.GetByFilter(filter);

            switch (orderBy)
            {
                case "Asc":
                    data = data?.OrderBy(p => p.Name).ToList();
                    break;
                case "Desc":
                    data = data?.OrderByDescending(p => p.Name).ToList();
                    break;
                default:
                    data = data?.OrderBy(p => p.Name).ToList();
                    break;
            }

            ViewBag.Title = "Payment Method";
            ViewBag.Filter = filter;
            ViewBag.PageSize = (currPageSize ?? pageSize);
            ViewBag.OrderBy = orderBy ?? "Asc";
            return View(Pagination<VMMPaymentMethod>.Create(data ?? new List<VMMPaymentMethod>(), pageNumber ?? 1, ViewBag.PageSize));
        }

        public IActionResult Create()
        {
            ViewBag.Title = "Create New Payment Method";
            return View();
        }

        [HttpPost]
        public async Task<VMResponse<VMMPaymentMethod>?> CreateAsync(VMMPaymentMethod data)
        {
            data.CreatedBy = 1;
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
            data.ModifiedBy = 1;
            return await paymentMethod.Update(data);
        }

        public async Task<IActionResult> Delete(long id)
        {
            VMMPaymentMethod? data = await paymentMethod.GetById(id);
            ViewBag.Title = "Delete Payment Method";
            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse<VMMPaymentMethod>?> DeleteAsync(long id)
        {
            long userId = 1;
            return await paymentMethod.Delete(id, userId);
        }
    }
}

using HealthCare340B.ViewModel;
using HealthCare340B.Web.AddOns;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HealthCare340B.Web.Controllers
{
    public class PaymentMethodController : Controller
    {
        private readonly PaymentMethodModel paymentMethod;

        private readonly int pageSize = 5;

        private string? userId;
        private string? roleName;


        public PaymentMethodController(IConfiguration _config)
        {
            paymentMethod = new PaymentMethodModel(_config);
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


        public async Task<IActionResult> Index(string? filter, int? pageNumber, int? currPageSize, string? orderBy)
        {
            if (!isInSession())
            {
                return RedirectToAction("Index", "Auth");
            }

            if (!isAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

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
            if (!isInSession())
            {
                return RedirectToAction("Index", "Auth");
            }

            if (!isAdmin())
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Title = "Create New Payment Method";
            return View();
        }

        [HttpPost]
        public async Task<VMResponse<VMMPaymentMethod>?> CreateAsync(VMMPaymentMethod data)
        {
            if (!isInSession() || !isAdmin())
                return new VMResponse<VMMPaymentMethod>()
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Message = $"{HttpStatusCode.Forbidden} - You are not authorized!"
                };
            data.CreatedBy = long.Parse(userId!);
            return await paymentMethod.Create(data);
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

            VMMPaymentMethod? data = await paymentMethod.GetById(id);
            ViewBag.Title = "Edit Payment Method";
            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse<VMMPaymentMethod>?> UpdateAsync(VMMPaymentMethod data)
        {
            if (!isInSession() || !isAdmin())
                return new VMResponse<VMMPaymentMethod>()
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Message = $"{HttpStatusCode.Forbidden} - You are not authorized!"
                };

            data.ModifiedBy = long.Parse(userId!);
            return await paymentMethod.Update(data);
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

            VMMPaymentMethod? data = await paymentMethod.GetById(id);
            ViewBag.Title = "Delete Payment Method";
            return View(data);
        }

        [HttpPost]
        public async Task<VMResponse<VMMPaymentMethod>?> DeleteAsync(long id)
        {
            if (!isInSession() || !isAdmin())
                return new VMResponse<VMMPaymentMethod>()
                {
                    StatusCode = HttpStatusCode.Forbidden,
                    Message = $"{HttpStatusCode.Forbidden} - You are not authorized!"
                };

            return await paymentMethod.Delete(id, long.Parse(userId!));
        }
    }
}

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

        private string? ModifyString(string dataName)
        {
            try
            {
                string[] words = dataName.Trim().Split(' ');
                string modifiedName = "";

                foreach (string word in words)
                {
                    if (word == " " || string.IsNullOrEmpty(word))
                        continue;
                    modifiedName += word;
                    if (!word.EndsWith("-"))
                        modifiedName += " ";
                }

                modifiedName = modifiedName.Trim();
                return modifiedName;
            }
            catch (Exception e)
            {
                return null;
            }         
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

            List<VMMPaymentMethod>? data = new List<VMMPaymentMethod>();

            try
            {
                data = await paymentMethod.GetByFilter(filter);
            }
            catch (Exception e)
            {
                HttpContext.Session.SetString("errMsg", e.Message);
            }

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

            ViewBag.Breadcrumb = new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Name = "Beranda", Controller = "Home", Action = "Index" },
                new BreadcrumbItem { Name = "Payment Method", IsActive = true }
            };

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


            List<VMMPaymentMethod>? dataSearch = new List<VMMPaymentMethod>();

            string modifiedName = ModifyString(data.Name!);
            if (string.IsNullOrEmpty(modifiedName))
            {
                VMResponse<VMMPaymentMethod> response = new VMResponse<VMMPaymentMethod>();
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = $"{HttpStatusCode.BadRequest} - Input Data is Invalid!";
                return response;
            }

            try
            {
                dataSearch = await paymentMethod.GetByFilter(modifiedName);
                if (dataSearch != null && dataSearch.Count > 0)
                    throw new Exception("Duplicate Data!");
            }
            catch (Exception e)
            {
                if (dataSearch != null && dataSearch.Count > 0)
                {
                    VMResponse<VMMPaymentMethod> response = new VMResponse<VMMPaymentMethod>();
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = $"{HttpStatusCode.BadRequest} - Duplicate Data!";
                    return response;
                }
            }

            data.CreatedBy = long.Parse(userId!);
            VMResponse<VMMPaymentMethod>? dataApi = new VMResponse<VMMPaymentMethod>();
            try
            {
                dataApi = await paymentMethod.Create(data);
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

            List<VMMPaymentMethod>? dataSearch = new List<VMMPaymentMethod>();

            string modifiedName = ModifyString(data.Name);
            if (string.IsNullOrEmpty(modifiedName))
            {
                VMResponse<VMMPaymentMethod> response = new VMResponse<VMMPaymentMethod>();
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = $"{HttpStatusCode.BadRequest} - Input Data is Invalid!";
                return response;
            }

            try
            {
                dataSearch = await paymentMethod.GetByFilter(modifiedName);
                if (dataSearch != null && dataSearch.Count > 0)
                    throw new Exception("Duplicate Data!");
            }
            catch (Exception e)
            {
                if (dataSearch != null && dataSearch.Count > 0)
                {
                    VMResponse<VMMPaymentMethod> response = new VMResponse<VMMPaymentMethod>();
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = $"{HttpStatusCode.BadRequest} - Duplicate Data!";
                    return response;
                }
            }

            data.ModifiedBy = long.Parse(userId!);
            VMResponse<VMMPaymentMethod>? dataApi = new VMResponse<VMMPaymentMethod>();
            try
            {
                dataApi = await paymentMethod.Update(data);
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
            VMResponse<VMMPaymentMethod>? dataApi = new VMResponse<VMMPaymentMethod>();
            try
            {
                dataApi = await paymentMethod.Delete(id, long.Parse(userId!));
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

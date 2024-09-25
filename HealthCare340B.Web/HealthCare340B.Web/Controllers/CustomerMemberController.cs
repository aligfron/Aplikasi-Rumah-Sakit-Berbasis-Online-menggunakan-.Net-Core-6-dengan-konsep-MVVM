using HealthCare340B.ViewModel;
using HealthCare340B.Web.AddOns;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.Web.Controllers
{
    [Route("Profile/Pasien")]
    public class CustomerMemberController : Controller
    {
        private readonly CustomerMemberModel _customerMemberModel;

        public CustomerMemberController(IConfiguration configuration)
        {
            _customerMemberModel = new CustomerMemberModel(configuration);
        }

        public async Task<IActionResult> Index(string? filter)
        {
            List<VMMCustomerMember>? data = new List<VMMCustomerMember>();

            try
            {
                data = await _customerMemberModel.GetByFilter(string.IsNullOrEmpty(filter) ? "" : filter);
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            ViewBag.Title = "Daftar Pasien";
            ViewBag.Role = "ROLE_PASIEN";
            ViewBag.Filter = filter;

            ViewBag.Breadcrumb = new List<BreadcrumbItem>
            {
                new BreadcrumbItem { Name = "Beranda", Controller = "Home", Action = "Index" },
                new BreadcrumbItem { Name = "Profile", Controller = "Profile", Action = "Index" },
                new BreadcrumbItem { Name = "Pasien", IsActive = true }
            };

            return View(data);
        }
    }
}

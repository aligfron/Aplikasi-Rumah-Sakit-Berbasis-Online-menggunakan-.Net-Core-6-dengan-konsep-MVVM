using HealthCare340B.ViewModel;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.Web.Controllers
{
    public class AuthController : Controller
    {
        private UserModel user;
        private string? _custId;

        public AuthController(IConfiguration _config)
        {
            user = new UserModel(_config);
        }

        private bool inSession()
        {
            _custId = HttpContext.Session.GetString("userId");
            return string.IsNullOrEmpty(_custId);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            ViewBag.Title = "Masuk";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(VMMUser data)
        {
            try
            {
                if (inSession())
                {
                    HttpContext.Session.SetString("errMsg", "You're Already Login");
                    RedirectToAction("Index", "Home");
                }
                VMMUser? dataApi = await user.GetByEmail(data.Email!);
                if (dataApi != null)
                {
                    if (data.Password == dataApi.Password)
                    {
                        long value = dataApi.Id;
                        HttpContext.Session.SetString("userId", value.ToString());
                        HttpContext.Session.SetString("userEmail", dataApi.Email!);
                        HttpContext.Session.SetString("userName", dataApi.Name!);
                        HttpContext.Session.SetInt32("userRoleId", (int)dataApi.RoleId!);
                        HttpContext.Session.SetString("userRole", dataApi.RoleName!);

                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        throw new Exception("Invalid Password");
                    }
                }
                else
                {
                    throw new Exception("Email not registered!");
                }
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}

using HealthCare340B.ViewModel;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
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
            return !string.IsNullOrEmpty(_custId);
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            ViewBag.Title = "Masuk";
            //ViewBag.Url = url;
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
        [HttpPost]
        public async Task<string> Login2(VMMUser data) 
        {
            try
            {
                VMResponse<VMMUser?> responseData = await user.LoginAsync(data);
                VMMUser? dataApi = responseData.Data;
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
                        HttpContext.Session.SetString("userRoleCode", dataApi.RoleCode!);
                        
                        HttpContext.Session.SetInt32("userBiodataId", (int)dataApi.BiodataId!);
                        if (!string.IsNullOrEmpty(dataApi.ImagePath))
                        {
                            HttpContext.Session.SetString("userImagePath", dataApi.ImagePath);
                        }
                        HttpContext.Session.SetString("userSince", dataApi.CreatedOn.ToString("yyyy"));

                        HttpContext.Session.SetString("successMsg", "Anda berhasil login!");

                        return "success";
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
                return "error";
            }
            //return RedirectToAction("Index","Home");
        }
    }
}

using HealthCare340B.ViewModel;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.Web.Controllers
{
    public class RegisterController : Controller
    {
        private readonly RegistrationModel register;

        //public RegisterController()
        public IActionResult EmailConfirm()
        {
            ViewBag.Title = "Daftar";
            return View();
        }
        [HttpPost]
        public async Task<VMResponse<VMTToken>> EmailConfirmAsync(string email) =>
            await register.ConfirmAsync(email);

        public IActionResult VerifyOtp() 
        {
            ViewBag.Title = "Verifikasi E-Mail";
            return View();
        }
        public async Task<VMResponse<VMTToken>> VerifyOtpAsync(string otp) =>
            await register.VerifyOtpAsync(otp);

        public IActionResult ConfirmPassword() 
        {
            ViewBag.Title = "Set Password";
            return View();
        }
        public async Task<VMResponse<VMMUser>> ConfirmPasswordAsync(string password, string confirmPassword) =>
            await register.ConfirmPasswordAsync(password, confirmPassword);

        public IActionResult Register() 
        {
            ViewBag.Title = "Set Password";
            return View();
        }
    }
}

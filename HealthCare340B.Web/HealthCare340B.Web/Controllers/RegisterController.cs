using HealthCare340B.ViewModel;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HealthCare340B.Web.Controllers
{
    public class RegisterController : Controller
    {
        private readonly RegistrationModel register;
        public RegisterController(IConfiguration _config)
        {
            register = new RegistrationModel(_config);
        }
        //public RegisterController()
        public IActionResult EmailConfirm()
        {
            ViewBag.Title = "Daftar";
            return View();
        }
        [HttpPost]
        public async Task<VMResponse<VMTToken>> EmailConfirmAsync(string email)
        {
            var response = await register.EmailConfirmAsync(email);

            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created) // Misalnya, ada properti IsSuccess dalam VMResponse
            {
                // Jika berhasil, kembalikan tampilan untuk modal OTP
                return response; // Ganti dengan nama view yang sesuai
            }

            // Jika tidak berhasil, kembalikan pesan error
            return response;
        }

        

        public IActionResult VerifyOtp() 
        {
            ViewBag.Title = "Verifikasi E-Mail";
            return View();
        }
        [HttpPost]
        public async Task<VMResponse<VMTToken>> VerifyOtpAsync(string otp) 
        {
            var response = await register.VerifyOtpAsync(otp);

            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created) // Misalnya, ada properti IsSuccess dalam VMResponse
            {
                // Jika berhasil, kembalikan tampilan untuk modal OTP
                return response; // Ganti dengan nama view yang sesuai
            }

            // Jika tidak berhasil, kembalikan pesan error
            return response;
        }
            

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
        public async Task<VMResponse<VMMUser>> ResgisterAsync(VMMUser data) =>
            await register.RegisterAsync(data);
    }
}

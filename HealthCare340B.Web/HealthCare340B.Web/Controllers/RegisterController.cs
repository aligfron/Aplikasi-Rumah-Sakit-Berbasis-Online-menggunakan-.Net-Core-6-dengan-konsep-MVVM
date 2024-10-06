using HealthCare340B.ViewModel;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static System.Net.WebRequestMethods;

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

            if (response.StatusCode == HttpStatusCode.OK || response.StatusCode == HttpStatusCode.Created) 
            {
                
                return response; 
            }

            
            return response;
        }

        

        public IActionResult VerifyOtp() 
        {
            ViewBag.Title = "Verifikasi E-Mail";
            return View();
        }
        [HttpPost]
        public async Task<VMResponse<VMTToken>> VerifyOtpAsync(string OTP) 
        {

            var response = await register.VerifyOtpAsync(OTP);

           /* var email = */

            if (response.StatusCode == HttpStatusCode.OK) 
            {
                return response; 
            }
            return response;
        }

        public async Task<VMResponse<VMTToken>> ResendToken(string OTP) 
        {
            var emailResponse = await register.VerifyOtpAsync(OTP);
            var email = emailResponse.Data.Email;
            var response = await register.EmailConfirmAsync(email);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response;
            }
            return response;
        }
        
            

        public IActionResult ConfirmPassword() 
        {
            ViewBag.Title = "Set Password";
            return View();
        }
        [HttpPost]
        public async Task<VMResponse<VMMUser>> ConfirmPasswordAsync(string password, string confirmPassword)
        {
            var response = await register.ConfirmPasswordAsync(password, confirmPassword);
            if (response.StatusCode == HttpStatusCode.OK) 
            {
                return response;
            }
            return response;
        }

        public IActionResult SignUp() 
        {
            ViewBag.Title = "Daftar";
            return View();
        }
        [HttpPost]
        public async Task<VMResponse<VMMUser>> SignUpAsync(VMMUser data) 
        {
            VMResponse<VMMUser> response = await register.SignUpAsync(data);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response;
            }
            return response;
        }
    }
}

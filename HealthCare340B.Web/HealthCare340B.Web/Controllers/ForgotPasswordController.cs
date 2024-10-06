using HealthCare340B.ViewModel;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HealthCare340B.Web.Controllers
{
    public class ForgotPasswordController : Controller
    {
        private readonly ForgotPasswordModel forgot;
        public ForgotPasswordController(IConfiguration _config)
        {
            forgot = new ForgotPasswordModel(_config);
        }
        public IActionResult SendEmail()
        {
            ViewBag.Title = "Lupa Password";
            return View();
        }

        [HttpPost]
        public async Task<VMResponse<VMTToken>> SendEmailAsync(string email)
        {
            var response = await forgot.SendEmailAsync(email);

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

            var response = await forgot.VerifyOtpAsync(OTP);

            /* var email = */

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
            var response = await forgot.ConfirmPasswordAsync(password, confirmPassword);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response;
            }
            return response;
        }
    }
}

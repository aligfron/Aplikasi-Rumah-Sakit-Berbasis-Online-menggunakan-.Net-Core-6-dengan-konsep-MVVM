using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Cryptography;
using MailKit.Net.Smtp;
using System.Net.Mail;
using static System.Net.WebRequestMethods;

namespace HealthCare340B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ForgotPasswordController : ControllerBase
    {
        public DAForgotPassword? forgot;
        private readonly HealthCare340BContext db;
        public ForgotPasswordController(HealthCare340BContext _db)
        {
            forgot = new DAForgotPassword(_db);
            db = _db;
        }

        [HttpPost("[action]/{email?}")]
        public async Task<ActionResult> GenerateOTP(string email)
        {
            try
            {
                VMResponse<VMTToken> response = await Task.Run(() => forgot.GenerateOTP(email));
                
                
                var mail = new MimeMessage();
                mail.From.Add(MailboxAddress.Parse("alvafikri14@gmail.com"));
                mail.To.Add(MailboxAddress.Parse(response.Data.Email));

                mail.Subject = "OTP Verification";
                mail.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = $"your OTP is {response.Data.Token}" };

                using var smtp = new MailKit.Net.Smtp.SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                smtp.Authenticate("alvafikri14@gmail.com", "tirk fjil eirm ggja");
                smtp.Send(mail);
                smtp.Disconnect(true);
                smtp.Dispose();
                return Created("", new { Message = "OTP successfully created." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }

        /*[HttpPost("[action]")]
        public async Task<ActionResult> SendEmail(string email) 
        {
            try
            {
                VMResponse<VMMUser> response = await Task.Run(() => forgot.GetByEmail(email));
                return Ok(response);
            }
            catch (Exception ex) 
            {
            }
        }*/


        [HttpPost("[action]")]
        public async Task<ActionResult> VerifyOTP(string OTP)
        {
            try
            {
                VMResponse<VMTToken> response = await Task.Run(() => forgot.VerifyOTP(OTP));
                if (response.Data != null)
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest("OTP is Wrong or Expired");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost("[action]")]

        public async Task<ActionResult> ConfirmPassword(string password, string confirmPassword)
        {
            try
            {
                VMResponse<VMMUser> response = await Task.Run(() => forgot.ConfirmPassword(password, confirmPassword));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}

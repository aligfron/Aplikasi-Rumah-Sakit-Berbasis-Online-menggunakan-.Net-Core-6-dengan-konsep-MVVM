using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using static System.Net.WebRequestMethods;

namespace HealthCare340B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        public DARegister? user;
        private readonly HealthCare340BContext db;
        public RegisterController(HealthCare340BContext _db)
        {
            user = new DARegister(_db);
            db = _db;
        }
        [HttpPost("[action]/{email?}")]
        public async Task<ActionResult> GenerateOTP(string email) 
        {
            try 
            {
                VMResponse<VMTToken> response = await Task.Run(() => user.GenerateOTP(email));

                if (response.Data != null)
                {
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
                    return Created("api/Register", response);
                }
                else 
                {
                    return BadRequest(response.Message);
                }
            }
            catch (Exception ex) 
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [HttpPost("[action]")]
        public async Task<ActionResult> VerifyOTP(string OTP)
        {
            try
            {
                VMResponse<VMTToken> response = await Task.Run(() => user.VerifyOTP(OTP));
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
                VMResponse<VMMUser> response = await Task.Run(() => user.ConfirmPassword(password,confirmPassword));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("[action]")]
        public async Task<ActionResult> Register(VMMUser data)
        {
            try
            {
                VMResponse<VMMUser> response = await Task.Run(() => user.register(data));
                if (response.Data != null)
                {
                    return Ok(response);
                }
                else
                {
                    Console.WriteLine("UserController.Create: " + response.Message);
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("UserController.Create: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
    
}

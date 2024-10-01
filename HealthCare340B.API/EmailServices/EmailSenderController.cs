using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Cryptography;
using MailKit.Net.Smtp;
using System.Net.Mail;

namespace HealthCare340B.API.EmailServices
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailSenderController : ControllerBase
    {
        [HttpPost]
        public IActionResult SendEmail(string Email, string OTP)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("alvafikri14@gmail.com"));
            email.To.Add(MailboxAddress.Parse(Email));

            email.Subject = "OTP Verification";
            email.Body = new TextPart(MimeKit.Text.TextFormat.Html) {Text = $"your OTP is {OTP}"};

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
            smtp.Authenticate("alvafikri14@gmail.com", "tirk fjil eirm ggja");
            smtp.Send(email);
            smtp.Disconnect(true);
            smtp.Dispose();
            return Ok();
        }
    }
}

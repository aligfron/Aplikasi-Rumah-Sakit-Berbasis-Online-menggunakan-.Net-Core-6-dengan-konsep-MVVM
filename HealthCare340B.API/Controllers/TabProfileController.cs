using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Net;

namespace HealthCare340B.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TabProfileController : ControllerBase
    {
        private readonly DATabProfile tabProfile;
        public TabProfileController(HealthCare340BContext _db)
        {
            tabProfile = new DATabProfile(_db);
        }


        [HttpGet("[action]/{id?}")]
        public async Task<ActionResult> GetById(long id)
        {
            try
            {
                VMResponse<VMMCustomer> response = await Task.Run(() => tabProfile.GetById(id));
                if (response.Data != null)
                {
                    return Ok(response);
                }
                else
                {
                    Console.WriteLine(response.Message);
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("TabProfileController.GetById: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{id?}")]
        public async Task<ActionResult> GetCustomerByBioId(long id)
        {
            try
            {
                VMResponse<VMMCustomer> response = await Task.Run(() => tabProfile.GetCustomerByBioId(id));
                if (response.Data != null)
                {
                    return Ok(response);
                }
                else
                {
                    Console.WriteLine(response.Message);
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
               Console.WriteLine("TabProfileController.GetCustomerByBioId: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{id?}")]
        public async Task<ActionResult> GetUserById(long id)
        {
            try
            {
                VMResponse<VMMUser> response = await Task.Run(() => tabProfile.GetUserById(id));
                if (response.Data != null)
                {
                    return Ok(response);
                }
                else
                {
                    Console.WriteLine(response.Message);
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("TabProfileController.GetUserById: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> Update(VMMCustomer data)
        {
            try
            {
                return Ok(await Task.Run(() => tabProfile.Update(data)));
            }
            catch (Exception e)
            {
                Console.WriteLine("TabProfileController.Update: " + e.Message);

                return BadRequest(e.Message);
            }
        }

        [HttpPost("request-update-email")]
        public IActionResult RequestUpdateEmail([FromBody] VMTToken request)
        {
            var response = tabProfile.GenerateOTP(request.Email);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                return Ok(new { message = "OTP has been sent to your email." });
            }
            else
            {
                return StatusCode((int)response.StatusCode, response.Message);
            }
        }

        [HttpPost("UpdateEmail")]
        public ActionResult<VMResponse<VMMUser>> UpdateEmail([FromBody] VMMUser data)
        {
            if (data == null || string.IsNullOrEmpty(data.Email))
            {
                return BadRequest(new VMResponse<VMMUser>
                {
                    Message = "Invalid data",
                    StatusCode = HttpStatusCode.BadRequest
                });
            }

            var response = tabProfile.UpdateEmail(data);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return Ok(response);
            }
            else
            {
                return StatusCode((int)response.StatusCode, response);
            }
        }

        [HttpPost("[action]/{email?}")]
        public async Task<ActionResult> GenerateOTP(string email)
        {
            try
            {
                VMResponse<VMTToken> response = await Task.Run(() => tabProfile.GenerateOTP(email));


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

    }
}

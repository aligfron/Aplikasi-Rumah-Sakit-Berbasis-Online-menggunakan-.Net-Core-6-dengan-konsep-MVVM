using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Mvc;
using static System.Net.WebRequestMethods;

namespace HealthCare340B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ForgotPasswordController:ControllerBase
    {
        public DAUser? user;
        private readonly HealthCare340BContext db;
        public ForgotPasswordController(HealthCare340BContext _db)
        {
            user = new DAUser(_db);
            db = _db;
        }

        [HttpPost("[action]/{email?}")]
        public async Task<ActionResult> GenerateOTP(string email)
        {
            try
            {
                await user.OTPForgotPassword(email);
                return Created("", new { Message = "OTP successfully created." });
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
                VMResponse<VMTToken> response = await Task.Run(() => user.VerifyForgotPassword(OTP));
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


        [HttpPut("[action]")]
        public async Task<ActionResult> EditPassword(VMMUser data) 
        {
            try
            {
                VMResponse<VMMUser> response = await Task.Run(() => user.EditPassword(data));
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

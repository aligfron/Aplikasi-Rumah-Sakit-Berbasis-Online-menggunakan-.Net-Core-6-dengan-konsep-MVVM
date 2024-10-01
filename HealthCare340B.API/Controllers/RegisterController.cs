using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthCare340B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        public DAUser? user;
        private readonly HealthCare340BContext db;
        public RegisterController(HealthCare340BContext _db)
        {
            user = new DAUser(_db);
            db = _db;
        }
        [HttpPost("[action]/{email?}")]
        public async Task<ActionResult> GenerateOTP(string email) 
        {
            try 
            {
                await user.GenerateOTP(email);
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
        [HttpPost]
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

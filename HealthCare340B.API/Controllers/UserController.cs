using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HealthCare340B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public DAUser? user;
        private readonly HealthCare340BContext db;
        public UserController(HealthCare340BContext _db)
        {
            user = new DAUser(_db);
            db = _db;
        }
        [HttpGet("[action]/{email?}")]
        public async Task<ActionResult> GetByEmail(string email)
        {
            try
            {
                VMResponse<VMMUser> response = await Task.Run(() => user!.GetByEmail(email));
                return (response.Data != null) ?
                    Ok(response) : throw new Exception(response.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("User Controller.GetByEmail : " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        /*[HttpPost]
        public async Task<ActionResult> Create(VMMUser data) 
        {
            try
            {
                VMResponse<VMMUser> response = await Task.Run(
                    () => user.Create(data)
                );
                if (response.Data != null)
                {
                    return Ok(response);
                }
                else
                {
                    Console.WriteLine("CustomerController.Create: " + response.Message);
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                //Console logging
                Console.WriteLine("CustomerController.Create: " + ex.Message);
                return BadRequest(ex.Message);
            }

        }*/

        
        [HttpPut("login")]
        public async Task<ActionResult> Login(VMMUser data)
        {
            try
            {
                VMResponse<VMMUser> response = await Task.Run(() => user.Login(data));
                if (response.Data != null)
                {
                    return Ok(response);
                }
                else
                {
                    Console.WriteLine("LoginController.Update: " + response.Message);
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("UserController.Update: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}


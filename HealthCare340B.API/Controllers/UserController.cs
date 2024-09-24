using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        public DAUser? user;
        public UserController(HealthCare340BContext _db)
        {
            user = new DAUser(_db);
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
    }
}

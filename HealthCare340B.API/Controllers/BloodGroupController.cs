using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodGroupController : ControllerBase
    {
        private DABloodGroup _bloodGroup;

        public BloodGroupController(HealthCare340BContext db)
        {
            _bloodGroup = new DABloodGroup(db);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                VMResponse<List<VMMBloodGroup>> response = await Task.Run(() => _bloodGroup.GetByFilter(""));

                if (response.Data != null && response.Data.Count > 0)
                {
                    return Ok(response);
                }
                else
                {
                    Console.WriteLine("BloodGroupController.GetAll: " + response.Message);
                    return BadRequest(response);
                }
            }
            catch (Exception e)
            {
                // Console Logging
                Console.WriteLine("BloodGroupController.GetAll: " + e.Message);

                return BadRequest(e.Message);
            }
        }
    }
}

using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SpecializationController : Controller
    {
        private readonly DASpecialization spec;

        public SpecializationController(HealthCare340BContext _db)
        {
            spec = new DASpecialization(_db);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                VMResponse<List<VMMSpecialization>?> response = await Task.Run(() => spec.GetAll());
                if (response.Data != null && response.Data.Count > 0)
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
                // Console Logging
                Console.WriteLine("SpecializationController.GetAll: " + ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}

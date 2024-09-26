using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalFacilityController : ControllerBase
    {
        private readonly DAMedicalFacility medFac;

        public MedicalFacilityController(HealthCare340BContext _db)
        {
            medFac = new DAMedicalFacility(_db);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                VMResponse<List<VMMMedicalFacility>?> response = await Task.Run(() => medFac.GetAll());
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
                Console.WriteLine("MedicalFacilityController.GetAll: " + ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{idDoctor?}")]
        public async Task<ActionResult> GetByDoctorId(long? idDoctor)
        {
            try
            {
                if (idDoctor == null)
                    throw new ArgumentNullException();
                VMResponse<List<VMMMedicalFacility>?> response = await Task.Run(() => medFac.GetByDoctorId((long)idDoctor));
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
                Console.WriteLine("MedicalFacilityController.GetAll: " + ex.Message);

                return BadRequest(ex.Message);
            }
        }
    }
}

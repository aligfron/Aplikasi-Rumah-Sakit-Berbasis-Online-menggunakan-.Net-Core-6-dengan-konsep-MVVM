using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorTreatmentController : Controller
    {
        private readonly DADoctorTreatment treatment;

        public DoctorTreatmentController(HealthCare340BContext _db)
        {
            treatment = new DADoctorTreatment(_db);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                VMResponse<List<VMTDoctorTreatment>?> response = await Task.Run(() => treatment.GetAll());
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
                Console.WriteLine("DoctorTreatmentController.GetAll: " + ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{medicalFacilityId?}/{docId?}")]
        public async Task<ActionResult> GetByDoctorIdAndMedicalFacilityId(long? medicalFacilityId, long? docId)
        {
            try
            {
                if (medicalFacilityId == null || docId == null)
                    throw new ArgumentNullException();
                VMResponse<List<VMTDoctorTreatment>?> response = await Task.Run(() => treatment.GetByDoctorIdAndMedicalFacilityId((long)docId, (long)medicalFacilityId));
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
            catch (Exception e)
            {
                // Console Logging
                Console.WriteLine("DoctorTreatmentController.GetByDoctorIdAndMedicalFacilityId: " + e.Message);

                return BadRequest(e.Message);
            }
        }
    }
}

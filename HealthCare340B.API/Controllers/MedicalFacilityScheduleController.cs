using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalFacilityScheduleController : ControllerBase
    {
        private DAMedicalFacilitySchedule mfs;

        public MedicalFacilityScheduleController(HealthCare340BContext _db)
        {
            mfs = new DAMedicalFacilitySchedule(_db);
        }

        [HttpGet("[action]/{medicalFacilityId?}/{doctorId?}")]
        public async Task<ActionResult> GetByMedicalFacilityIdAndDoctorId(long? medicalFacilityId, long? doctorId)
        {
            try
            {
                if (medicalFacilityId == null || doctorId == null)
                    throw new ArgumentNullException();
                VMResponse<List<VMMMedicalFacilitySchedule>?> response = await Task.Run(() =>
                    mfs.GetByMedicalFacilityIdAndDoctorId((long)medicalFacilityId, (long)doctorId));

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
                Console.WriteLine("MedicalFacilityScheduleController.GetByMedicalFacilityIdAndDoctorId: " + e.Message);

                return BadRequest(e.Message);
            }
        }
    }
}

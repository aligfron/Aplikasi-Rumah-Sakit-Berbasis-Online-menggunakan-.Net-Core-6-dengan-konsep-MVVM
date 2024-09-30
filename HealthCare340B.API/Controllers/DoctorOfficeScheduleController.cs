using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HealthCare340B.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorOfficeScheduleController : ControllerBase
    {
        private DADoctorOfficeSchedule dos;

        public DoctorOfficeScheduleController(HealthCare340BContext _db)
        {
            dos = new DADoctorOfficeSchedule(_db);
        }

        [HttpGet("[action]/{doctorId?}/{medFacId?}/{day?}/{timeStart?}")]
        public async Task<ActionResult> GetByUserChoice(long doctorId, long medFacId, string day, string timeStart)
        {
            try
            {
                VMResponse<VMTDoctorOfficeSchedule?> response = await Task.Run(() => dos.GetByUserChoice(doctorId, medFacId, day, timeStart));
                if (response.Data != null)
                {
                    return Ok(response);
                }
                else
                {
                    throw new Exception(response.Message);
                }
            }
            catch (Exception e)
            {
                throw new Exception($"{HttpStatusCode.BadRequest} - {e.Message}");
            }
        }
    }
}

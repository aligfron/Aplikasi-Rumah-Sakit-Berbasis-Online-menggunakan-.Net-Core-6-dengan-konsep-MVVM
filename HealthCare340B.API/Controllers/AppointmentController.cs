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
    public class AppointmentController : ControllerBase
    {
        private DAAppointment appointment;

        public AppointmentController(HealthCare340BContext _db)
        {
            appointment = new DAAppointment(_db);
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> GetEmptySlotDate(List<VMMMedicalFacilitySchedule> data)
        {
            try
            {
                List<DateTime>? excludedDate = await Task.Run(() => appointment.GetEmptySlotDate(data));
                if (excludedDate!.Count > 0)
                {
                    return Ok(excludedDate);
                }
                else
                {
                    Console.WriteLine("Every slot date is available!");
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"{HttpStatusCode.BadRequest}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(VMTAppointment data)
        {
            try
            {
                VMResponse<VMTAppointment> response = await Task.Run(() => appointment.Create(data));
                if (response.StatusCode == HttpStatusCode.OK)
                    return Created("api/Appointment", response);
                else
                    throw new Exception(response.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("AppointmentController.Create: " + e.Message);

                return BadRequest(e.Message);
            }
        }
    }
}

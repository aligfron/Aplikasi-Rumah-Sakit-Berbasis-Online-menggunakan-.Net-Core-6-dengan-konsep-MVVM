using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentDoneController : ControllerBase
    {
        public DAAppointmentDone _appointmentDone;

        public AppointmentDoneController(HealthCare340BContext db)
        {
            _appointmentDone = new DAAppointmentDone(db);
        }

        [HttpGet("{parentId}")]
        public async Task<ActionResult> GetAll(long parentId)
        {
            try
            {
                VMResponse<List<VMTAppointmentDone>> response = await Task.Run(() => _appointmentDone.GetByFilter("", parentId));

                if (response.Data != null && response.Data.Count > 0)
                {
                    return Ok(response);
                }
                else
                {
                    Console.WriteLine("AppointmentDoneController.GetAll: " + response.Message);
                    return BadRequest(response);
                }
            }
            catch (Exception e)
            {
                // Console Logging
                Console.WriteLine("AppointmentDoneController.GetAll: " + e.Message);

                return BadRequest(e.Message);
            }
        }

        [HttpGet("[action]/{parentId}/{filter}")]
        public async Task<ActionResult> GetByFilter(long parentId, string filter)
        {
            try
            {
                VMResponse<List<VMTAppointmentDone>> response = await Task.Run(() => _appointmentDone.GetByFilter(filter, parentId));

                if (response.Data != null && response.Data.Count > 0)
                {
                    return Ok(response);
                }
                else
                {
                    Console.WriteLine("AppointmentDoneController.GetByFilter: " + response.Message);
                    return BadRequest(response);
                }
            }
            catch (Exception e)
            {
                // Console Logging
                Console.WriteLine("AppointmentDoneController.GetByFilter: " + e.Message);

                return BadRequest(e.Message);
            }
        }
    }
}

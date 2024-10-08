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

        [HttpPut("[action]/{originalDate}")]
        public async Task<ActionResult> Update(VMTAppointment data, string originalDate)
        {
            VMResponse<VMTAppointment?> response = new VMResponse<VMTAppointment?>();
            try
            {
                response = await Task.Run(() => appointment.Update(data, originalDate));
                if (response.StatusCode == HttpStatusCode.OK)
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
                Console.WriteLine("AppointmentController.Update: " + e.Message);

                return BadRequest(response);
            }
        }

        [HttpDelete("[action]/{id}/{userId}")]
        public async Task<ActionResult> DeleteOne(long id, long userId)
        {
            VMResponse<VMTAppointment?> response = new VMResponse<VMTAppointment?>();
            try
            {
                response = await Task.Run(() => appointment.DeleteOne(id, userId));
                if (response.StatusCode == HttpStatusCode.OK)
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
                Console.WriteLine("AppointmentController.DeleteOne: " + e.Message);

                throw new Exception(response.Message);
            }
        }

        [HttpPut("[action]/{userId}")]
        public async Task<ActionResult> DeleteMultiple(List<long> id, long userId)
        {
            VMResponse<List<VMTAppointment>?> response = new VMResponse<List<VMTAppointment>?>();
            try
            {
                response = await Task.Run(() => appointment.DeleteMultiple(id, userId));
                if (response.StatusCode == HttpStatusCode.OK)
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
                Console.WriteLine("AppointmentController.DeleteMultiple: " + e.Message);

                throw new Exception(response.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> GetByCustomerId(List<long>? custId)
        {
            try
            {
                if (custId == null || custId.Count < 1)
                    throw new ArgumentNullException();
                VMResponse<List<VMTAppointment>?> response = await Task.Run(() => appointment.GetByCustomerId(custId!));
                if (response.Data != null && response.Data.Count > 0)
                {
                    return Ok(response);
                }
                else
                {
                    Console.WriteLine("No Appointments Found!");
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                throw new Exception($"{HttpStatusCode.BadRequest}");
            }
        }

        [HttpPost("[action]")]
        public async Task<ActionResult> GetAppointmentDone(List<VMTAppointment> dataApp)
        {
            try
            {
                VMResponse<List<VMTAppointmentDone>?> response = await Task.Run(() => appointment.GetAppointmentDone(dataApp));
                if (response.Data != null)
                {
                    return Ok(response);
                }
                else
                {
                    Console.WriteLine("No appointments found!");
                    return NoContent();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
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
                throw new Exception(e.Message);
            }
        }

        [HttpGet("[action]/{bioId?}")]
        public async Task<ActionResult> GetCustId(long? bioId)
        {
            try
            {
                if (bioId == null)
                    throw new ArgumentNullException();
                VMResponse<VMMCustomer> response = await Task.Run(() => appointment.GetCustId((long)bioId));

                if (response.Data != null)
                {
                    return Ok(response);
                }
                else
                {
                    return NoContent(); 
                }
            }
            catch (Exception e)
            {
                throw new Exception($"{HttpStatusCode.BadRequest} - {e.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(VMTAppointment data)
        {
            VMResponse<VMTAppointment> response = new VMResponse<VMTAppointment>();
            try
            {
                response = await Task.Run(() => appointment.Create(data));
                if (response.StatusCode == HttpStatusCode.Created)
                    return Created("api/Appointment", response);
                else if (response.StatusCode == HttpStatusCode.Found)
                    throw new Exception(response.Message);
                else
                    throw new Exception(response.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("AppointmentController.Create: " + e.Message);

                return BadRequest(response);
            }
        }
    }
}

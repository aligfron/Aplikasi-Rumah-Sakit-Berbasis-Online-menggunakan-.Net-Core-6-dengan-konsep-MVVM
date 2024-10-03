using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionController : ControllerBase
    {
        public DAPrescription _prescription;

        public PrescriptionController(HealthCare340BContext db)
        {
            _prescription = new DAPrescription(db);
        }

        [HttpGet("[action]/{appointmentId}")]
        public async Task<ActionResult> GetByAppointmentId(long appointmentId)
        {
            try
            {
                VMResponse<List<VMTPrescription>> response = await Task.Run(() => _prescription.GetByAppointmentId(appointmentId));

                if (response.Data != null && response.Data.Count > 0)
                {
                    return Ok(response);
                }
                else
                {
                    Console.WriteLine("PrescriptionController.GetByAppointmentId: " + response.Message);
                    return BadRequest(response);
                }
            }
            catch (Exception e)
            {
                // Console Logging
                Console.WriteLine("PrescriptionController.GetByAppointmentId: " + e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> UpdatePrintAttempt(List<VMTPrescription> model)
        {
            try
            {
                VMResponse<List<VMTPrescription>> response = await Task.Run(() => _prescription.UpdatePrintAttempt(model));

                if (response.Data != null)
                {
                    return Ok(response);
                }
                else
                {
                    Console.WriteLine("PrescriptionController.UpdatePrintAttempt: " + response.Message);
                    return BadRequest(response);
                }
            }
            catch (Exception e)
            {
                // Console Logging
                Console.WriteLine("PrescriptionController.UpdatePrintAttempt: " + e.Message);

                return BadRequest(e.Message);
            }
        }
    }
}

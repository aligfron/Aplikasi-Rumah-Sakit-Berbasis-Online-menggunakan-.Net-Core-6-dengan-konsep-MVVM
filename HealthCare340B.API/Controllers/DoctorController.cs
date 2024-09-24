using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HealthCare340B.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly DADoctor doctor;

        public DoctorController(HealthCare340BContext _db)
        {
            doctor = new DADoctor(_db);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                VMResponse<List<VMMDoctor>?> response = await Task.Run(() => doctor.GetAll());
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
                Console.WriteLine("DoctorController.GetAll: " + ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetByFilter(string? location, string? doctorName, string? specialization, string? treatment)
        {
            try
            {
                VMResponse<List<VMMDoctor>?> response = await Task.Run(() => doctor.GetByFilter(location, doctorName, specialization, treatment));
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
                Console.WriteLine("DoctorController.GetByFilter: " + e.Message);

                return BadRequest(e.Message);
            }
        }
    }
}
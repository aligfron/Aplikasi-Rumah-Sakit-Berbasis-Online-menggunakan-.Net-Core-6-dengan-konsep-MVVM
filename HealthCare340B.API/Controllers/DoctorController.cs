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
        private readonly DAMedicalFacility medfac;

        public DoctorController(HealthCare340BContext _db)
        {
            doctor = new DADoctor(_db);
            medfac = new DAMedicalFacility(_db);
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
        public async Task<ActionResult> GetAllLocation()
        {
            try
            {
                VMResponse<List<VMMLocation>?> response = await Task.Run(() => medfac.GetAllLocation());
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
                Console.WriteLine("DoctorController.GetAllLocation: " + ex.Message);

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

        [HttpGet("[action]/{idDoctor?}")]
        public async Task<ActionResult> GetById(long? idDoctor)
        {
            try
            {
                if (idDoctor == null)
                    throw new ArgumentNullException();
                VMResponse<VMMDoctor?> response = await Task.Run(() => doctor.GetById((long)idDoctor));
                if (response.Data != null)
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
                Console.WriteLine("DoctorController.GetById: " + e.Message);

                return BadRequest(e.Message);
            }
        }

        [HttpGet("[action]/{id?}")]
        public async Task<ActionResult> GetLocationById(long? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException();
                VMResponse<VMMLocation?> response = await Task.Run(() => medfac.GetLocationById((long)id));
                if (response.Data != null)
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
                Console.WriteLine("DoctorController.GetLocationById: " + e.Message);

                return BadRequest(e.Message);
            }
        }
    }
}
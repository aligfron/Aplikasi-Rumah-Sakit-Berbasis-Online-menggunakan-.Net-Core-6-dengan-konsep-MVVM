using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationDoctorController : Controller
    {
        private readonly DASpecializationDoctor dASpecializationDoctor;
        public SpecializationDoctorController(HealthCare340BContext _db)
        {
            dASpecializationDoctor = new DASpecializationDoctor(_db);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                VMResponse<VMTCurrentDoctorSpecialization?> response = await Task.Run(() => dASpecializationDoctor.GetById(id));
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
            catch (Exception ex)
            {
                Console.WriteLine("dASpecializationDoctorController.GetByID " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Create(VMTCurrentDoctorSpecialization Data)
        {
            try
            {
                return Created("api/dASpecializationDoctor", await Task.Run(() => dASpecializationDoctor.Create(Data)));
            }
            catch (Exception ex)
            {
                Console.WriteLine("dASpecializationDoctorController.Create " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(VMTCurrentDoctorSpecialization Data)
        {
            try
            {
                VMResponse<VMTCurrentDoctorSpecialization?> response = await Task.Run(() => dASpecializationDoctor.Update(Data));
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
            catch (Exception ex)
            {
                Console.WriteLine("dASpecializationDoctorController.Update " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

    }
}

using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class SpecializationController : Controller
    {
        private readonly DASpecialization spec;

        public SpecializationController(HealthCare340BContext _db)
        {
            spec = new DASpecialization(_db);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                VMResponse<List<VMMSpecialization>?> response = await Task.Run(() => spec.GetAll());
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
                Console.WriteLine("SpecializationController.GetAll: " + ex.Message);

                return BadRequest(ex.Message);
            }
        }

        //tambahan ali
        [HttpGet]
        public async Task<ActionResult> GetAllFilter()
        {
            try
            {
                VMResponse<List<VMMSpecialization>> response = await Task.Run(() => spec.GetByFilter(""));
                if (response.Data.Count > 0)
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
                Console.WriteLine("spec Controller Get All: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{filter?}")]
        public async Task<ActionResult> GetBy(string? filter)
        {
            try
            {
                return (filter != null)
                    ? Ok(await Task.Run(() => spec.GetByFilter(filter)))
                    : BadRequest("spec Name Tidak Ada");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                VMResponse<VMMSpecialization?> response = await Task.Run(() => spec.GetById(id));
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
                Console.WriteLine("specController.GetByID " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Create(VMMSpecialization Data)
        {
            try
            {
                return Created("api/spec", await Task.Run(() => spec.Create(Data)));
            }
            catch (Exception ex)
            {
                Console.WriteLine("specController.Create " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(VMMSpecialization Data)
        {
            try
            {
                VMResponse<VMMSpecialization?> response = await Task.Run(() => spec.Update(Data));
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
                Console.WriteLine("specController.Update " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id?}/{userId?}")]
        public async Task<ActionResult> Delete(int id, long userId)
        {
            try
            {
                VMResponse<VMMSpecialization> response = await Task.Run(() => spec.Delete(id, userId));
                if (response.Data != null) { return Ok(response); }
                else
                {
                    Console.WriteLine(response.Message);
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("specController.Delete " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}

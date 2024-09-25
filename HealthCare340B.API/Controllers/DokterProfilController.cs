using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DokterProfilController : Controller
    {
        private readonly DADokterProfil dokterProfil;
        public DokterProfilController(HealthCare340BContext _db)
        {
            dokterProfil = new DADokterProfil(_db);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            try
            {
                VMResponse<VMMDoctor?> response = await Task.Run(() => dokterProfil.GetByDokterProfil(id));
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
                Console.WriteLine("DokterProfilController.GetByID " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}

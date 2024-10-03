using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BiodataController : ControllerBase
    {
        private DABiodata _biodata;

        public BiodataController(HealthCare340BContext _db)
        {
            _biodata = new DABiodata(_db);
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> UpdateImagePath(VMMBiodatum data)
        {
            try
            {
                VMResponse<VMMBiodatum?> response = await Task.Run(() => _biodata.UpdateImagePath(data));

                if (response.Data != null)
                {
                    return Ok(response);
                }
                else
                {
                    Console.WriteLine("BiodataController.UpdateImagePath: " + response.Message);
                    return BadRequest(response);
                }
            }
            catch (Exception e)
            {
                // Console Logging
                Console.WriteLine("BiodataController.UpdateImagePath: " + e.Message);

                return BadRequest(e.Message);
            }
        }
    }
}

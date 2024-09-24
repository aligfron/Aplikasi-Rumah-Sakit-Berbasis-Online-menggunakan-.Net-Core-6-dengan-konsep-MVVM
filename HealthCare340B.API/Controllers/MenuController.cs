using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase
    {
        private DAMenu menu;
        private object db;

        public MenuController(HealthCare340BContext _db)
        {

            menu = new DAMenu(_db);
            db = _db;
        }
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                VMResponse<List<VMMMenuRole?>> response = await Task.Run(() => menu.GetByFilter(""));
                if (response.Data!.Count > 0 && response.Data != null)
                {
                    return Ok(response);
                }
                else
                {
                    throw new Exception(response.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("action/{filter?}")]
        public async Task<ActionResult> GetByFilter(string filter)
        {
            try
            {
                VMResponse<List<VMMMenuRole?>> response = await Task.Run(() => menu.GetByFilter(filter));
                if (string.IsNullOrEmpty(filter)) throw new ArgumentNullException("No filter provived");

                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MenuController.GetByFilter: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }

}
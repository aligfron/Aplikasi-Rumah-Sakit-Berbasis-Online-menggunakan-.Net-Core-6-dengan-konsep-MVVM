using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TabProfileController : ControllerBase
    {
        private readonly DATabProfile tabProfile;
        public TabProfileController(HealthCare340BContext _db)
        {
            tabProfile = new DATabProfile(_db);
        }


        [HttpGet("[action]/{id?}")]
        public async Task<ActionResult> GetById(long id)
        {
            try
            {
                VMResponse<VMMCustomer> response = await Task.Run(() => tabProfile.GetById(id));
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
                Console.WriteLine("TabProfileController.GetById: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{id?}")]
        public async Task<ActionResult> GetCustomerByBioId(long id)
        {
            try
            {
                VMResponse<VMMCustomer> response = await Task.Run(() => tabProfile.GetCustomerByBioId(id));
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
               Console.WriteLine("TabProfileController.GetCustomerByBioId: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> Update(VMMCustomer data)
        {
            try
            {
                return Ok(await Task.Run(() => tabProfile.Update(data)));
            }
            catch (Exception e)
            {
                Console.WriteLine("TabProfileController.Update: " + e.Message);

                return BadRequest(e.Message);
            }
        }
    }
}

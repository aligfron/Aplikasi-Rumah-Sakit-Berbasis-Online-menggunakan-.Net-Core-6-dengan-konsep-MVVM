using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace HealthCare340B.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalItemController : ControllerBase
    {
        private readonly DAMedicalItem medItem;

        public MedicalItemController(HealthCare340BContext _db)
        {
            medItem = new DAMedicalItem(_db);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetAllCategory()
        {
            try
            {
                VMResponse<List<VMMMedicalItemCategory>?> response = await Task.Run(() => medItem.GetAllCategory());
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
                Console.WriteLine("MedicalItemController.GetAllCategory: " + ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetAllSegmentation()
        {
            try
            {
                VMResponse<List<VMMMedicalItemSegmentation>?> response = await Task.Run(() => medItem.GetAllSegmentation());
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
                Console.WriteLine("MedicalItemController.GetAllSegmentation: " + ex.Message);

                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetByFilter(long? categoryId, bool? segmentation, int? priceMax, int? priceMin, string? filter)
        {
            try
            {
                VMResponse<List<VMMMedicalItem>?> response = await Task.Run(() => medItem.GetByFilter(categoryId, segmentation, priceMax, priceMin, filter));
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
                Console.WriteLine("MedicalItemController.GetByFilter: " + e.Message);

                return BadRequest(e.Message);
            }
        }

        [HttpGet("[action]/{id?}")]
        public async Task<ActionResult> GetById(long? id)
        {
            try
            {
                if (id == null)
                    throw new ArgumentNullException();
                VMResponse<List<VMMMedicalItemCategory>?> response = await Task.Run(() => medItem.GetById((long)id));
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
                Console.WriteLine("MedicalItemController.GetById: " + e.Message);

                return BadRequest(e.Message);
            }
        }


    }
}

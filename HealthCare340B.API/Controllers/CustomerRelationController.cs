using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;

namespace HealthCare340B.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerRelationController : ControllerBase
    {
        private DACustomerRelation _customerRelation;

        public CustomerRelationController(HealthCare340BContext _db)
        {
            _customerRelation = new DACustomerRelation(_db);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                VMResponse<List<VMMCustomerRelation>> response = await Task.Run(() => _customerRelation.GetByFilter(""));

                if (response.Data != null && response.Data.Count > 0)
                {
                    return Ok(response);
                }
                else
                {
                    Console.WriteLine("CustomerRelationController.GetAll: " + response.Message);
                    return BadRequest(response);
                }
            }
            catch (Exception e)
            {
                // Console Logging
                Console.WriteLine("CustomerRelationController.GetAll: " + e.Message);

                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            try
            {
                VMResponse<VMMCustomerRelation> response = await Task.Run(() => _customerRelation.GetById(id));

                if (response.Data != null)
                {
                    return Ok(response);
                }
                else
                {
                    Console.WriteLine("CustomerRelationController.GetById: " + response.Message);
                    return BadRequest(response);
                }
            }
            catch (Exception e)
            {
                // Console Logging
                Console.WriteLine("CustomerRelationController.GetById: " + e.Message);

                return BadRequest(e.Message);
            }
        }

        [HttpGet("[action]/{filter}")]
        public async Task<ActionResult> GetByFilter(string filter)
        {
            try
            {
                VMResponse<List<VMMCustomerRelation>> response = await Task.Run(() => _customerRelation.GetByFilter(filter));

                if (response.Data != null && response.Data.Count > 0)
                {
                    return Ok(response);
                }
                else
                {
                    Console.WriteLine("CustomerRelationController.GetByFilter: " + response.Message);
                    return BadRequest(response);
                }
            }
            catch (Exception e)
            {
                // Console Logging
                Console.WriteLine("CustomerRelationController.GetByFilter: " + e.Message);

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(VMMCustomerRelation model)
        {
            try
            {
                VMResponse<VMMCustomerRelation> response = await Task.Run(() => _customerRelation.Create(model));

                if (response.Data != null)
                {
                    return Ok(response);
                }
                else
                {
                    Console.WriteLine("CustomerRelationController.Insert: " + response.Message);
                    return BadRequest(response);
                }
            }
            catch (Exception e)
            {
                // Console Logging
                Console.WriteLine("CustomerRelationController.Insert: " + e.Message);

                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(VMMCustomerRelation model)
        {
            try
            {
                VMResponse<VMMCustomerRelation> response = await Task.Run(() => _customerRelation.Update(model));

                if (response.Data != null)
                {
                    return Ok(response);
                }
                else
                {
                    Console.WriteLine("CustomerRelationController.Update: " + response.Message);
                    return BadRequest(response);
                }
            }
            catch (Exception e)
            {
                // Console Logging
                Console.WriteLine("CustomerRelationController.Update: " + e.Message);

                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}/{userId}")]
        public async Task<ActionResult> Delete(long id, long userId)
        {
            try
            {
                VMResponse<VMMCustomerRelation> response = await Task.Run(() => _customerRelation.Delete(id, userId));

                if (response.Data != null)
                {
                    return Ok(response);
                }
                else
                {
                    Console.WriteLine("CustomerRelationController.Delete: " + response.Message);
                    return BadRequest(response);
                }
            }
            catch (Exception e)
            {
                // Console Logging
                Console.WriteLine("CustomerRelationController.Delete: " + e.Message);

                return BadRequest(e.Message);
            }
        }
    }
}

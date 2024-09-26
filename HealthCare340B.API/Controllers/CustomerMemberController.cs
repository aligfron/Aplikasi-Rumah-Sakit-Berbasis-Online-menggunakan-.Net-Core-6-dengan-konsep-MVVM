using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;

namespace HealthCare340B.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerMemberController : ControllerBase
    {
        private DACustomerMember _customerMember;

        public CustomerMemberController(HealthCare340BContext db)
        {
            _customerMember = new DACustomerMember(db);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                VMResponse<List<VMMCustomerMember>> response = await Task.Run(() => _customerMember.GetByFilter(""));

                if (response.Data != null && response.Data.Count > 0)
                {
                    return Ok(response);
                }
                else
                {
                    Console.WriteLine("CustomerMemberController.GetAll: " + response.Message);
                    return BadRequest(response);
                }
            }
            catch (Exception e)
            {
                // Console Logging
                Console.WriteLine("CustomerMemberController.GetAll: " + e.Message);

                return BadRequest(e.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(long id)
        {
            try
            {
                VMResponse<VMMCustomerMember> response = await Task.Run(() => _customerMember.GetById(id));

                if (response.Data != null)
                {
                    return Ok(response);
                }
                else
                {
                    Console.WriteLine("CustomerMemberController.GetById: " + response.Message);
                    return BadRequest(response);
                }
            }
            catch (Exception e)
            {
                // Console Logging
                Console.WriteLine("CustomerMemberController.GetById: " + e.Message);

                return BadRequest(e.Message);
            }
        }

        [HttpGet("[action]/{filter}")]
        public async Task<ActionResult> GetByFilter(string filter)
        {
            try
            {
                VMResponse<List<VMMCustomerMember>> response = await Task.Run(() => _customerMember.GetByFilter(filter));

                if (response.Data != null && response.Data.Count > 0)
                {
                    return Ok(response);
                }
                else
                {
                    Console.WriteLine("CustomerMemberController.GetByFilter: " + response.Message);
                    return BadRequest(response);
                }
            }
            catch (Exception e)
            {
                // Console Logging
                Console.WriteLine("CustomerMemberController.GetByFilter: " + e.Message);

                return BadRequest(e.Message);
            }
        }

        [HttpGet("[action]/{userId?}")]
        public async Task<ActionResult> GetByUserId(long? userId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException();
                VMResponse<List<VMMCustomerMember>?> response = await Task.Run(() => _customerMember.GetByUserId((long)userId));
                if (response.Data != null && response.Data.Count > 0)
                {
                    return Ok(response);
                }
                else
                {
                    Console.WriteLine("CustomerMemberController.GetByUserId: " + response.Message);
                    return BadRequest(response);
                }
            }
            catch (Exception e) 
            {
                Console.WriteLine("CustomerMemberController.GetByUserId: " + e.Message);

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(VMMCustomerMember model)
        {
            try
            {
                VMResponse<VMMCustomerMember> response = await Task.Run(() => _customerMember.Create(model));

                if (response.Data != null)
                {
                    return Ok(response);
                }
                else
                {
                    Console.WriteLine("CustomerMemberController.Create: " + response.Message);
                    return BadRequest(response);
                }
            }
            catch (Exception e)
            {
                // Console Logging
                Console.WriteLine("CustomerMemberController.Create: " + e.Message);

                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(VMMCustomerMember model)
        {
            try
            {
                VMResponse<VMMCustomerMember> response = await Task.Run(() => _customerMember.Update(model));

                if (response.Data != null)
                {
                    return Ok(response);
                }
                else
                {
                    Console.WriteLine("CustomerMemberController.Update: " + response.Message);
                    return BadRequest(response);
                }
            }
            catch (Exception e)
            {
                // Console Logging
                Console.WriteLine("CustomerMemberController.Update: " + e.Message);

                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id}/{userId}")]
        public async Task<ActionResult> Delete(long id, long userId)
        {
            try
            {
                VMResponse<VMMCustomerMember> response = await Task.Run(() => _customerMember.Delete(id, userId));

                if (response.Data != null)
                {
                    return Ok(response);
                }
                else
                {
                    Console.WriteLine("CustomerMemberController.Delete: " + response.Message);
                    return BadRequest(response);
                }
            }
            catch (Exception e)
            {
                // Console Logging
                Console.WriteLine("CustomerMemberController.Delete: " + e.Message);

                return BadRequest(e.Message);
            }
        }
    }
}

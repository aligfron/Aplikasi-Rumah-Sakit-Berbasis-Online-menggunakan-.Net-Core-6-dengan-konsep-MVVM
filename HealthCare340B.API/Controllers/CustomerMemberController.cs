using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using System.Net;

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

        [HttpGet("{parentId}")]
        public async Task<ActionResult> GetAll(long parentId)
        {
            try
            {
                VMResponse<List<VMMCustomerMember>> response = await Task.Run(() => _customerMember.GetByFilter("", parentId));

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

        [HttpGet("{id}/{parentId}")]
        public async Task<ActionResult> GetById(long id, long parentId)
        {
            try
            {
                VMResponse<VMMCustomerMember> response = await Task.Run(() => _customerMember.GetById(id, parentId));

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

        [HttpGet("[action]/{parentId}/{filter}")]
        public async Task<ActionResult> GetByFilter(string filter, long parentId)
        {
            try
            {
                VMResponse<List<VMMCustomerMember>> response = await Task.Run(() => _customerMember.GetByFilter(filter, parentId));

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

        [HttpGet("[action]/{userId?}/{parentId}")]
        public async Task<ActionResult> GetByUserId(long? userId, long parentId)
        {
            try
            {
                if (userId == null)
                    throw new ArgumentNullException();
                VMResponse<List<VMMCustomerMember>?> response = await Task.Run(() => _customerMember.GetByUserId((long)userId, parentId));
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
                // Trim the input fields to avoid whitespaces
                model.Fullname = model.Fullname?.Trim();

                // Check if the trimmed input is empty or null
                if (string.IsNullOrWhiteSpace(model.Fullname))
                {
                    return BadRequest(new VMResponse<VMMCustomerRelation>
                    {
                        Message = "Fullname cannot be empty or just spaces.",
                        StatusCode = HttpStatusCode.BadRequest
                    });
                }

                // Check if the date of birth is not greater than today
                if (model.Dob.HasValue && model.Dob.Value.Date > DateTime.Today)
                {
                    return BadRequest(new VMResponse<VMMCustomerRelation>
                    {
                        Message = "Date of birth cannot be greater than today.",
                        StatusCode = HttpStatusCode.BadRequest
                    });
                }

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

using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private DAPaymentMethod paymentMethod;

        public PaymentMethodController(HealthCare340BContext _db)
        {
            paymentMethod = new DAPaymentMethod(_db);
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                VMResponse<List<VMMPaymentMethod>?> response = await Task.Run(() => paymentMethod.GetByFilter(""));
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
                //Console Logging
                Console.WriteLine("PaymentMethodController.GetAll: " + e.Message);

                return BadRequest(e.Message);
            }
        }

        [HttpGet("[action]/{filter?}")]
        public async Task<ActionResult> GetByFilter(string? filter)
        {
            try
            {
                VMResponse<List<VMMPaymentMethod>?> response = await Task.Run(() => paymentMethod.GetByFilter(filter));
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
                //Console Logging
                Console.WriteLine("PaymentMethodController.GetByFilter: " + e.Message);

                return BadRequest(e.Message);
            }
        }

        [HttpGet("[action]/{id?}")]
        public async Task<ActionResult> GetById(long? id)
        {
            if (id == null)
                throw new Exception("ID cannot be null!");
            try
            {
                VMResponse<VMMPaymentMethod?> response = await Task.Run(() => paymentMethod.GetById((long)id));
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
                //Console Logging
                Console.WriteLine("PaymentMethodController.GetById: " + e.Message);

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(VMMPaymentMethod data)
        {
            try
            {
                return Created("api/PaymentMethod", await Task.Run(() => paymentMethod.Create(data)));
            }
            catch (Exception e)
            {
                //Console Logging
                Console.WriteLine("PaymentMethodController.Create: " + e.Message);

                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<ActionResult> Update(VMMPaymentMethod data)
        {
            try
            {
                VMResponse<VMMPaymentMethod> response = await Task.Run(() => paymentMethod.Update(data));
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
                Console.WriteLine("PaymentMethodController.Update: " + e.Message);

                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(long? id, long? deletedBy)
        {
            if (id == null || deletedBy == null)
                throw new Exception("ID or deletedBy cannot be null!");
            try
            {
                VMResponse<VMMPaymentMethod> response = await Task.Run(() => paymentMethod.Delete((long)id, (long)deletedBy));
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
                Console.WriteLine("PaymentMethodController.Delete: " + e.Message);

                return BadRequest(e.Message);
            }
        }
    }
}

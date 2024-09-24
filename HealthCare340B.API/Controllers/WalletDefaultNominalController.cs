using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalletDefaultNominalController : ControllerBase
    {
        private DAWalletDefaultNominal walletDefaultNominal;

        public WalletDefaultNominalController(HealthCare340BContext _db)
        {
            walletDefaultNominal = new DAWalletDefaultNominal(_db);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                VMResponse<List<VMMWalletDefaultNominal>?> response = await Task.Run(() => walletDefaultNominal.GetByFilter(null));
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
                Console.WriteLine("WalletDefaultNominalController.GetAll: " + e.Message);

                return BadRequest(e.Message);
            }
        }

        [HttpGet("[action]/{nominal?}")]
        public async Task<IActionResult> GetByFilter(int? nominal = null)
        {
            try
            {
                VMResponse<List<VMMWalletDefaultNominal>?> response = await Task.Run(() => walletDefaultNominal.GetByFilter(nominal));
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
                Console.WriteLine("WalletDefaultNominalController.GetByFilter: " + e.Message);

                return BadRequest(e.Message);
            }
        }

        [HttpGet("[action]/{id?}")]
        public async Task<IActionResult> GetById(long? id)
        {
            try
            {
                if (id == null)
                    throw new Exception("ID cannot be null!");
                VMResponse<VMMWalletDefaultNominal?> response = await Task.Run(() => walletDefaultNominal.GetById((long)id));
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
                Console.WriteLine("WalletDefaultNominalController.GetById: " + e.Message);

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create(VMMWalletDefaultNominal data)
        {
            try
            {
                return Created("api/WalletDefaultNominal", await Task.Run(() => walletDefaultNominal.Create(data)));
            }
            catch (Exception e)
            {
                //Console Logging
                Console.WriteLine("WalletDefaultNominalController.Create: " + e.Message);

                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update(VMMWalletDefaultNominal data)
        {
            try
            {
                VMResponse<VMMWalletDefaultNominal> response = await Task.Run(() => walletDefaultNominal.Update(data));
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
                Console.WriteLine("WalletDefaultNominalController.Update: " + e.Message);

                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long? id, long? deletedBy)
        {
            try
            {
                if (id == null || deletedBy == null)
                    throw new Exception("ID or deletedBy cannot be null!");
                VMResponse<VMMWalletDefaultNominal> response = await Task.Run(() => walletDefaultNominal.Delete((long)id, (long)deletedBy));
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
                Console.WriteLine("WalletDefaultNominalController.Delete: " + e.Message);

                return BadRequest(e.Message);
            }
        }
    }
}

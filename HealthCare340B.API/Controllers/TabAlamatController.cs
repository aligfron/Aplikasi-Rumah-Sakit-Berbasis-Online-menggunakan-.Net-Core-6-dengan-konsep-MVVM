using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TabAlamatController : ControllerBase
    {
        private readonly DATabAlamat tabAlamat;

        public TabAlamatController(HealthCare340BContext _db)
        {
            tabAlamat = new DATabAlamat(_db);
        }
        public class MultipleDeleteRequest
        {
            public List<long> Ids { get; set; }
            public long UserId { get; set; }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetAll()
        {
            try
            {
                VMResponse<List<VMMBiodataAddress>> response = await Task.Run(() => tabAlamat.GetAll());
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
                Console.WriteLine("TabAlamatController.GetAll: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{id?}")]
        public async Task<ActionResult> GetById(long id)
        {
            try
            {
                VMResponse<VMMBiodataAddress> response = await Task.Run(() => tabAlamat.GetById(id));
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
                Console.WriteLine("TabAlamatController.GetById: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{id?}")]
        public async Task<ActionResult> GetBioAddressByBioId(long? id)
        {
            try
            {
                VMResponse<List<VMMBiodataAddress>> response = await Task.Run(() => tabAlamat.GetBioAddressByBioId(id));
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
                Console.WriteLine("TabAlamatController.GetBioAddressByBioId: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{filter?}")]
        public async Task<ActionResult> GetByFilter(string? filter)
        {
            try
            {
                VMResponse<List<VMMBiodataAddress>> response = await Task.Run(() => tabAlamat.GetByFilter(filter));
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
                Console.WriteLine("TabAlamatController.GetByFilter: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]/{label?}")]
        public async Task<ActionResult> GetByLabel(string? label)
        {
            try
            {
                VMResponse<List<VMMBiodataAddress>> response = await Task.Run(() => tabAlamat.GetByLabel(label));
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
                Console.WriteLine("TabAlamatController.GetByLabel: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Create(VMMBiodataAddress data)
        {
            try
            {
                return Created("api/TabAlamat", await Task.Run(() => tabAlamat.Create(data)));
            }
            catch (Exception e)
            {
                //Console Logging
                Console.WriteLine("TabAalamatController.Create: " + e.Message);

                return BadRequest(e.Message);
            }
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> Update(VMMBiodataAddress data)
        {
            try
            {
                return Ok(await Task.Run(() => tabAlamat.Update(data)));
            }
            catch (Exception e)
            {
                //Console Logging
                Console.WriteLine("TabAlamatController.Update: " + e.Message);

                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id?}/{userId?}")]
        public async Task<ActionResult> Delete(int id, int userId)
        {
            try
            {
                return Ok(await Task.Run(() => tabAlamat.Delete(id, userId)));
            }
            catch (Exception e)
            {
                Console.WriteLine("TabAlamatController.Delete: " + e.Message);
                return BadRequest(e.Message);
            }
        }

        [HttpPut("[action]")]
        public async Task<ActionResult> MultipleDelete(VMMBiodataAddress.MultipleDeleteRequest deleteRequest)
        {
            try
            {
                // Validasi permintaan
                if (deleteRequest.Ids == null || deleteRequest.Ids.Count == 0)
                {
                    return BadRequest("Invalid request. Please provide at least one ID.");
                }

                if (deleteRequest.UserId <= 0)
                {
                    return BadRequest("Invalid User ID.");
                }

                // Memanggil metode MultipleDelete di tabAlamat
                VMResponse<VMMBiodataAddress> response = await Task.Run(() => tabAlamat.MultipleDelete(deleteRequest.Ids, deleteRequest.UserId));

                // Mengembalikan hasil berdasarkan status kode
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return Ok(response);
                }
                else
                {
                    return StatusCode((int)response.StatusCode, response.Message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("TabAlamatController.MultipleDelete: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("[action]")]
        public async Task<ActionResult> GetAllLocation()
        {
            try
            {
                VMResponse<List<VMMLocation>> response = await Task.Run(() => tabAlamat.GetAllLocation());
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
                Console.WriteLine("TabAlamatController.GetAllLocation: " + ex.Message);
                return BadRequest(ex.Message);
            }
        }
    }
}
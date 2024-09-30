using HealthCare340B.DataAccess;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace HealthCare340B.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorOfficeController : ControllerBase
    {
        private DADoctorOffice dof;

        public DoctorOfficeController(HealthCare340BContext _db)
        {
            dof = new DADoctorOffice(_db);
        }

        [HttpGet("[action]/{docId?}/{medFacId?}")]
        public async Task<ActionResult> GetByDoctorIdAndMedFacId(long? docId, long? medFacId)
        {
            try
            {
                if (docId == null || medFacId == null)
                    throw new ArgumentNullException();
                VMResponse<VMTDoctorOffice?> response = await Task.Run(() => dof.GetByDoctorIdAndMedFacId((long)docId, (long)medFacId));
                if (response.Data != null)
                {
                    return Ok(response);
                }
                else
                {
                    return NoContent();
                }

            }
            catch (Exception e)
            {
                throw new Exception($"{HttpStatusCode.BadRequest} - {e.Message}");
            }   
        }
    }
}

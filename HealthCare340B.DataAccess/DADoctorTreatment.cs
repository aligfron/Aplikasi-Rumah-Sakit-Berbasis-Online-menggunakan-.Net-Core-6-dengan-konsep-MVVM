using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare340B.DataAccess
{
    public class DADoctorTreatment
    {
        private readonly HealthCare340BContext db;

        public DADoctorTreatment(HealthCare340BContext _db)
        {
            db = _db;
        }

        public VMResponse<List<VMTDoctorTreatment>?> GetAll()
        {
            VMResponse<List<VMTDoctorTreatment>?> response = new VMResponse<List<VMTDoctorTreatment>?>();
            try
            {
                var query = from m in db.TDoctorTreatments
                            select new VMTDoctorTreatment
                            {
                                Id = m.Id,
                                Name = m.Name,
                                CreatedBy = m.CreatedBy,
                                CreatedOn = m.CreatedOn,
                                ModifiedBy = m.ModifiedBy,
                                ModifiedOn = m.ModifiedOn,
                                DeletedBy = m.DeletedBy,
                                DeletedOn = m.DeletedOn,
                                IsDelete = m.IsDelete,
                            };
                var result = query.ToList();
                Console.WriteLine($"Query returned {result.Count} treatment");

                response.Data = result;
                response.Message = (response.Data.Count > 0)
                    ? $"{HttpStatusCode.OK} - {response.Data.Count} treatment(s) successfully fetched"
                    : $"{HttpStatusCode.NoContent} - No treatment is found";
                response.StatusCode = (response.Data.Count > 0)
                    ? HttpStatusCode.OK
                    : HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                response.StatusCode = HttpStatusCode.InternalServerError;
            }
            return response;
        }
    }
}

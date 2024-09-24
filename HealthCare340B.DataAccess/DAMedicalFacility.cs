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
    public class DAMedicalFacility
    {
        private readonly HealthCare340BContext db;

        public DAMedicalFacility(HealthCare340BContext _db)
        {
            db = _db;
        }

        public VMResponse<List<VMMMedicalFacility>?> GetAll()
        {
            VMResponse<List<VMMMedicalFacility>?> response = new VMResponse<List<VMMMedicalFacility>?>();
            try
            {
                var query = from m in db.MMedicalFacilities
                            join f in db.MMedicalFacilityCategories on m.MedicalFacilityCategoryId equals f.Id
                            select new VMMMedicalFacility
                            {
                                Id = m.Id,
                                Name = m.Name,
                                MedicalFacilityCategoryId = m.MedicalFacilityCategoryId,
                                LocationId = m.LocationId,
                                FullAddress = m.FullAddress,
                                Email = m.Email,
                                PhoneCode = m.PhoneCode,
                                Phone = m.Phone,
                                Fax = m.Fax,
                                CreatedBy = m.CreatedBy,
                                CreatedOn = m.CreatedOn,
                                ModifiedBy = m.ModifiedBy,
                                ModifiedOn = m.ModifiedOn,
                                DeletedBy = m.DeletedBy,
                                DeletedOn = m.DeletedOn,
                                IsDelete = m.IsDelete,
                            };
                var result = query.ToList();
                Console.WriteLine($"Query returned {result.Count} medical facilities");

                response.Data = result;
                response.Message = (response.Data.Count > 0)
                    ? $"{HttpStatusCode.OK} - {response.Data.Count} medical facility(s) successfully fetched"
                    : $"{HttpStatusCode.NoContent} - No medical facility is found";
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

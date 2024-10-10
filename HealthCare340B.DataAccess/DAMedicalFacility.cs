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
                            join l in db.MLocations on m.LocationId equals l.Id
                            select new VMMMedicalFacility
                            {
                                Id = m.Id,
                                Name = m.Name,
                                MedicalFacilityCategoryId = m.MedicalFacilityCategoryId,
                                LocationId = m.LocationId,
                                FullAddress = m.FullAddress,
                                LocationName = l.Name,
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

        public VMResponse<List<VMMMedicalFacility>?> GetByDoctorId(long idDoctor)
        {
            VMResponse<List<VMMMedicalFacility>?> response = new VMResponse<List<VMMMedicalFacility>?>();

            try
            {
                response.Data = (
                    from mf in db.MMedicalFacilities
                    join dof in db.TDoctorOffices on mf.Id equals dof.MedicalFacilityId
                    where mf.IsDelete == false && dof.DoctorId == idDoctor && dof.EndDate > DateTime.Now
                    select new VMMMedicalFacility
                    {
                        Id = mf.Id,
                        Name = mf.Name,
                        MedicalFacilityCategoryId = mf.MedicalFacilityCategoryId,
                        LocationId = mf.LocationId,
                        FullAddress = mf.FullAddress,
                        Email = mf.Email,
                        PhoneCode = mf.PhoneCode,
                        Phone = mf.Phone,
                        Fax = mf.Fax,
                        CreatedBy = mf.CreatedBy,
                        CreatedOn = mf.CreatedOn,
                        ModifiedBy = mf.ModifiedBy,
                        ModifiedOn = mf.ModifiedOn,
                        DeletedBy = mf.DeletedBy,
                        DeletedOn = mf.DeletedOn,
                        IsDelete = mf.IsDelete
                    }
                    ).ToList();

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

        public VMResponse<List<VMMLocation>?> GetAllLocation()
        {
            VMResponse<List<VMMLocation>?> response = new VMResponse<List<VMMLocation>?>();
            try
            {
                var query = from m in db.MLocations
                            join l in db.MMedicalFacilities on m.Id equals l.LocationId
                            where m.IsDelete == false
                            select new VMMLocation
                            {
                                Id = m.Id,
                                Name = m.Name
                            };
                var result = query.Distinct().ToList();
                Console.WriteLine($"Query returned {result.Count} location");

                response.Data = result;
                response.Message = (response.Data.Count > 0)
                    ? $"{HttpStatusCode.OK} - {response.Data.Count} location(s) successfully fetched"
                    : $"{HttpStatusCode.NoContent} - No location is found";
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

        public VMResponse<VMMLocation?> GetLocationById(long id)
        {
            VMResponse<VMMLocation?> response = new VMResponse<VMMLocation?>();

            try
            {
                response.Data = (
                    from m in db.MLocations
                    join l in db.MMedicalFacilities on m.Id equals l.LocationId
                    where m.IsDelete == false   
                    select new VMMLocation
                    {
                        Id = m.Id,
                        Name = m.Name
                    }
                    ).FirstOrDefault();

                response.Message = (response.StatusCode == HttpStatusCode.OK)
                   ? $"{HttpStatusCode.OK} -  medical facility(s) successfully fetched"
                   : $"{HttpStatusCode.NoContent} - No medical facility is found";

                response.StatusCode = (response.StatusCode == HttpStatusCode.OK)
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

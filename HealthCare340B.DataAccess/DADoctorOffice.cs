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
    public class DADoctorOffice
    {
        private readonly HealthCare340BContext db;

        public DADoctorOffice (HealthCare340BContext _db)
        {
            db = _db;
        }

        public VMResponse<VMTDoctorOffice?> GetByDoctorIdAndMedFacId(long docId, long medFacId)
        {
            VMResponse<VMTDoctorOffice?> response = new VMResponse<VMTDoctorOffice?>();

            try
            {
                response.Data = (
                    from dof in db.TDoctorOffices
                    where dof.IsDelete == false && dof.DoctorId == docId && dof.MedicalFacilityId == medFacId
                    select new VMTDoctorOffice
                    {
                        Id = dof.Id,
                        DoctorId = dof.DoctorId,
                        MedicalFacilityId = medFacId,
                        Specialization = dof.Specialization,
                        StartDate = dof.StartDate,
                        EndDate = dof.EndDate,
                        ServiceUnitId = dof.ServiceUnitId,
                        CreatedBy = dof.CreatedBy,
                        CreatedOn = dof.CreatedOn,
                        ModifiedBy = dof.ModifiedBy,
                        ModifiedOn = dof.ModifiedOn,
                        DeletedBy = dof.DeletedBy,
                        DeletedOn = dof.DeletedOn,
                        IsDelete = dof.IsDelete
                    }
                    ).FirstOrDefault();

                response.Message = (response.Data != null)
                    ? $"{HttpStatusCode.OK} - Doctor Office is found!"
                    : $"{HttpStatusCode.NoContent} - No Doctor Office is found";

                response.StatusCode = (response.Data != null)
                    ? HttpStatusCode.OK
                    : HttpStatusCode.NoContent;
            }
            catch (Exception e)
            {
                response.Message = $"{HttpStatusCode.InternalServerError} - {e.Message}";
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }
    }
}

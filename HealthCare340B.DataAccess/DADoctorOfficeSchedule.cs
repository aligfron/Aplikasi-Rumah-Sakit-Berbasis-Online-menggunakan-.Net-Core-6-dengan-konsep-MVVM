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
    public class DADoctorOfficeSchedule
    {
        private readonly HealthCare340BContext db;

        public DADoctorOfficeSchedule(HealthCare340BContext _db)
        {
            db = _db;
        }

        public VMResponse<VMTDoctorOfficeSchedule?> GetByUserChoice(long doctorId, long medFacId, string day, string timeStart)
        {
            VMResponse<VMTDoctorOfficeSchedule?> response = new VMResponse<VMTDoctorOfficeSchedule?>();

            try
            {
                response.Data = (
                    from dof in db.TDoctorOfficeSchedules
                    join mfs in db.MMedicalFacilitySchedules on dof.MedicalFacilityScheduleId equals mfs.Id
                    where dof.IsDelete == false && dof.DoctorId == doctorId && mfs.MedicalFacilityId == medFacId && mfs.Day == day && mfs.TimeScheduleStart == timeStart
                    select new VMTDoctorOfficeSchedule
                    {
                        Id = dof.Id,
                        DoctorId = dof.DoctorId,
                        MedicalFacilityScheduleId = dof.Id,
                        Slot = dof.Slot,
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
                   ? $"{HttpStatusCode.OK} - Doctor Office Schedule is found!"
                   : $"{HttpStatusCode.NoContent} - No Doctor Office Schedule is found";

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

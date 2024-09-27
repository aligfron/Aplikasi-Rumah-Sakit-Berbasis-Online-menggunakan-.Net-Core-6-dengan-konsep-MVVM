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
    public class DAMedicalFacilitySchedule
    {
        private readonly HealthCare340BContext db;

        public DAMedicalFacilitySchedule(HealthCare340BContext _db)
        {
            db = _db;
        }

        public VMResponse<List<VMMMedicalFacilitySchedule>?> GetByMedicalFacilityIdAndDoctorId(long mfId, long doctorId)
        {
            VMResponse<List<VMMMedicalFacilitySchedule>?> response = new VMResponse<List<VMMMedicalFacilitySchedule>?>();

            try
            {
                response.Data = (
                    from mfs in db.MMedicalFacilitySchedules
                    join dos in db.TDoctorOfficeSchedules on mfs.Id equals dos.MedicalFacilityScheduleId
                    where mfs.IsDelete == false && dos.DoctorId == doctorId && mfs.MedicalFacilityId == mfId && dos.Slot > 0
                    select new VMMMedicalFacilitySchedule
                    {
                        Id = mfs.Id,
                        MedicalFacilityId = mfs.Id,
                        Day = mfs.Day,
                        TimeScheduleStart = mfs.TimeScheduleStart,
                        TimeScheduleEnd = mfs.TimeScheduleEnd,
                        CreatedBy = mfs.CreatedBy,
                        CreatedOn = mfs.CreatedOn,
                        ModifiedBy = mfs.ModifiedBy,
                        ModifiedOn = mfs.ModifiedOn,
                        DeletedBy = mfs.DeletedBy,
                        DeletedOn = mfs.DeletedOn,
                        IsDelete = mfs.IsDelete
                    }
                    ).ToList();

                response.Message = (response.Data.Count > 0)
                   ? $"{HttpStatusCode.OK} - {response.Data.Count} schedule(s) successfully fetched"
                   : $"{HttpStatusCode.NoContent} - No schedule is found";
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

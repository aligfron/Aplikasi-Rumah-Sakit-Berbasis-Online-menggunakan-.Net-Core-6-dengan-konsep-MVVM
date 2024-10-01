using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.EntityFrameworkCore.Storage;

namespace HealthCare340B.DataAccess
{
    public class DAPrescription
    {
        private readonly HealthCare340BContext _db;

        public DAPrescription(HealthCare340BContext db)
        {
            _db = db;
        }

        public VMResponse<List<VMTPrescription>> GetByAppointmentId(long appointmentId)
        {
            VMResponse<List<VMTPrescription>> response = new VMResponse<List<VMTPrescription>>();

            try
            {
                if (appointmentId > 0)
                {
                    response.Data = (
                        from p in _db.TPrescriptions
                        join mi in _db.MMedicalItems on p.MedicalItemId equals mi.Id
                        where
                            p.AppointmentId == appointmentId
                            && p.IsDelete == false
                            && mi.IsDelete == false
                        select new VMTPrescription
                        {
                            Id = p.Id,
                            AppointmentId = p.AppointmentId,
                            MedicalItemId = p.MedicalItemId,
                            MedicalItemName = mi.Name,
                            Dosage = p.Dosage,
                            Directions = p.Directions,
                            Time = p.Time,
                            Notes = p.Notes,
                            PrintedOn = p.PrintedOn,
                            PrintAttempt = p.PrintAttempt,
                            CreatedBy = p.CreatedBy,
                            CreatedOn = p.CreatedOn,
                            ModifiedBy = p.ModifiedBy,
                            ModifiedOn = p.ModifiedOn,
                            DeletedBy = p.DeletedBy,
                            DeletedOn = p.DeletedOn,
                            IsDelete = p.IsDelete,
                        }
                    ).ToList();

                    response.Message =
                        (response.Data != null)
                            ? $"{HttpStatusCode.OK} - Prescription data successfully fetched"
                            : $"{HttpStatusCode.NoContent} - No prescription data found";
                    response.StatusCode =
                        (response.Data != null) ? HttpStatusCode.OK : HttpStatusCode.NoContent;
                }
                else
                {
                    response.Message = $"{HttpStatusCode.BadRequest} - Id is required";
                    response.StatusCode = HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
            }

            return response;
        }
    }
}

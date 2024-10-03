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

        public VMResponse<List<VMTPrescription>> UpdatePrintAttempt(List<VMTPrescription> model)
        {
            VMResponse<List<VMTPrescription>> response = new VMResponse<List<VMTPrescription>>();

            using (IDbContextTransaction dbTran = _db.Database.BeginTransaction())
            {
                try
                {
                    if (model != null)
                    {
                        foreach (VMTPrescription prescription in model)
                        {
                            TPrescription tprescription = _db.TPrescriptions.Find(prescription.Id);

                            if (DateTime.Now.Date < prescription.CreatedOn.Date.AddDays(2))
                            {
                                if (prescription.PrintAttempt == null || prescription.PrintAttempt < 2)
                                {
                                    if (tprescription.PrintAttempt == null)
                                    {
                                        tprescription.PrintAttempt = 1;
                                    }
                                    else
                                    {
                                        tprescription.PrintAttempt = tprescription.PrintAttempt + 1;
                                    }

                                    tprescription.PrintedOn = DateTime.Now;
                                    tprescription.ModifiedBy = prescription.ModifiedBy;
                                    tprescription.ModifiedOn = DateTime.Now;

                                    _db.Update(tprescription);
                                    _db.SaveChanges();
                                }
                                else
                                {
                                    throw new Exception("Prescription print attempt has reached the limit");
                                }
                            }
                            else
                            {
                                throw new Exception("Prescription is no longer valid after 2 days from creation");
                            }
                            
                        }

                        dbTran.Commit();

                        //Yang melakukan printattemp dipastikan memiliki setidaknya 1 obat
                        response.Data = GetByAppointmentId(model[0].AppointmentId ?? 0).Data;
                        response.Message = $"{HttpStatusCode.OK} - Prescription print attempt successfully updated";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                }

                return response;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;

namespace HealthCare340B.DataAccess
{
    public class DAAppointmentDone
    {
        private readonly HealthCare340BContext _db;

        public DAAppointmentDone(HealthCare340BContext db)
        {
            _db = db;
        }

        public VMResponse<List<VMTAppointmentDone>> GetByFilter(string filter, long parentBiodataId)
        {
            VMResponse<List<VMTAppointmentDone>> response =
                new VMResponse<List<VMTAppointmentDone>>();

            try
            {
                List<long> customerIds = (
                    from cm in _db.MCustomerMembers
                    join c in _db.MCustomers on cm.CustomerId equals c.Id
                    join cr in _db.MCustomerRelations on cm.CustomerRelationId equals cr.Id
                    where
                        cm.IsDelete == false
                        && c.IsDelete == false
                        && cr.IsDelete == false
                        && cm.ParentBiodataId == parentBiodataId
                    select c.Id
                ).ToList();

                List<VMTAppointmentDone> appointmentDones = (
                    from ad in _db.TAppointmentDones
                    join a in _db.TAppointments on ad.AppointmentId equals a.Id
                    where
                        ad.IsDelete == false
                        && a.IsDelete == false
                        && customerIds.Contains(a.CustomerId!.Value)
                    select new VMTAppointmentDone
                    {
                        Id = ad.Id,
                        AppointmentId = ad.AppointmentId,
                        CustomerId = a.CustomerId,
                        AppointmentDate = a.AppointmentDate,
                        DoctorOfficeId = a.DoctorOfficeId,
                        Diagnosis = ad.Diagnosis,
                        CreatedOn = ad.CreatedOn,
                        ModifiedBy = ad.ModifiedBy,
                        ModifiedOn = ad.ModifiedOn,
                        DeletedBy = ad.DeletedBy,
                        DeletedOn = ad.DeletedOn,
                        IsDelete = ad.IsDelete,
                    }
                ).ToList();

                foreach (VMTAppointmentDone appointmentDone in appointmentDones)
                {
                    appointmentDone.CustomerFullname = (
                        from cm in _db.MCustomerMembers
                        join c in _db.MCustomers on cm.CustomerId equals c.Id
                        join b in _db.MBiodata on c.BiodataId equals b.Id
                        where
                            cm.IsDelete == false
                            && c.IsDelete == false
                            && b.IsDelete == false
                            && c.Id == appointmentDone.CustomerId
                        select b.Fullname
                    ).FirstOrDefault();

                    appointmentDone.MedicalFacilityName = (
                        from a in _db.TAppointments
                        join d in _db.TDoctorOffices on a.DoctorOfficeId equals d.Id
                        join mf in _db.MMedicalFacilities on d.MedicalFacilityId equals mf.Id
                        where
                            a.IsDelete == false
                            && d.IsDelete == false
                            && mf.IsDelete == false
                            && a.Id == appointmentDone.AppointmentId
                        select mf.Name
                    ).FirstOrDefault();

                    appointmentDone.DoctorFullname = (
                        from a in _db.TAppointments
                        join dof in _db.TDoctorOffices on a.DoctorOfficeId equals dof.Id
                        join d in _db.MDoctors on dof.DoctorId equals d.Id
                        join b in _db.MBiodata on d.BiodataId equals b.Id
                        where
                            a.IsDelete == false
                            && dof.IsDelete == false
                            && d.IsDelete == false
                            && b.IsDelete == false
                            && a.Id == appointmentDone.AppointmentId
                        select b.Fullname
                    ).FirstOrDefault();

                    appointmentDone.DoctorTreatmentName = (
                        from a in _db.TAppointments
                        join dof in _db.TDoctorOffices on a.DoctorOfficeId equals dof.Id
                        join dot in _db.TDoctorOfficeTreatments
                            on dof.Id equals dot.DoctorTreatmentId
                        join dt in _db.TDoctorTreatments on dot.DoctorTreatmentId equals dt.Id
                        where
                            a.IsDelete == false
                            && dof.IsDelete == false
                            && dot.IsDelete == false
                            && dt.IsDelete == false
                            && a.Id == appointmentDone.AppointmentId
                        select dt.Name
                    ).FirstOrDefault();

                    appointmentDone.Prescriptions = (
                        from p in _db.TPrescriptions
                        join mi in _db.MMedicalItems on p.MedicalItemId equals mi.Id
                        where
                            p.IsDelete == false
                            && mi.IsDelete == false
                            && p.AppointmentId == appointmentDone.AppointmentId
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
                }

                appointmentDones = (
                    from a in appointmentDones
                    where
                        (
                            a.CustomerFullname.ToLower().Contains(filter.ToLower())
                            || a.DoctorFullname.ToLower().Contains(filter.ToLower())
                        )
                        && a.AppointmentDate < DateTime.Now
                    select a
                ).ToList();

                response.Data = appointmentDones;
                response.Message =
                    (response.Data.Count > 0)
                        ? $"{HttpStatusCode.OK} - {response.Data.Count} appointment data(s) successfully fetched"
                        : $"{HttpStatusCode.NoContent} - No appointment found";

                response.StatusCode =
                    (response.Data.Count > 0) ? HttpStatusCode.OK : HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
            }

            return response;
        }
    }
}

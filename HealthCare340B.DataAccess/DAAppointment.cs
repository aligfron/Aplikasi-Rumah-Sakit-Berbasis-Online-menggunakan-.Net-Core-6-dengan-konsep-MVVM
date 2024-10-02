using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HealthCare340B.DataAccess
{
    public class DAAppointment
    {
        private readonly HealthCare340BContext db;

        public DAAppointment(HealthCare340BContext _db)
        {
            db = _db;
        }

        public VMResponse<List<VMTAppointmentDone>?> GetAppointmentDone(List<VMTAppointment> dataApp)
        {
            VMResponse<List<VMTAppointmentDone>?> response = new VMResponse<List<VMTAppointmentDone>?>();

            response.Data = new List<VMTAppointmentDone>();

            try
            {
                foreach (VMTAppointment app in dataApp)
                {
                    VMTAppointmentDone? data = (
                        from ad in db.TAppointmentDones
                        where ad.AppointmentId == app.Id
                        select new VMTAppointmentDone
                        {
                            Id = ad.Id,
                            AppointmentId = ad.Id,
                            Diagnosis = ad.Diagnosis,
                            CreatedBy = ad.CreatedBy,
                            CreatedOn = ad.CreatedOn,
                            ModifiedBy = ad.ModifiedBy,
                            ModifiedOn = ad.ModifiedOn,
                            DeletedBy = ad.DeletedBy,
                            DeletedOn = ad.DeletedOn,
                            IsDelete = ad.IsDelete
                        }
                        ).FirstOrDefault();

                    if (data != null)
                    {
                        response.Data.Add(data);
                    }
                }

                if (response.Data != null)
                {
                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = $"{HttpStatusCode.OK} - Appointments successfully fetch!";
                }
                else
                {
                    response.StatusCode = HttpStatusCode.NoContent;
                    response.Message = $"{HttpStatusCode.NoContent} - No Appointments Found!";
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Something's wrong - {e.Message}");
            }
            return response;
        }

        public VMResponse<List<VMTAppointment>?> GetByCustomerId(List<long> custId)
        {
            VMResponse<List<VMTAppointment>?> response = new VMResponse<List<VMTAppointment>?>();

            response.Data = new List<VMTAppointment>();

            try
            {
                foreach (long id in custId)
                {
                    List<VMTAppointment>? data = (
                        from a in db.TAppointments
                        join c in db.MCustomers on a.CustomerId equals c.Id
                        join b in db.MBiodata on c.BiodataId equals b.Id
                        join dof in db.TDoctorOffices on a.DoctorOfficeId equals dof.Id
                        join doc in db.MDoctors on dof.DoctorId equals doc.Id
                        join mf in db.MMedicalFacilities on dof.MedicalFacilityId equals mf.Id
                        join dot in db.TDoctorOfficeTreatments on a.DoctorOfficeTreatmentId equals dot.Id
                        join dt in db.TDoctorTreatments on dot.DoctorTreatmentId equals dt.Id
                        where a.CustomerId == id && a.IsDelete == false && a.AppointmentDate > DateTime.Now
                        select new VMTAppointment
                        {
                            Id = a.Id,
                            CustomerId = a.CustomerId,
                            DoctorOfficeId = a.DoctorOfficeId,
                            DoctorOfficeScheduleId = a.DoctorOfficeScheduleId,
                            DoctorOfficeTreatmentId = a.DoctorOfficeTreatmentId,
                            AppointmentDate = a.AppointmentDate,
                            CustomerName = b.Fullname,
                            DoctorId = doc.Id,
                            MedicalFacilityId = mf.Id,
                            MedicalFacilityName = mf.Name,
                            Treatment = dt.Name,
                            CreatedBy = a.CreatedBy,
                            CreatedOn = a.CreatedOn,
                            ModifiedBy = a.ModifiedBy,
                            ModifiedOn = a.ModifiedOn,
                            DeletedBy = a.DeletedBy,
                            DeletedOn = a.DeletedOn,
                            IsDelete = a.IsDelete
                        }
                        ).ToList();
                        
                    if (data != null && data.Count > 0)
                    {
                        foreach (VMTAppointment resQuery in data)
                        {
                            response.Data.Add(resQuery);
                        }
                    }
                }

                if (response.Data.Count > 0)
                {
                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = $"{HttpStatusCode.OK} - Appointments successfully fetch!";
                }
                else
                {
                    response.StatusCode = HttpStatusCode.NoContent;
                    response.Message = $"{HttpStatusCode.NoContent} - No Appointments Found!";
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Something's wrong - {e.Message}");
            }

            return response;
        }

        public List<DateTime>? GetEmptySlotDate(List<VMMMedicalFacilitySchedule> data)
        {
            List<DateTime> excludedDate = new List<DateTime>();
            List<VMAppointmentSchedule> appSchedule = new List<VMAppointmentSchedule>();
            try
            {              
                foreach (VMMMedicalFacilitySchedule sch in data)
                {
                    List<VMTAppointment>? appointment = (
                        from ap in db.TAppointments
                        where ap.IsDelete == false && ap.DoctorOfficeScheduleId == sch.DoctorOfficeScheduleId
                        select new VMTAppointment
                        {
                            Id = ap.Id,
                            DoctorOfficeId = ap.DoctorOfficeId,
                            DoctorOfficeScheduleId = ap.DoctorOfficeScheduleId,
                            DoctorOfficeTreatmentId = ap.DoctorOfficeTreatmentId,
                            AppointmentDate = ap.AppointmentDate,
                            CreatedBy = ap.CreatedBy,
                            CreatedOn = ap.CreatedOn,
                            ModifiedBy = ap.ModifiedBy,
                            ModifiedOn = ap.ModifiedOn,
                            DeletedBy = ap.DeletedBy,
                            DeletedOn = ap.DeletedOn,
                            IsDelete = ap.IsDelete
                        }
                    ).ToList();

                    List<DateTime> dateSch = new List<DateTime>();

                    foreach (VMTAppointment date in appointment)
                    {
                        dateSch.Add((DateTime)date.AppointmentDate!);
                    }

                    dateSch.Sort();

                    DateTime? compare = null;
                    int count = 0;
                    foreach (DateTime date in dateSch)
                    {
                        if (compare == null)
                        {
                            compare = date;
                            count++;
                        }
                        else if (compare == date && count++ < sch.Slot)
                            count++;
                        else
                        {
                            if (count >= sch.Slot)
                                excludedDate.Add((DateTime)(compare + 
                                    new TimeSpan(DateTime.Parse(sch.TimeScheduleStart!).Hour, 
                                    DateTime.Parse(sch.TimeScheduleStart!).Minute, 0)));
                            count = 1;
                            compare = date;
                        }
                    }

                    if (count >= sch.Slot)
                        excludedDate.Add((DateTime)(compare +
                        new TimeSpan(DateTime.Parse(sch.TimeScheduleStart!).Hour,
                        DateTime.Parse(sch.TimeScheduleStart!).Minute, 0))!);


                }
            }
            catch (Exception e)
            {
                throw new Exception($"Something's wrong - {e.Message}");
            }
            return excludedDate;
        }

        public VMResponse<List<VMTAppointment>?> GetByDateAndDoctorOfficeScheduleId(DateTime appDate, long doctorOfficeScheduleId)
        {
            VMResponse<List<VMTAppointment>?> response = new VMResponse<List<VMTAppointment>?>();

            try
            {
                response.Data = (
                    from ap in db.TAppointments
                    where ap.IsDelete == false && ap.AppointmentDate == appDate && ap.DoctorOfficeScheduleId == doctorOfficeScheduleId
                    select new VMTAppointment
                    {
                        Id = ap.Id,
                        DoctorOfficeId = ap.DoctorOfficeId,
                        DoctorOfficeScheduleId = ap.DoctorOfficeScheduleId,
                        DoctorOfficeTreatmentId = ap.DoctorOfficeTreatmentId,
                        AppointmentDate = ap.AppointmentDate,
                        CreatedBy = ap.CreatedBy,
                        CreatedOn = ap.CreatedOn,
                        ModifiedBy = ap.ModifiedBy,
                        ModifiedOn = ap.ModifiedOn,
                        DeletedBy = ap.DeletedBy,
                        DeletedOn = ap.DeletedOn,
                        IsDelete = ap.IsDelete
                    }
                ).ToList();

                if (response.Data != null)
                {
                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = $"{HttpStatusCode.OK} - Appointment successfully fetch!";
                }
                else
                {
                    response.StatusCode = HttpStatusCode.NoContent;
                    response.Message = $"{HttpStatusCode.NoContent} - Appointment does not exist!";
                }
            }
            catch (Exception e)
            {
                response.Message = $"{HttpStatusCode.InternalServerError} - {e.Message}";
            }

            return response;
        }

        public VMResponse<VMTAppointment?> GetByDate(DateTime appDate, long id)
        {
            VMResponse<VMTAppointment?> response = new VMResponse<VMTAppointment?>();

            try
            {
                response.Data = (
                    from ap in db.TAppointments
                    where ap.IsDelete == false && ap.AppointmentDate == appDate && ap.CustomerId == id
                    select new VMTAppointment
                    {
                        Id = ap.Id,
                        DoctorOfficeId = ap.DoctorOfficeId,
                        DoctorOfficeScheduleId = ap.DoctorOfficeScheduleId,
                        DoctorOfficeTreatmentId = ap.DoctorOfficeTreatmentId,
                        AppointmentDate = ap.AppointmentDate,
                        CreatedBy = ap.CreatedBy,
                        CreatedOn = ap.CreatedOn,
                        ModifiedBy = ap.ModifiedBy,
                        ModifiedOn = ap.ModifiedOn,
                        DeletedBy = ap.DeletedBy,
                        DeletedOn = ap.DeletedOn,
                        IsDelete = ap.IsDelete
                    }
                ).FirstOrDefault();

                if (response.Data != null)
                {
                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = $"{HttpStatusCode.OK} - Appointment successfully fetch!";
                }
                else
                {
                    response.StatusCode = HttpStatusCode.NoContent;
                    response.Message = $"{HttpStatusCode.NoContent} - Appointment does not exist!";
                }
            }
            catch (Exception e)
            {
                response.Message = $"{HttpStatusCode.InternalServerError} - {e.Message}";
            }

            return response;
        }

        public bool IsAppDateMatchWithOfficeSchedule(string day, string time, long doctorOfficeScheduleId)
        {
            try
            {
                string? dayCheck = (
                     from dos in db.TDoctorOfficeSchedules
                     join mfs in db.MMedicalFacilitySchedules on dos.MedicalFacilityScheduleId equals mfs.Id
                     where dos.IsDelete == false && dos.Id == doctorOfficeScheduleId && mfs.Day == day
                     select mfs.Day
                    ).FirstOrDefault();

                if (string.IsNullOrEmpty(dayCheck))
                    throw new ArgumentNullException();

                string? timeCheck = (
                     from dos in db.TDoctorOfficeSchedules
                     join mfs in db.MMedicalFacilitySchedules on dos.MedicalFacilityScheduleId equals mfs.Id
                     where dos.IsDelete == false && dos.Id == doctorOfficeScheduleId && mfs.TimeScheduleStart == time
                     select mfs.TimeScheduleStart
                    ).FirstOrDefault();

                if (string.IsNullOrEmpty(timeCheck))
                    throw new ArgumentNullException();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public int? Slot(long doctorOfficeScheduleId)
        {
            int? slot;
            try
            {
                slot = (
                    from dos in db.TDoctorOfficeSchedules
                    where dos.IsDelete == false && dos.Id == doctorOfficeScheduleId
                    select dos.Slot
                    ).FirstOrDefault();

                if (slot == null)
                    throw new ArgumentNullException();
            }
            catch
            {
                slot = null;
            }

            return slot;
        }

        public bool UpdateSlot(long doctorOfficeScheduleId)
        {
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMTDoctorOfficeSchedule? existingData = (
                        from dos in db.TDoctorOfficeSchedules
                        where dos.IsDelete == false && dos.Id == doctorOfficeScheduleId
                        select new VMTDoctorOfficeSchedule
                        {
                            Id = dos.Id,
                            DoctorId = dos.Id,
                            MedicalFacilityScheduleId = dos.MedicalFacilityScheduleId,
                            Slot = dos.Slot,
                            CreatedBy = dos.CreatedBy,
                            CreatedOn = dos.CreatedOn,
                            ModifiedBy = dos.ModifiedBy,
                            ModifiedOn = dos.ModifiedOn,
                            DeletedBy = dos.DeletedBy,
                            DeletedOn = dos.DeletedOn,
                            IsDelete = dos.IsDelete
                        }
                    ).FirstOrDefault();

                    if (existingData == null)
                        throw new ArgumentNullException();
                    else
                    {
                        TDoctorOfficeSchedule updatedData = new TDoctorOfficeSchedule
                        {
                            Id = existingData.Id,
                            DoctorId = existingData.Id,
                            MedicalFacilityScheduleId = existingData.MedicalFacilityScheduleId,
                            Slot = existingData.Slot - 1,
                            CreatedBy = existingData.CreatedBy,
                            CreatedOn = existingData.CreatedOn,
                            ModifiedBy = existingData.ModifiedBy,
                            ModifiedOn = existingData.ModifiedOn,
                            DeletedBy = existingData.DeletedBy,
                            DeletedOn = existingData.DeletedOn,
                            IsDelete = existingData.IsDelete
                        };
                        
                        db.Update(updatedData);
                        db.SaveChanges();

                        dbTran.Commit();
                    }
                }
                catch (Exception e)
                {
                    dbTran.Rollback();
                    return false;
                }
                return true;
            }
        }

        public VMResponse<VMMCustomer> GetCustId(long bioId)
        {
            VMResponse<VMMCustomer> response = new VMResponse<VMMCustomer>();

            try
            {
                response.Data = (
                    from c in db.MCustomers
                    where c.IsDelete == false && c.BiodataId == bioId
                    select new VMMCustomer
                    {
                        Id = c.Id,
                        BiodataId = c.BiodataId,
                        Dob = c.Dob,
                        Gender = c.Gender,
                        BloodGroupId = c.BloodGroupId,
                        RhesusType = c.RhesusType,
                        Height = c.Height,
                        Weight = c.Weight,
                        CreatedBy = c.CreatedBy,
                        CreatedOn = c.CreatedOn,
                        ModifiedBy = c.ModifiedBy,
                        ModifiedOn = c.ModifiedOn,
                        DeletedBy = c.DeletedBy,
                        DeletedOn = c.DeletedOn,
                        IsDelete = c.IsDelete
                    }
                    ).FirstOrDefault();

                if (response.Data != null)
                {
                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = $"{HttpStatusCode.OK} - Customer successfully fetch!";
                }
                else
                {
                    response.StatusCode = HttpStatusCode.NoContent;
                    response.Message = $"{HttpStatusCode.NoContent} - Customer does not exist!";
                }

            }
            catch (Exception e)
            {
                response.Message = $"{HttpStatusCode.InternalServerError} - {e.Message}";
            }

            return response;
        }

        public VMResponse<VMTAppointment> Create(VMTAppointment data)
        {
            VMResponse<VMTAppointment> response = new VMResponse<VMTAppointment>();

            DateTime appDate = (DateTime)data.AppointmentDate!;
            string day = appDate.DayOfWeek.ToString();
            string time = appDate.ToString("HH:mm");

            if (!IsAppDateMatchWithOfficeSchedule(day, time, (long)data.DoctorOfficeScheduleId!))
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Message = $"{HttpStatusCode.BadRequest} - Appointment Date did not match with the schedule!";

                return response;
            }

            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    // Check if the customer has an appointment in the input date
                    if (GetByDate((DateTime)data.AppointmentDate!, (long)data.CustomerId!).Data == null)
                    {
                        // Check if there is any slot left for the input date and doctor office
                        if (GetByDateAndDoctorOfficeScheduleId((DateTime)data.AppointmentDate!, (long)data.DoctorOfficeScheduleId).Data == null ||
                            (GetByDateAndDoctorOfficeScheduleId((DateTime)data.AppointmentDate!, (long)data.DoctorOfficeScheduleId).Data != null &&
                            GetByDateAndDoctorOfficeScheduleId((DateTime)data.AppointmentDate!, (long)data.DoctorOfficeScheduleId).Data!.Count < 
                            Slot((long)data.DoctorOfficeScheduleId)))
                        {                    
                            TAppointment newData = new TAppointment();
                            newData.CustomerId = data.CustomerId;
                            newData.DoctorOfficeId = data.DoctorOfficeId;
                            newData.DoctorOfficeScheduleId = data.DoctorOfficeScheduleId;
                            newData.DoctorOfficeTreatmentId = data.DoctorOfficeTreatmentId;
                            newData.AppointmentDate = data.AppointmentDate;
                            newData.CreatedBy = data.CreatedBy;
                            newData.CreatedOn = DateTime.Now;

                            db.Add(newData);
                            db.SaveChanges();

                            dbTran.Commit();

                            response.Data = new VMTAppointment
                            {
                                Id = newData.Id,
                                CustomerId = newData.CustomerId,
                                DoctorOfficeId = newData.DoctorOfficeId,
                                DoctorOfficeScheduleId = newData.DoctorOfficeScheduleId,
                                DoctorOfficeTreatmentId = newData.DoctorOfficeTreatmentId,
                                CreatedBy = newData.CreatedBy,
                                CreatedOn = newData.CreatedOn,
                                ModifiedBy = newData.ModifiedBy,
                                ModifiedOn = newData.ModifiedOn,
                                DeletedBy = newData.DeletedBy,
                                DeletedOn = newData.DeletedOn,
                                IsDelete = newData.IsDelete,
                            };

                            response.StatusCode = HttpStatusCode.Created;
                            response.Message = $"{HttpStatusCode.Created} - New Appointment has been successfully created!";
                            
                        }
                        else
                        {
                            response.StatusCode = HttpStatusCode.Found;
                            response.Message = $"{HttpStatusCode.Found} - Slot is Full!";
                        }
                    }      
                    else
                    {
                        response.StatusCode = HttpStatusCode.Found;
                        response.Message = $"{HttpStatusCode.Found} - You already have other appointment in that time!";
                    }
                }
                catch (Exception e)
                {
                    dbTran.Rollback();
                    response.Message = $"{HttpStatusCode.InternalServerError} - {e.Message}";
                }
                return response;
            }

        }
    }
}

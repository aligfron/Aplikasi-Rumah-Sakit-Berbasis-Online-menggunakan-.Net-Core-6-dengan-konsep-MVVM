using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace HealthCare340B.DataAccess
{
    public class DADoctor
    {
        private readonly HealthCare340BContext db;

        public DADoctor(HealthCare340BContext _db)
        {
            db = _db;
        }

        public VMResponse<List<VMMDoctor>?> GetAll()
        {
            VMResponse<List<VMMDoctor>?> response = new VMResponse<List<VMMDoctor>?>();
            try
            {
                var query = from d in db.MDoctors
                            join b in db.MBiodata on d.BiodataId equals b.Id
                            join c in db.TCurrentDoctorSpecializations on d.Id equals c.DoctorId
                            into temp
                            from c in temp.DefaultIfEmpty()
                            join s in db.MSpecializations on c.SpecializationId equals s.Id
                            where d.IsDelete == false
                            select new VMMDoctor
                            {
                                Id = d.Id,
                                Fullname = b.Fullname,
                                SpecializationName = s.Name,
                                ImagePath = b.ImagePath,
                                DoctorOffice = (
                                  from o in db.TDoctorOffices
                                  join m in db.MMedicalFacilities
                                  on o.MedicalFacilityId equals m.Id
                                  where o.DoctorId == d.Id
                                  select new VMTDoctorOffice { 
                                      Id = o.Id,
                                      MedicalFacilityName = m.Name                       
                                  }
                                
                                ).ToList(),
                                Treatments = (
                                from t in db.TDoctorTreatments
                                where t.DoctorId == d.Id
                                select new VMTDoctorTreatment
                                {
                                    Id = t.Id,
                                    Name = t.Name
                                }                             
                                ).ToList(),


                            };

                var result = query.ToList();
                Console.WriteLine($"Query returned {result.Count} doctors");

                response.Data = result;
                response.Message = (response.Data.Count > 0)
                    ? $"{HttpStatusCode.OK} - {response.Data.Count} doctor(s) successfully fetched"
                    : $"{HttpStatusCode.NoContent} - No doctor is found";
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


        public VMResponse<List<VMMDoctor>?> GetByFilter(string? location, string? doctorName, string? specialization, string? treatment)
        {
            VMResponse<List<VMMDoctor>?> response = new VMResponse<List<VMMDoctor>?>();
            try
            {
               
                var query = (
                    from d in db.MDoctors
                    join b in db.MBiodata on d.BiodataId equals b.Id
                    join cds in db.TCurrentDoctorSpecializations on d.Id equals cds.DoctorId into HaveCurrSpecialization
                    from cds in HaveCurrSpecialization.DefaultIfEmpty()
                    join s in db.MSpecializations on cds.SpecializationId equals s.Id into HaveSpecialization
                    from s in HaveSpecialization.DefaultIfEmpty()
                    where !d.IsDelete && (string.IsNullOrEmpty(specialization) || s.Id == long.Parse(specialization))
                    && db.TDoctorOffices.Any(doff => doff.DoctorId == d.Id && !doff.IsDelete 
                    && (string.IsNullOrEmpty(location) || doff.MedicalFacility.LocationId == long.Parse(location)))
                    &&(string.IsNullOrEmpty(doctorName) || b.Fullname.Contains(doctorName))
                    && db.TDoctorTreatments.Any(dt => dt.DoctorId == d.Id && !dt.IsDelete 
                    && (string.IsNullOrEmpty(treatment) || dt.Name.Contains(treatment)))
                    select new VMMDoctor
                    {
                        BiodataId = b.Id,
                        Fullname = b.Fullname,
                        Id = d.Id,
                        Image = b.Image,
                        ImagePath = b.ImagePath,
                        SpecializationId = s.Id,
                        SpecializationName = s.Name,
                        DoctorOffice = (
                            from doff in db.TDoctorOffices
                            join mf in db.MMedicalFacilities on doff.MedicalFacilityId equals mf.Id
                            where !doff.IsDelete && doff.DoctorId == d.Id
                            select new VMTDoctorOffice
                            {
                                DoctorId = d.Id,
                                Id = doff.Id,
                                MedicalFacilityId = mf.Id,
                                MedicalFacilityName = mf.Name,
                                JadwalPraktek = (
                                    from dos in db.TDoctorOfficeSchedules
                                    join mfs in db.MMedicalFacilitySchedules on dos.MedicalFacilityScheduleId equals mfs.Id
                                    where !dos.IsDelete && (dos.DoctorId == d.Id && mfs.MedicalFacilityId == mf.Id)
                                    select new VMMMedicalFacilitySchedule
                                    {
                                        Day = mfs.Day,
                                        Id = dos.Id,
                                        TimeScheduleStart = mfs.TimeScheduleStart,
                                        TimeScheduleEnd = mfs.TimeScheduleEnd
                                    }
                                ).ToList()
                            }
                        ).ToList()
                    }
                    ).ToList();

                List<VMMDoctor> result = query.ToList();
                Console.WriteLine($"Query returned {result.Count} doctors");

                response.Data = result;
                response.Message = (response.Data.Count > 0)
                    ? $"{HttpStatusCode.OK} - {response.Data.Count} doctor(s) successfully fetched"
                    : $"{HttpStatusCode.NoContent} - No doctor is found";
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


        public VMResponse<VMMDoctor?> GetById(long id)
        {
            VMResponse<VMMDoctor?> response = new VMResponse<VMMDoctor?>();

            try
            {
                response.Data = (
                    from d in db.MDoctors
                    join b in db.MBiodata on d.BiodataId equals b.Id
                    where d.IsDelete == false && d.Id == id
                    select new VMMDoctor
                    {
                        Id = d.Id,
                        BiodataId = d.BiodataId,
                        Str = d.Str,
                        Fullname = b.Fullname,
                        ImagePath = b.ImagePath
                    }
                    ).FirstOrDefault();

                response.Message = (response.Data != null)
                    ? $"{HttpStatusCode.OK} - Doctor is found!"
                    : $"{HttpStatusCode.NoContent} - No doctor is found";

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
﻿using HealthCare340B.DataModel;
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
                //var query = from d in db.MDoctors
                //            join b in db.MBiodata on d.BiodataId equals b.Id
                //            join o in db.TDoctorOffices on d.Id equals o.DoctorId
                //            join f in db.MMedicalFacilities on o.MedicalFacilityId equals f.Id
                //            join s in db.MSpecializations on o.Specialization equals s.Name
                //            join t in db.TDoctorTreatments on d.Id equals t.DoctorId
                //            join fc in db.MMedicalFacilityCategories on f.MedicalFacilityCategoryId equals fc.Id into fcJoin
                //            from fc in fcJoin.DefaultIfEmpty()
                //            join fs in db.MMedicalFacilitySchedules on f.Id equals fs.MedicalFacilityId into fsJoin
                //            from fs in fsJoin.DefaultIfEmpty()
                //            join e in db.MDoctorEducations on d.Id equals e.DoctorId into eJoin
                //            from e in eJoin.Where(e => e.IsLastEducation == true).DefaultIfEmpty()
                //            where (string.IsNullOrEmpty(location) || f.Name!.Contains(location!))
                //                  && (string.IsNullOrEmpty(doctorName) || b.Fullname!.Contains(doctorName))
                //                  && (string.IsNullOrEmpty(specialization) || s.Name!.Contains(specialization))
                //                  && (string.IsNullOrEmpty(treatment) || t.Name!.Contains(treatment))
                //            group new { d, b, o, f, s, t, fs, e } by new
                //            {
                //                d.Id,
                //                b.Fullname,
                //                SpecializationName = s.Name,
                //                MedicalFacilityName = f.Name,
                //                MedicalFacilityCategoryName = fc.Name,
                //                TreatmentName = t.Name,
                //                b.ImagePath,
                //                e.EndYear
                //            } into g
                //            select new VMMDoctor
                //            {
                //                Id = g.Key.Id,
                //                BiodataId = g.First().d.BiodataId,
                //                Str = g.First().d.Str,
                //                Fullname = g.Key.Fullname,
                //                Specialization = g.Key.SpecializationName,
                //                MedicalFacilityName = g.Key.MedicalFacilityName,
                //                MedicalFacilityCategory = g.Key.MedicalFacilityCategoryName,
                //                MedicalFacilityScheduleDay = g.Select(x => x.fs.Day).FirstOrDefault(),
                //                MedicalFacilityScheduleStartTime = g.Select(x => x.fs.TimeScheduleStart != null ? TimeSpan.Parse(x.fs.TimeScheduleStart) : (TimeSpan?)null).FirstOrDefault(),
                //                MedicalFacilityScheduleEndTime = g.Select(x => x.fs.TimeScheduleEnd != null ? TimeSpan.Parse(x.fs.TimeScheduleEnd) : (TimeSpan?)null).FirstOrDefault(),
                //                Treatment = g.Key.TreatmentName,
                //                StartDate = g.First().o.StartDate,
                //                EndDate = g.First().o.EndDate,
                //                ImagePath = g.Key.ImagePath,
                //                LastEducationEndYear = g.Key.EndYear
                //            };
                var query = from d in db.MDoctors
                            join b in db.MBiodata on d.BiodataId equals b.Id
                            join c in db.TCurrentDoctorSpecializations on d.Id equals c.DoctorId into temp
                            from c in temp.DefaultIfEmpty()
                            join s in db.MSpecializations on c.SpecializationId equals s.Id
                            where d.IsDelete == false && s.Name.Contains(specialization)
                            && (string.IsNullOrEmpty(doctorName) || b.Fullname.Contains(doctorName))
                            select new VMMDoctor
                            {
                                Id = d.Id,
                                Fullname = b.Fullname,
                                SpecializationName = s.Name,
                                ImagePath = b.ImagePath,
                                DoctorOffice = (
                                  from o in db.TDoctorOffices
                                  join m in db.MMedicalFacilities on o.MedicalFacilityId equals m.Id
                                  where o.DoctorId == d.Id && o.IsDelete == false
                                  && (string.IsNullOrEmpty(location) || m.Name.Contains(location))
                                  select new VMTDoctorOffice
                                  {
                                      Id = o.Id,
                                      MedicalFacilityName = m.Name
                                  }
                                ).ToList(),
                                Treatments = (
                                  from t in db.TDoctorTreatments
                                  where t.DoctorId == d.Id && t.IsDelete == false
                                  && (string.IsNullOrEmpty(treatment) || t.Name.Contains(treatment))
                                  select new VMTDoctorTreatment
                                  {
                                      Id = t.Id,
                                      Name = t.Name
                                  }
                                ).ToList(),
                                LastEducationEndYear = (
                                  from e in db.MDoctorEducations
                                  where e.DoctorId == d.Id && e.IsLastEducation == true && e.IsDelete == false
                                  select e.EndYear
                                ).FirstOrDefault(),
                                JadwalPraktek = (
                                  from h in db.TDoctorOfficeSchedules
                                  join mfs in db.MMedicalFacilitySchedules on h.MedicalFacilityScheduleId equals mfs.Id
                                  where h.DoctorId == d.Id && h.IsDelete == false
                                  select new VMMMedicalFacilitySchedule
                                  {
                                      DoctorOfficeScheduleId = h.Id,
                                      Day = mfs.Day,
                                      TimeScheduleStart = mfs.TimeScheduleStart,
                                      TimeScheduleEnd = mfs.TimeScheduleEnd
                                  }
                                ).ToList(),
                                MedicalFacilityCategory = (
                                  from mf in db.MMedicalFacilities
                                  join mfc in db.MMedicalFacilityCategories on mf.MedicalFacilityCategoryId equals mfc.Id
                                  join df in db.TDoctorOffices on mf.Id equals df.MedicalFacilityId
                                  where mfc.IsDelete == false && df.DoctorId == d.Id
                                  select mfc.Name
                                ).FirstOrDefault()
                            };


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
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
                            join o in db.TDoctorOffices on d.Id equals o.DoctorId
                            join f in db.MMedicalFacilities on o.MedicalFacilityId equals f.Id
                            join s in db.MSpecializations on o.Specialization equals s.Name
                            join t in db.TDoctorTreatments on d.Id equals t.DoctorId
                            join fc in db.MMedicalFacilityCategories on f.MedicalFacilityCategoryId equals fc.Id into fcJoin
                            from fc in fcJoin.DefaultIfEmpty()
                            join fs in db.MMedicalFacilitySchedules on f.Id equals fs.MedicalFacilityId into fsJoin
                            from fs in fsJoin.DefaultIfEmpty()
                            group new { d, b, o, f, s, t } by new
                            {
                                d.Id,
                                b.Fullname,
                                SpecializationName = s.Name,
                                MedicalFacilityName = f.Name,
                                MedicalFacilityCategoryName = fc.Name,
                                fs.Day,
                                fs.TimeScheduleStart,
                                fs.TimeScheduleEnd,
                                TreatmentName = t.Name,
                                b.ImagePath
                            } into g
                            select new VMMDoctor
                            {
                                Id = g.Key.Id,
                                BiodataId = g.First().d.BiodataId,
                                Str = g.First().d.Str,
                                Fullname = g.Key.Fullname,
                                Specialization = g.Key.SpecializationName,
                                MedicalFacilityName = g.Key.MedicalFacilityName,
                                MedicalFacilityCategory = g.Key.MedicalFacilityCategoryName,
                                MedicalFacilityScheduleDay = g.Key.Day,
                                MedicalFacilityScheduleStartTime = g.Key.TimeScheduleStart != null ? TimeSpan.Parse(g.Key.TimeScheduleStart) : (TimeSpan?)null,
                                MedicalFacilityScheduleEndTime = g.Key.TimeScheduleEnd != null ? TimeSpan.Parse(g.Key.TimeScheduleEnd) : (TimeSpan?)null,
                                Treatment = g.Key.TreatmentName,
                                ImagePath = g.Key.ImagePath
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
                var query = from d in db.MDoctors
                            join b in db.MBiodata on d.BiodataId equals b.Id
                            join o in db.TDoctorOffices on d.Id equals o.DoctorId
                            join f in db.MMedicalFacilities on o.MedicalFacilityId equals f.Id
                            join s in db.MSpecializations on o.Specialization equals s.Name
                            join t in db.TDoctorTreatments on d.Id equals t.DoctorId
                            join fc in db.MMedicalFacilityCategories on f.MedicalFacilityCategoryId equals fc.Id into fcJoin
                            from fc in fcJoin.DefaultIfEmpty()
                            join fs in db.MMedicalFacilitySchedules on f.Id equals fs.MedicalFacilityId into fsJoin
                            from fs in fsJoin.DefaultIfEmpty()
                            join e in db.MDoctorEducations on d.Id equals e.DoctorId into eJoin
                            from e in eJoin.Where(e => e.IsLastEducation == true).DefaultIfEmpty()
                            where (string.IsNullOrEmpty(location) || f.Name!.Contains(location!))
                                  && (string.IsNullOrEmpty(doctorName) || b.Fullname!.Contains(doctorName))
                                  && (string.IsNullOrEmpty(specialization) || s.Name!.Contains(specialization))
                                  && (string.IsNullOrEmpty(treatment) || t.Name!.Contains(treatment))
                            group new { d, b, o, f, s, t, fs, e } by new
                            {
                                d.Id,
                                b.Fullname,
                                SpecializationName = s.Name,
                                MedicalFacilityName = f.Name,
                                MedicalFacilityCategoryName = fc.Name,
                                TreatmentName = t.Name,
                                b.ImagePath,
                                e.EndYear
                            } into g
                            select new VMMDoctor
                            {
                                Id = g.Key.Id,
                                BiodataId = g.First().d.BiodataId,
                                Str = g.First().d.Str,
                                Fullname = g.Key.Fullname,
                                Specialization = g.Key.SpecializationName,
                                MedicalFacilityName = g.Key.MedicalFacilityName,
                                MedicalFacilityCategory = g.Key.MedicalFacilityCategoryName,
                                MedicalFacilityScheduleDay = g.Select(x => x.fs.Day).FirstOrDefault(),
                                MedicalFacilityScheduleStartTime = g.Select(x => x.fs.TimeScheduleStart != null ? TimeSpan.Parse(x.fs.TimeScheduleStart) : (TimeSpan?)null).FirstOrDefault(),
                                MedicalFacilityScheduleEndTime = g.Select(x => x.fs.TimeScheduleEnd != null ? TimeSpan.Parse(x.fs.TimeScheduleEnd) : (TimeSpan?)null).FirstOrDefault(),
                                Treatment = g.Key.TreatmentName,
                                StartDate = g.First().o.StartDate,
                                EndDate = g.First().o.EndDate,
                                ImagePath = g.Key.ImagePath,
                                LastEducationEndYear = g.Key.EndYear
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
                    select new VMMDoctor
                    {
                        Id = d.Id,
                        BiodataId = d.BiodataId,
                        Str = d.Str,
                        Fullname = b.Fullname
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
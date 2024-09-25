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
                            select new VMMDoctor
                            {
                                Id = d.Id,
                                BiodataId = d.BiodataId,
                                Str = d.Str,
                                Fullname = b.Fullname,
                                Specialization = s.Name,
                                MedicalFacilityName = f.Name,
                                Treatment = t.Name
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
                            where (string.IsNullOrEmpty(location) || f.Name!.Contains(location!))
                                  && (string.IsNullOrEmpty(doctorName) || b.Fullname!.Contains(doctorName))
                                  && (string.IsNullOrEmpty(specialization) || s.Name!.Contains(specialization))
                                  && (string.IsNullOrEmpty(treatment) || t.Name!.Contains(treatment))
                            select new VMMDoctor
                            {
                                Id = d.Id,
                                BiodataId = d.BiodataId,
                                Str = d.Str,
                                Fullname = b.Fullname,
                                Specialization = s.Name,
                                MedicalFacilityName = f.Name,
                                Treatment = t.Name,
                                StartDate = o.StartDate,
                                EndDate = o.EndDate,
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
    }
}
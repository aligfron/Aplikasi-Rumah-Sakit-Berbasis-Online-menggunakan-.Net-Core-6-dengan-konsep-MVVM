using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare340B.DataAccess
{
    
    public class DADokterProfil
    {
        private readonly HealthCare340BContext db;
        public DADokterProfil(HealthCare340BContext _db)
        {
            db = _db;
        }
        public VMResponse<VMMDoctor?> GetByDokterProfil(int id)
        {
            VMResponse<VMMDoctor?> response = new VMResponse<VMMDoctor?>();
            try
            {
                if (id > 0)
                {
                    response.Data = (

                        from doktor in db.MDoctors join biodata in db.MBiodata on doktor.BiodataId equals biodata.Id
                        where doktor.IsDelete == false
                        && (doktor.Id == id)
                        select new VMMDoctor
                        {
                            Id = doktor.Id,
                            ImagePath = biodata.ImagePath,
                            BiodataId = biodata.Id,
                            Fullname = biodata.Fullname,
                            SpecializationName = (
                                from Cspesialisation in db.TCurrentDoctorSpecializations 
                                join s in db.MSpecializations on Cspesialisation.SpecializationId equals s.Id // untuk ngambil nama spesialisasi
                                where  doktor.Id == Cspesialisation.DoctorId &&
                                doktor.IsDelete == false
                                select new VMTCurrentDoctorSpecialization
                                {
                                    Id = Cspesialisation.Id,
                                    Specialization = s.Name,
                                    SpecializationId = Cspesialisation.SpecializationId
                                }
                            ).ToList(),

                            // untuk ngambil nama tindakan medis
                            TreatmentName = (
                                from tindakanmedis in db.TDoctorTreatments 
                                where doktor.Id == tindakanmedis.DoctorId &&
                                doktor.IsDelete == false
                                select new VMTDoctorTreatment
                                {
                                    Id = tindakanmedis.Id,
                                    Name = tindakanmedis.Name
                                }
                            ).ToList(),

                            DoctorOffice = (
                                    from riwayarpraktek in db.TDoctorOffices 
                                    join d in db.MMedicalFacilities on riwayarpraktek.MedicalFacilityId equals d.Id
                                    join e in db.MLocations on d.LocationId equals e.Id // untuk ngambil nama lokasi rumah sakit dan spesialisasi
                                    where doktor.Id == riwayarpraktek.DoctorId &&
                                doktor.IsDelete == false
                                select new VMTDoctorOffice
                                {
                                    LocationName = e.Name,
                                    Specialization = riwayarpraktek.Specialization,
                                    StartDate = riwayarpraktek.StartDate,
                                    EndDate = riwayarpraktek.EndDate
                                }
                            ).ToList(),

                            InstitutionName = (
                                from pendidikan in db.MDoctorEducations 
                                where doktor.Id == pendidikan.DoctorId &&
                                doktor.IsDelete == false
                                select new VMMDoctorEducation
                                {
                                    Id = pendidikan.Id,
                                    InstitutionName = pendidikan.InstitutionName,
                                    Major = pendidikan.Major,
                                    EndYear = pendidikan.EndYear
                                }
                            ).ToList(),
                            Appointment =(
                                from janji in db.TAppointments
                                join doff in db.TDoctorOffices on janji.DoctorOfficeId equals doff.Id
                                where janji.IsDelete == false && doktor.Id == doff.DoctorId
                                select new VMTAppointment
                                {
                                    Id = janji.Id
                                }
                            ).ToList()

                        }                         
  
                    ).FirstOrDefault();

                    if (response.Data != null)
                    {
                        response.StatusCode = HttpStatusCode.OK;
                        response.Message = $"{HttpStatusCode.OK} - Spesialisasi Sukses Full";
                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.NoContent;
                        response.Message = $"{HttpStatusCode.NoContent} - Spesialisasi does not exis";
                    }
                }
                else
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = $"{HttpStatusCode.BadRequest} - please input Spesialisasi";
                }
            }
            catch (Exception e)
            {

                response.Message = $"{HttpStatusCode.InternalServerError} - {e.Message}";
            }
            return response;
        }
        public VMResponse<VMMBiodatum?> Update(VMMBiodatum data)
        {
            var response = new VMResponse<VMMBiodatum?>();
            using (IDbContextTransaction dbTrans = db.Database.BeginTransaction())
            {
                try
                {

                    var existingData = db.MBiodata
                                         .FirstOrDefault(c => c.Id == data.Id && !c.IsDelete);

                    if (existingData == null)
                    {
                        response.StatusCode = HttpStatusCode.NotFound;
                        response.Message = $"{HttpStatusCode.NotFound} - Biodata Not Found";
                        return response;
                    }
                    existingData.ImagePath = data.ImagePath;
                    existingData.ModifiedBy = data.ModifiedBy;
                    existingData.ModifiedOn = DateTime.Now;

                    db.Update(existingData);
                    db.SaveChanges();
                    dbTrans.Commit();


                    response.Data = new VMMBiodatum(existingData);
                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = $"{HttpStatusCode.OK} - Biodata Has Been Updated";
                }
                catch (Exception ex)
                {
                    dbTrans.Rollback();
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                }
            }
            return response;
        }

    }
}

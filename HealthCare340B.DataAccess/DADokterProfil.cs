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
        public VMResponse<VMMDoctor?> GetByDokterProfil(long id)
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
                                where doktor.Id == Cspesialisation.DoctorId &&
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
                                    orderby riwayarpraktek.EndDate descending
                                    select new VMTDoctorOffice
                                    {
                                        Id = riwayarpraktek.Id,
                                        LocationName = e.Name,
                                        Specialization = riwayarpraktek.Specialization,
                                        StartDate = riwayarpraktek.StartDate,
                                        EndDate = riwayarpraktek.EndDate
                                    }
                            ).ToList(),
                            MaxEndDate = (
                                    from riwayarpraktek in db.TDoctorOffices
                                    where doktor.Id == riwayarpraktek.DoctorId &&
                                doktor.IsDelete == false
                                    select new VMTDoctorOffice
                                    {
                                        StartDate = riwayarpraktek.StartDate,
                                        EndDate = riwayarpraktek.EndDate
                                    }
                            ).ToList(),
                            MinStarDate = (
                                    from riwayarpraktek in db.TDoctorOffices
                                    where doktor.Id == riwayarpraktek.DoctorId &&
                                doktor.IsDelete == false
                                    select new VMTDoctorOffice
                                    {
                                        StartDate = riwayarpraktek.StartDate
                                    }
                            ).ToList(),

                            InstitutionName = (
                                from pendidikan in db.MDoctorEducations
                                where pendidikan.DoctorId == doktor.Id &&
                                doktor.IsDelete == false
                                select new VMMDoctorEducation
                                {
                                    Id = pendidikan.Id,
                                    InstitutionName = pendidikan.InstitutionName,
                                    Major = pendidikan.Major,
                                    EndYear = pendidikan.EndYear
                                }
                            ).ToList(),
                            Appointment = (
                                from janji in db.TAppointments
                                join doff in db.TDoctorOffices on janji.DoctorOfficeId equals doff.Id
                                where janji.IsDelete == false && doktor.Id == doff.DoctorId
                                select new VMTAppointment
                                {
                                    Id = janji.Id
                                }
                            ).ToList(),

                            //detail dokter tambahan
                            JadwalPraktek = (
                                from mfs in db.MMedicalFacilitySchedules 
                                join mf in db.MMedicalFacilities on mfs.MedicalFacilityId equals mf.Id
                                join mfc in db.MMedicalFacilityCategories on mf.MedicalFacilityCategoryId equals mfc.Id
                                join dof in db.TDoctorOffices on mf.Id equals dof.MedicalFacilityId
                                where dof.DoctorId == doktor.Id && mfs.IsDelete == false
                                select new VMMMedicalFacilitySchedule
                                {
                                    Day = mfs.Day,
                                    TimeScheduleStart = mfs.TimeScheduleStart,
                                    TimeScheduleEnd = mfs.TimeScheduleEnd

                                }
                            ).ToList(),
                            HargaKonsulMulai = (
                                from dotp in db.TDoctorOfficeTreatmentPrices
                                join dot in db.TDoctorOfficeTreatments on dotp.DoctorOfficeTreatmentId equals dot.Id
                                join dof in db.TDoctorOffices on dot.DoctorOfficeId equals dof.Id
                                where dof.DoctorId == doktor.Id && dotp.IsDelete == false
                                select new VMTDoctorOfficeTreatmentPrice
                                {
                                    Price = dotp.Price,
                                    PriceStartFrom = dotp.PriceStartFrom,
                                    PriceUntilFrom = dotp.PriceUntilFrom
                                }
                            ).ToList()

                        }

                    ).FirstOrDefault();

                    if (response.Data != null)
                    {
                        response.StatusCode = HttpStatusCode.OK;
                        response.Message = $"{HttpStatusCode.OK} - Dokter Profil Sukses Full";
                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.NoContent;
                        response.Message = $"{HttpStatusCode.NoContent} - Dokter Profil does not exis";
                    }
                }
                else
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = $"{HttpStatusCode.BadRequest} - please input Dokter Profil";
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
                        response.Message = $"{HttpStatusCode.NotFound} - Dokter Profil Not Found";
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
                    response.Message = $"{HttpStatusCode.OK} - Dokter Profil Been Updated";
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

        public VMResponse<VMMDoctor?> GetDoctorByBiodataId(int idBiodata)
        {
            VMResponse<VMMDoctor?> response = new VMResponse<VMMDoctor?>();
            try
            {
                if (idBiodata > 0)
                {
                    response.Data = (

                        from doktor in db.MDoctors
                        where doktor.IsDelete == false
                        && (doktor.BiodataId == idBiodata)
                        select new VMMDoctor
                        {
                            Id = doktor.Id,
                            BiodataId = doktor.BiodataId
                        }

                    ).FirstOrDefault();

                    if (response.Data != null)
                    {
                        response.StatusCode = HttpStatusCode.OK;
                        response.Message = $"{HttpStatusCode.OK} - GetDoctorByBiodataId Sukses Full";
                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.NoContent;
                        response.Message = $"{HttpStatusCode.NoContent} - GetDoctorByBiodataId does not exis";
                    }
                }
                else
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                    response.Message = $"{HttpStatusCode.BadRequest} - please input GetDoctorByBiodataId";
                }
            }
            catch (Exception e)
            {

                response.Message = $"{HttpStatusCode.InternalServerError} - {e.Message}";
            }
            return response;
        }

    }
}

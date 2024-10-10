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
                VMMDoctor? DetailDokter = (
                    from doktor in db.MDoctors
                    join biodata in db.MBiodata on doktor.BiodataId equals biodata.Id
                    where doktor.IsDelete == false
                    && (doktor.Id == id)
                    select new VMMDoctor
                    {
                        Id = doktor.Id,
                        ImagePath = biodata.ImagePath,
                        BiodataId = biodata.Id,
                        Fullname = biodata.Fullname
                    }
                ).FirstOrDefault();

                DetailDokter!.SpecializationName = (
                                from Cspesialisation in db.TCurrentDoctorSpecializations
                                join s in db.MSpecializations on Cspesialisation.SpecializationId equals s.Id // untuk ngambil nama spesialisasi
                                where Cspesialisation.DoctorId == DetailDokter.Id &&
                                Cspesialisation.IsDelete == false
                                select s.Name
               ).FirstOrDefault();
                DetailDokter.SpecializationId = (
                                from ts in db.TCurrentDoctorSpecializations
                                join sp in db.MSpecializations on ts.SpecializationId equals sp.Id // untuk ngambil nama spesialisasi
                                where ts.DoctorId == DetailDokter.Id && 
                                ts.IsDelete == false
                                select ts.SpecializationId
               ).FirstOrDefault();
                DetailDokter.TreatmentName = (
                                from tindakanmedis in db.TDoctorTreatments
                                where tindakanmedis.DoctorId == DetailDokter.Id &&
                                tindakanmedis.IsDelete == false
                                select new VMTDoctorTreatment
                                {
                                    Id = tindakanmedis.Id,
                                    Name = tindakanmedis.Name
                                }
                 ).ToList();
                DetailDokter.DoctorOffice = (
                                    from riwayarpraktek in db.TDoctorOffices //spesialisasi, tahun
                                    join d in db.MMedicalFacilities on riwayarpraktek.MedicalFacilityId equals d.Id //nama rumah sakit
                                    join mfc in db.MMedicalFacilityCategories on d.MedicalFacilityCategoryId equals mfc.Id
                                    
                                    join e in db.MLocations on d.LocationId equals e.Id // untuk ngambil lokasi rumah sakit  
                                    where riwayarpraktek.DoctorId == DetailDokter.Id &&
                                    riwayarpraktek.IsDelete == false
                                    orderby riwayarpraktek.EndDate descending
                                    select new VMTDoctorOffice
                                    {
                                        Id = riwayarpraktek.Id,
                                        LocationName = e.Name,
                                        Specialization = riwayarpraktek.Specialization,
                                        StartDate = riwayarpraktek.StartDate,
                                        EndDate = riwayarpraktek.EndDate,
                                        FullAddress = d.FullAddress,
                                        MedicalFacilityName = d.Name,
                                        MedicalFacilityCategory = mfc.Name
                                    }
                            ).ToList();
                DetailDokter.InstitutionName = (
                                from pendidikan in db.MDoctorEducations
                                where pendidikan.DoctorId == DetailDokter.Id &&
                                pendidikan.IsDelete == false
                                orderby pendidikan.EndYear descending
                                select new VMMDoctorEducation
                                {
                                    Id = pendidikan.Id,
                                    InstitutionName = pendidikan.InstitutionName,
                                    Major = pendidikan.Major,
                                    EndYear = pendidikan.EndYear
                                }
                            ).ToList();
                DetailDokter.Appointment = (
                    from janji in db.TAppointments
                    join doff in db.TDoctorOffices on janji.DoctorOfficeId equals doff.Id
                    where janji.IsDelete == false && DetailDokter.Id == doff.DoctorId && janji.IsDelete == false
                    select janji.Id
                ).Count();
                //obrolan
                DetailDokter.Obrolan = (
                                from obrolan in db.TCustomerChats
                                where obrolan.DoctorId == DetailDokter.Id &&
                                obrolan.IsDelete == false
                                select obrolan.Id
                                
                ).Count();

                response.Data = DetailDokter;

                response.Message =
                    (response.Data != null)
                        ? $"{HttpStatusCode.OK} - DetailDokter data successfully fetched"
                        : $"{HttpStatusCode.NoContent} - No DetailDokter found";

                response.StatusCode =
                    (response.Data != null) ? HttpStatusCode.OK : HttpStatusCode.NoContent;


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


        public VMResponse<VMMDoctor?> GetByDetailDokter(long id)
        {
            VMResponse<VMMDoctor?> response = new VMResponse<VMMDoctor?>();
            try
            {
                VMMDoctor? DetailDokter = (
                    from doktor in db.MDoctors
                    join biodata in db.MBiodata on doktor.BiodataId equals biodata.Id
                    where doktor.IsDelete == false
                    && (doktor.Id == id)
                    select new VMMDoctor
                    {
                        Id = doktor.Id,
                        ImagePath = biodata.ImagePath,
                        BiodataId = biodata.Id,
                        Fullname = biodata.Fullname
                    }
                ).FirstOrDefault();

                DetailDokter!.SpecializationName = (
                                from Cspesialisation in db.TCurrentDoctorSpecializations
                                join s in db.MSpecializations on Cspesialisation.SpecializationId equals s.Id // untuk ngambil nama spesialisasi
                                where Cspesialisation.DoctorId == DetailDokter.Id &&
                                Cspesialisation.IsDelete == false
                                select s.Name                                    
               ).FirstOrDefault();
                DetailDokter.TreatmentName = (
                                from tindakanmedis in db.TDoctorTreatments
                                where tindakanmedis.DoctorId == DetailDokter.Id &&
                                tindakanmedis.IsDelete == false
                                select new VMTDoctorTreatment
                                {
                                    Id = tindakanmedis.Id,
                                    Name = tindakanmedis.Name
                                }
                 ).ToList();
                DetailDokter.DoctorOffice = (
                                    from riwayarpraktek in db.TDoctorOffices
                                    join d in db.MMedicalFacilities on riwayarpraktek.MedicalFacilityId equals d.Id //nama rumah sakit
                                    join mfc in db.MMedicalFacilityCategories on d.MedicalFacilityCategoryId equals mfc.Id
                                    join e in db.MLocations on d.LocationId equals e.Id // untuk ngambil nama lokasi rumah sakit dan spesialisasi
                                    join service in db.MServiceUnits on riwayarpraktek.ServiceUnitId equals service.Id
                                    where riwayarpraktek.DoctorId == DetailDokter.Id &&
                                    riwayarpraktek.IsDelete == false
                                    orderby riwayarpraktek.EndDate descending
                                    select new VMTDoctorOffice
                                    {
                                        Id = riwayarpraktek.Id,
                                        LocationName = e.Name,
                                        Specialization = riwayarpraktek.Specialization,
                                        StartDate = riwayarpraktek.StartDate,
                                        EndDate = riwayarpraktek.EndDate,
                                        FullAddress = d.FullAddress,
                                        MedicalFacilityName = d.Name,
                                        MedicalFacilityCategory = mfc.Name,
                                        Servicename = service.Name,
                                        JadwalPraktek = (
                                               from dos in db.TDoctorOfficeSchedules
                                               join mfs in db.MMedicalFacilitySchedules on dos.MedicalFacilityScheduleId equals mfs.Id
                                               join dof in db.TDoctorOffices on mfs.MedicalFacilityId equals dof.MedicalFacilityId
                                               where dos.DoctorId == DetailDokter.Id && dos.IsDelete == false && mfs.MedicalFacilityId == d.Id
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
                                                where dof.DoctorId == DetailDokter.Id
                                                      && dotp.IsDelete == false
                                                      && dof.MedicalFacilityId == riwayarpraktek.MedicalFacilityId
                                                select dotp.PriceStartFrom
                                        ).Min()
                                    }
                            ).ToList();
                //DetailDokter.TotalYearsExperience = (
                //        from riwayarpraktek in db.TDoctorOffices
                //        where riwayarpraktek.DoctorId == DetailDokter.Id && riwayarpraktek.IsDelete == false
                //        select riwayarpraktek.EndDate.HasValue
                //            ? riwayarpraktek.EndDate.Value.Year - riwayarpraktek.StartDate.Year
                //            : 1
                //    ).Sum();
                var maxTest = (
                    from riwayarpraktek in db.TDoctorOffices
                    where riwayarpraktek.DoctorId == DetailDokter.Id && riwayarpraktek.IsDelete == false
                    select riwayarpraktek.EndDate.Value.Year
                ).ToList();

                var minTest = (
                    from riwayarpraktek in db.TDoctorOffices
                    where riwayarpraktek.DoctorId == DetailDokter.Id && riwayarpraktek.IsDelete == false
                    select riwayarpraktek.StartDate.Year
                ).ToList();
                if ( maxTest.Count > 0)
                {
                    DetailDokter.maxendTotalYearsExperience = maxTest.Max();
                }
                else
                {
                    DetailDokter.maxendTotalYearsExperience = 0;
                }

                if (minTest.Count > 0)
                {
                    DetailDokter.minstartTotalYearsExperience = minTest.Min();
                }
                else
                {
                    DetailDokter.minstartTotalYearsExperience = 0;
                }

              

                DetailDokter.InstitutionName = (
                                from pendidikan in db.MDoctorEducations
                                where pendidikan.DoctorId == DetailDokter.Id&&
                                pendidikan.IsDelete == false
                                orderby pendidikan.EndYear descending
                                select new VMMDoctorEducation
                                {
                                    Id = pendidikan.Id,
                                    InstitutionName = pendidikan.InstitutionName,
                                    Major = pendidikan.Major,
                                    EndYear = pendidikan.EndYear
                                }
                            ).ToList();

                //detail dokter tambahan
                DetailDokter.JadwalPraktek = (
                                 from dos in db.TDoctorOfficeSchedules
                                 join mfs in db.MMedicalFacilitySchedules on dos.MedicalFacilityScheduleId equals mfs.Id
                                 join mf in db.MMedicalFacilities on mfs.MedicalFacilityId equals mf.Id
                                 join mfc in db.MMedicalFacilityCategories on mf.MedicalFacilityCategoryId equals mfc.Id
                                 join dof in db.TDoctorOffices on mf.Id equals dof.MedicalFacilityId
                                 where dof.DoctorId == DetailDokter.Id && mfs.IsDelete == false
                                 select new VMMMedicalFacilitySchedule
                                 {
                                     Day = mfs.Day,
                                     TimeScheduleStart = mfs.TimeScheduleStart,
                                     TimeScheduleEnd = mfs.TimeScheduleEnd

                                 }
                             ).ToList();
                DetailDokter.HargaKonsulMulai = (
                                from dotp in db.TDoctorOfficeTreatmentPrices
                                join dot in db.TDoctorOfficeTreatments on dotp.DoctorOfficeTreatmentId equals dot.Id
                                join dof in db.TDoctorOffices on dot.DoctorOfficeId equals dof.Id
                                where dof.DoctorId == DetailDokter.Id && dotp.IsDelete == false
                                select dotp.Price
                                
                            ).FirstOrDefault();

                response.Data = DetailDokter;

                response.Message =
                    (response.Data != null)
                        ? $"{HttpStatusCode.OK} - DetailDokter data successfully fetched"
                        : $"{HttpStatusCode.NoContent} - No DetailDokter found";

                response.StatusCode =
                    (response.Data != null) ? HttpStatusCode.OK : HttpStatusCode.NoContent;

            }
            catch (Exception e)
            {

                response.Message = $"{HttpStatusCode.InternalServerError} - {e.Message}";
            }
            return response;

        }
        public VMResponse<VMMDoctor?> GetByDetailDokter2(long id)
        {
            VMResponse<VMMDoctor?> response = new VMResponse<VMMDoctor?>();
            try
            {
                if (id > 0)
                {
                    response.Data = (

                        from doktor in db.MDoctors
                        join biodata in db.MBiodata on doktor.BiodataId equals biodata.Id
                        where doktor.IsDelete == false
                        && (doktor.Id == id)
                        select new VMMDoctor
                        {
                            Id = doktor.Id,
                            ImagePath = biodata.ImagePath,
                            BiodataId = biodata.Id,
                            Fullname = biodata.Fullname,

                            //SpecializationName = (
                            //    from Cspesialisation in db.TCurrentDoctorSpecializations
                            //    join s in db.MSpecializations on Cspesialisation.SpecializationId equals s.Id // untuk ngambil nama spesialisasi
                            //    where doktor.Id == Cspesialisation.DoctorId &&
                            //    Cspesialisation.IsDelete == false
                            //    select new VMTCurrentDoctorSpecialization
                            //    {
                            //        Id = Cspesialisation.Id,
                            //        Specialization = s.Name,
                            //        SpecializationId = Cspesialisation.SpecializationId
                            //    }
                            //).FirstOrDefault(),

                            // untuk ngambil nama tindakan medis
                            TreatmentName = (
                                from tindakanmedis in db.TDoctorTreatments
                                where doktor.Id == tindakanmedis.DoctorId &&
                                tindakanmedis.IsDelete == false
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
                                    riwayarpraktek.IsDelete == false
                                    orderby riwayarpraktek.EndDate descending
                                    select new VMTDoctorOffice
                                    {
                                        Id = riwayarpraktek.Id,
                                        LocationName = e.Name,
                                        Specialization = riwayarpraktek.Specialization,
                                        StartDate = riwayarpraktek.StartDate,
                                        EndDate = riwayarpraktek.EndDate,
                                        FullAddress = d.FullAddress,
                                        MedicalFacilityName = d.Name,
                                        JadwalPraktek = (
                                                from mfs in db.MMedicalFacilitySchedules
                                                join mf in db.MMedicalFacilities on mfs.MedicalFacilityId equals mf.Id
                                                join mfc in db.MMedicalFacilityCategories on mf.MedicalFacilityCategoryId equals mfc.Id
                                                join dof in db.TDoctorOffices on mf.Id equals dof.MedicalFacilityId
                                                where doktor.Id == dof.DoctorId && mfs.IsDelete == false && mfs.MedicalFacilityId == riwayarpraktek.MedicalFacilityId
                                                select new VMMMedicalFacilitySchedule
                                                {
                                                    Day = mfs.Day,
                                                    TimeScheduleStart = mfs.TimeScheduleStart,
                                                    TimeScheduleEnd = mfs.TimeScheduleEnd

                                                }
                                            ).ToList(),
                                        //HargaKonsulMulai = (
                                        //        from dotp in db.TDoctorOfficeTreatmentPrices
                                        //        join dot in db.TDoctorOfficeTreatments on dotp.DoctorOfficeTreatmentId equals dot.Id
                                        //        join dof in db.TDoctorOffices on dot.DoctorOfficeId equals dof.Id
                                        //        where doktor.Id == dof.DoctorId && dotp.IsDelete == false && dof.MedicalFacilityId == riwayarpraktek.MedicalFacilityId
                                        //        select new VMTDoctorOfficeTreatmentPrice
                                        //        {
                                        //            Price = dotp.Price,
                                        //            PriceStartFrom = dotp.PriceStartFrom,
                                        //            PriceUntilFrom = dotp.PriceUntilFrom
                                        //        }
                                        //    ).ToList()
                                    }
                            ).ToList(),
                            MaxEndDate = (
                                    from riwayarpraktek in db.TDoctorOffices
                                    where doktor.Id == riwayarpraktek.DoctorId &&
                                    riwayarpraktek.IsDelete == false
                                    select new VMTDoctorOffice
                                    {
                                        StartDate = riwayarpraktek.StartDate,
                                        EndDate = riwayarpraktek.EndDate
                                    }
                            ).ToList(),

                            InstitutionName = (
                                from pendidikan in db.MDoctorEducations
                                where doktor.Id == pendidikan.DoctorId &&
                                pendidikan.IsDelete == false
                                orderby pendidikan.EndYear descending
                                select new VMMDoctorEducation
                                {
                                    Id = pendidikan.Id,
                                    InstitutionName = pendidikan.InstitutionName,
                                    Major = pendidikan.Major,
                                    EndYear = pendidikan.EndYear
                                }
                            ).ToList(),

                            //detail dokter tambahan
                            JadwalPraktek = (
                                from mfs in db.MMedicalFacilitySchedules
                                join mf in db.MMedicalFacilities on mfs.MedicalFacilityId equals mf.Id
                                join mfc in db.MMedicalFacilityCategories on mf.MedicalFacilityCategoryId equals mfc.Id
                                join dof in db.TDoctorOffices on mf.Id equals dof.MedicalFacilityId
                                where doktor.Id == dof.DoctorId && mfs.IsDelete == false
                                select new VMMMedicalFacilitySchedule
                                {
                                    Day = mfs.Day,
                                    TimeScheduleStart = mfs.TimeScheduleStart,
                                    TimeScheduleEnd = mfs.TimeScheduleEnd

                                }
                            ).ToList(),
                            /*HargaKonsulMulai = (
                                from dotp in db.TDoctorOfficeTreatmentPrices
                                join dot in db.TDoctorOfficeTreatments on dotp.DoctorOfficeTreatmentId equals dot.Id
                                join dof in db.TDoctorOffices on dot.DoctorOfficeId equals dof.Id
                                where doktor.Id == dof.DoctorId && dotp.IsDelete == false
                                select new VMTDoctorOfficeTreatmentPrice
                                {
                                    Price = dotp.Price,
                                    PriceStartFrom = dotp.PriceStartFrom,
                                    PriceUntilFrom = dotp.PriceUntilFrom
                                }
                            ).ToList()*/

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

    }
}

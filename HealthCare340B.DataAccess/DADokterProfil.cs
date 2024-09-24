using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
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
        public VMResponse<VMTCurrentDoctorSpecialization?> GetBySpesialisasiDokter(int id)
        {
            VMResponse<VMTCurrentDoctorSpecialization?> response = new VMResponse<VMTCurrentDoctorSpecialization?>();
            try
            {
                if (id > 0)
                {
                    response.Data = (

                        from c in db.TCurrentDoctorSpecializations join d in db.MDoctors on c.DoctorId equals d.Id
                        join e in db.MBiodata on d.BiodataId equals e.Id
                        join s in db.MSpecializations on c.SpecializationId equals s.Id
                        where c.IsDelete == false
                        && (c.Id == id)
                        select new VMTCurrentDoctorSpecialization(c,d,e,s)
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
        public VMResponse<VMTCurrentDoctorSpecialization?> GetByTindakanMedisDokter(int id)
        {
            VMResponse<VMTCurrentDoctorSpecialization?> response = new VMResponse<VMTCurrentDoctorSpecialization?>();
            try
            {
                if (id > 0)
                {
                    response.Data = (

                        from c in db.TCurrentDoctorSpecializations
                        join d in db.MDoctors on c.DoctorId equals d.Id
                        join e in db.MBiodata on d.BiodataId equals e.Id
                        join s in db.MSpecializations on c.SpecializationId equals s.Id
                        where c.IsDelete == false
                        && (c.Id == id)
                        select new VMTCurrentDoctorSpecialization(c, d, e, s)
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
    }
}

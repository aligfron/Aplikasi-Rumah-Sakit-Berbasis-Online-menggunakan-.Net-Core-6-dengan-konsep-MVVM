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
    
    public class DASpecializationDoctor
    {
        private readonly HealthCare340BContext db;
        public DASpecializationDoctor(HealthCare340BContext _db)
        {
            db = _db;
        }
        public VMResponse<VMTCurrentDoctorSpecialization?> GetById(int id)
        {
            VMResponse<VMTCurrentDoctorSpecialization?> response = new VMResponse<VMTCurrentDoctorSpecialization?>();
            try
            {
                if (id > 0)
                {
                    response.Data = (

                        from c in db.TCurrentDoctorSpecializations
                        where c.IsDelete == false
                        && (c.Id == id)
                        select new VMTCurrentDoctorSpecialization(c)
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

        public VMResponse<VMTCurrentDoctorSpecialization?> Create(VMTCurrentDoctorSpecialization data)
        {
            var response = new VMResponse<VMTCurrentDoctorSpecialization?>();
            using (IDbContextTransaction dbTrans = db.Database.BeginTransaction())
            {
                try
                {
                    TCurrentDoctorSpecialization newData = new TCurrentDoctorSpecialization
                    {
                        DoctorId = data.DoctorId,
                        SpecializationId = data.SpecializationId,
                        CreatedBy = data.CreatedBy,
                        CreatedOn = DateTime.Now,
                        IsDelete = false
                    };
                    db.Add(newData);
                    db.SaveChanges();
                    dbTrans.Commit();
                    response.Data = new VMTCurrentDoctorSpecialization(newData);
                    response.StatusCode = HttpStatusCode.Created;
                    response.Message = $"{HttpStatusCode.Created} - New Spesialisasi successfully created.";
                }
                catch (Exception ex)
                {
                    dbTrans.Rollback();
                    response.Data = null;
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                }
            }

            return response;
        }

        public VMResponse<VMTCurrentDoctorSpecialization?> Update(VMTCurrentDoctorSpecialization data)
        {
            var response = new VMResponse<VMTCurrentDoctorSpecialization?>();
            using (IDbContextTransaction dbTrans = db.Database.BeginTransaction())
            {
                try
                {
                    var existingData = db.TCurrentDoctorSpecializations
                                         .FirstOrDefault(c => c.Id == data.Id && !c.IsDelete);

                    if (existingData == null)
                    {
                        response.StatusCode = HttpStatusCode.NotFound;
                        response.Message = $"{HttpStatusCode.NotFound} - Specialization Not Found";
                        return response;
                    }


                    existingData.SpecializationId = data.SpecializationId;
                    existingData.ModifiedBy = data.ModifiedBy;
                    existingData.ModifiedOn = DateTime.Now;

                    db.Update(existingData);
                    db.SaveChanges();
                    dbTrans.Commit();

                    response.Data = new VMTCurrentDoctorSpecialization(existingData);
                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = $"{HttpStatusCode.OK} - Specialization Has Been Updated";
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

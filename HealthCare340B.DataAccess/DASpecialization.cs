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
    public class DASpecialization
    {
        private readonly HealthCare340BContext db;

        public DASpecialization(HealthCare340BContext _db)
        {
            db = _db;
        }

        public VMResponse<List<VMMSpecialization>?> GetAll()
        {
            VMResponse<List<VMMSpecialization>?> response = new VMResponse<List<VMMSpecialization>?>();
            try
            {
                var query = from m in db.MSpecializations
                            where m.IsDelete == false
                            select new VMMSpecialization
                            {
                                Id = m.Id,
                                Name = m.Name,
                                CreatedBy = m.CreatedBy,
                                CreatedOn = m.CreatedOn,
                                ModifiedBy = m.ModifiedBy,
                                ModifiedOn = m.ModifiedOn,
                                DeletedBy = m.DeletedBy,
                                DeletedOn = m.DeletedOn,
                                IsDelete = m.IsDelete,
                            };
                var result = query.ToList();
                Console.WriteLine($"Query returned {result.Count} medical facilities");

                response.Data = result;
                response.Message = (response.Data.Count > 0)
                    ? $"{HttpStatusCode.OK} - {response.Data.Count} medical facility(s) successfully fetched"
                    : $"{HttpStatusCode.NoContent} - No medical facility is found";
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


        //ali tambah
        

        public VMResponse<List<VMMSpecialization>> GetByFilter(string filter)
        {
            VMResponse<List<VMMSpecialization>> response = new VMResponse<List<VMMSpecialization>>();
            try
            {
                response.Data = (
                    from a in db.MSpecializations
                    join b in db.MUsers on a.CreatedBy equals b.Id
                    join c in db.MBiodata on b.BiodataId equals c.Id

                    join d in db.MUsers on a.ModifiedBy equals d.Id into Haveuser from d in Haveuser.DefaultIfEmpty()
                    join e in db.MBiodata on d.BiodataId equals e.Id into HaveBioModif from e in HaveBioModif.DefaultIfEmpty()
                    where a.IsDelete == false
                    && (a.Name.Contains(filter))
                    select new VMMSpecialization
                    {
                       Id = a.Id,
                       Name = a.Name,
                       CreatedBy = a.CreatedBy,
                       ModifiedBy = a.ModifiedBy,
                       IsDelete = a.IsDelete,
                       fullname = c.Fullname,
                       fullnameMod = e.Fullname,
                       IdUser = b.Id

                    }).ToList();
                response.Message = (response.Data.Count > 0)
                    ? $"{response.Data.Count} of Spesialisasi(s) found successfully."
                    : $"{HttpStatusCode.NoContent} - No data found";

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
        public VMResponse<VMMSpecialization?> GetById(int id)
        {
            VMResponse<VMMSpecialization?> response = new VMResponse<VMMSpecialization?>();
            try
            {
                if (id > 0)
                {
                    response.Data = (

                        from c in db.MSpecializations
                        where c.IsDelete == false
                        && (c.Id == id)
                        select new VMMSpecialization(c)
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

        public VMResponse<VMMSpecialization?> Create(VMMSpecialization data)
        {
            var response = new VMResponse<VMMSpecialization?>();
            using (IDbContextTransaction dbTrans = db.Database.BeginTransaction())
            {
                try
                {
                    MSpecialization newData = new MSpecialization
                    {
                        Name = data.Name,
                        CreatedBy = data.CreatedBy,
                        CreatedOn = DateTime.Now,
                        IsDelete = false
                    };
                    db.Add(newData);
                    db.SaveChanges();
                    dbTrans.Commit();
                    response.Data = new VMMSpecialization(newData);
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

        public VMResponse<VMMSpecialization?> Update(VMMSpecialization data)
        {
            var response = new VMResponse<VMMSpecialization?>();
            using (IDbContextTransaction dbTrans = db.Database.BeginTransaction())
            {
                try
                {
                    var existingData = db.MSpecializations
                                         .FirstOrDefault(c => c.Id == data.Id && !c.IsDelete);

                    if (existingData == null)
                    {
                        response.StatusCode = HttpStatusCode.NotFound;
                        response.Message = $"{HttpStatusCode.NotFound} - Specialization Not Found";
                        return response;
                    }


                    existingData.Name = data.Name;
                    existingData.ModifiedBy = data.ModifiedBy;
                    existingData.ModifiedOn = DateTime.Now;

                    db.Update(existingData);
                    db.SaveChanges();
                    dbTrans.Commit();

                    response.Data = new VMMSpecialization(existingData);
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


        public VMResponse<VMMSpecialization> Delete(int id, long userId)

        {
            VMResponse<VMMSpecialization?> response = new VMResponse<VMMSpecialization?>();
            using (IDbContextTransaction dbTrans = db.Database.BeginTransaction())
            {
                try
                {
                    MSpecialization? existingData = db.MSpecializations
                                                   .FirstOrDefault(c => c.Id == id && !c.IsDelete);
                    if (existingData == null)
                    {
                        response.StatusCode = HttpStatusCode.NotFound;
                        response.Message = $"{HttpStatusCode.NotFound} - Specialization Not Fount";
                    }


                    existingData.IsDelete = true;
                    existingData.DeletedBy = userId;
                    existingData.DeletedOn = DateTime.Now;

                    db.Update(existingData);
                    db.SaveChanges();
                    dbTrans.Commit();

                    response.Data = new VMMSpecialization(existingData);

                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = $"{HttpStatusCode.OK} - Specialization  Has been Deleted";
                }
                catch (Exception ex)
                {

                    dbTrans.Rollback();
                    response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                }
            }
            return response;
        }

        public VMResponse<VMMSpecialization> GetByDataSama(string filter)
        {
            VMResponse<VMMSpecialization> response = new VMResponse<VMMSpecialization>();
            try
            {
                response.Data = (
                    from a in db.MSpecializations
                    where a.IsDelete == false
                    && a.Name == filter
                    select new VMMSpecialization { Name = a.Name }).FirstOrDefault();
                response.Message = (response.Data != null)
                    ? $"{response.Data} of Spesialisasi(s) found successfully."
                    : $"{HttpStatusCode.NoContent} - No data found";

                response.StatusCode = (response.Data != null)
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

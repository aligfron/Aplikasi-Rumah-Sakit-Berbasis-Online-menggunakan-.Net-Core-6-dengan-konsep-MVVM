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
    public class DAPaymentMethod
    {
        private readonly HealthCare340BContext db;

        public DAPaymentMethod(HealthCare340BContext _db)
        {
            db = _db;
        }

        public VMResponse<List<VMMPaymentMethod>?> GetByFilter(string? filter)
        {
            VMResponse<List<VMMPaymentMethod>?> response = new VMResponse<List<VMMPaymentMethod>?>();
            try
            {
                response.Data = (
                    from pm in db.MPaymentMethods
                    where pm.IsDelete == false && pm.Name != null && pm.Name.Contains(filter!)
                    select new VMMPaymentMethod
                    {
                        Id = pm.Id,
                        Name = pm.Name,
                        CreatedBy = pm.CreatedBy,
                        CreatedOn = pm.CreatedOn,
                        ModifiedBy = pm.ModifiedBy,
                        ModifiedOn = pm.ModifiedOn,
                        DeletedBy = pm.DeletedBy,
                        DeletedOn = pm.DeletedOn,
                        IsDelete = pm.IsDelete
                    }
                ).ToList();

                response.Message = (response.Data.Count > 0)
                    ? $"{HttpStatusCode.OK} - {response.Data.Count} Payment Method data(s) successfully fetched"
                    : $"{HttpStatusCode.NoContent} - No data is found within Payment Method";

                response.StatusCode = (response.Data.Count > 0)
                    ? HttpStatusCode.OK
                    : HttpStatusCode.NoContent;
            }
            catch (Exception e)
            {
                response.Message = $"{HttpStatusCode.InternalServerError} - {e.Message}";
            }
            return response;
        }

        public VMResponse<VMMPaymentMethod?> GetById(long id)
        {
            VMResponse<VMMPaymentMethod?> response = new VMResponse<VMMPaymentMethod?>();
            try
            {
                response.Data = (
                    from pm in db.MPaymentMethods
                    where pm.IsDelete == false && pm.Id == id
                    select new VMMPaymentMethod
                    {
                        Id = pm.Id,
                        Name = pm.Name,
                        CreatedBy = pm.CreatedBy,
                        CreatedOn = pm.CreatedOn,
                        ModifiedBy = pm.ModifiedBy,
                        ModifiedOn = pm.ModifiedOn,
                        DeletedBy = pm.DeletedBy,
                        DeletedOn = pm.DeletedOn,
                        IsDelete = pm.IsDelete
                    }
                ).FirstOrDefault();

                if (response.Data != null)
                {
                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = $"{HttpStatusCode.OK} - Payment Method successfully fetch!";
                }
                else
                {
                    response.StatusCode = HttpStatusCode.NoContent;
                    response.Message = $"{HttpStatusCode.NoContent} - Payment Method does not exist!";
                }
            }
            catch (Exception e)
            {
                response.Message = $"{HttpStatusCode.InternalServerError} - {e.Message}";
            }
            return response;
        }

        public VMResponse<VMMPaymentMethod> Create(VMMPaymentMethod data)
        {
            VMResponse<VMMPaymentMethod> response = new VMResponse<VMMPaymentMethod>();
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    MPaymentMethod newData = new MPaymentMethod();
                    newData.Name = data.Name;
                    newData.CreatedBy = data.CreatedBy;
                    newData.CreatedOn = DateTime.Now;

                    db.Add(newData);
                    db.SaveChanges();

                    dbTran.Commit();

                    response.Data = new VMMPaymentMethod
                    {
                        Id = newData.Id,
                        Name = newData.Name,
                        CreatedBy = newData.CreatedBy,
                        CreatedOn = newData.CreatedOn,
                        ModifiedBy = newData.ModifiedBy,
                        ModifiedOn = newData.ModifiedOn,
                        DeletedBy = newData.DeletedBy,
                        DeletedOn = newData.DeletedOn,
                        IsDelete = newData.IsDelete,
                    };

                    response.StatusCode = HttpStatusCode.Created;
                    response.Message = $"{HttpStatusCode.Created} - New Payment Method has been successfully created!";
                }
                catch (Exception e)
                {
                    dbTran.Rollback();
                    response.Message = $"{HttpStatusCode.InternalServerError} - {e.Message}";
                }

                return response;
            }
        }

        public VMResponse<VMMPaymentMethod> Update(VMMPaymentMethod data)
        {
            VMResponse<VMMPaymentMethod> response = new VMResponse<VMMPaymentMethod>();
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMMPaymentMethod? existingData = GetById(data.Id).Data;
                    if (existingData != null)
                    {
                        MPaymentMethod updatedData = new MPaymentMethod
                        {
                            Id = existingData.Id,
                            Name = data.Name,
                            CreatedBy = existingData.CreatedBy,
                            CreatedOn = existingData.CreatedOn,
                            ModifiedBy = data.ModifiedBy,
                            ModifiedOn = DateTime.Now,
                            DeletedBy = existingData.DeletedBy,
                            DeletedOn = existingData.DeletedOn,
                            IsDelete = existingData.IsDelete
                        };

                        db.Update(updatedData);
                        db.SaveChanges();

                        dbTran.Commit();

                        response.Data = new VMMPaymentMethod
                        {
                            Id = updatedData.Id,
                            Name = updatedData.Name,
                            CreatedBy = updatedData.CreatedBy,
                            CreatedOn = updatedData.CreatedOn,
                            ModifiedBy = updatedData.ModifiedBy,
                            ModifiedOn = updatedData.ModifiedOn,
                            DeletedBy = updatedData.DeletedBy,
                            DeletedOn = updatedData.DeletedOn,
                            IsDelete = updatedData.IsDelete
                        };

                        response.StatusCode = HttpStatusCode.OK;
                        response.Message = $"{HttpStatusCode.OK} - The payment method has been successfully updated";
                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.NotFound;
                        response.Message = $"{HttpStatusCode.NotFound} - Payment Method does not exist";
                    }

                }
                catch (Exception e)
                {
                    dbTran.Rollback();
                    response.Message = $"{HttpStatusCode.InternalServerError} - {e.Message}";
                }
                return response;
            }
        }

        public VMResponse<VMMPaymentMethod> Delete(long id, long deletedBy)
        {
            VMResponse<VMMPaymentMethod> response = new VMResponse<VMMPaymentMethod>();
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMMPaymentMethod? existingData = GetById(id).Data;
                    if (existingData != null)
                    {
                        MPaymentMethod updatedData = new MPaymentMethod
                        {
                            Id = existingData.Id,
                            Name = existingData.Name,
                            CreatedBy = existingData.CreatedBy,
                            CreatedOn = existingData.CreatedOn,
                            ModifiedBy = existingData.ModifiedBy,
                            ModifiedOn = existingData.ModifiedOn,
                            DeletedBy = deletedBy,
                            DeletedOn = DateTime.Now,
                            IsDelete = true
                        };

                        db.Update(updatedData);
                        db.SaveChanges();

                        dbTran.Commit();

                        response.Data = new VMMPaymentMethod
                        {
                            Id = updatedData.Id,
                            Name = updatedData.Name,
                            CreatedBy = updatedData.CreatedBy,
                            CreatedOn = updatedData.CreatedOn,
                            ModifiedBy = updatedData.ModifiedBy,
                            ModifiedOn = updatedData.ModifiedOn,
                            DeletedBy = updatedData.DeletedBy,
                            DeletedOn = updatedData.DeletedOn,
                            IsDelete = updatedData.IsDelete
                        };

                        response.StatusCode = HttpStatusCode.OK;
                        response.Message = $"{HttpStatusCode.OK} - The payment method has been successfully deleted";
                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.NotFound;
                        response.Message = $"{HttpStatusCode.NotFound} - Payment Method does not exist";
                    }

                }
                catch (Exception e)
                {
                    dbTran.Rollback();
                    response.Message = $"{HttpStatusCode.InternalServerError} - {e.Message}";
                }
                return response;
            }
        }
    }
}

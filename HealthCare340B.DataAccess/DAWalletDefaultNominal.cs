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
    public class DAWalletDefaultNominal
    {
        private readonly HealthCare340BContext db;

        public DAWalletDefaultNominal(HealthCare340BContext _db)
        {
            db = _db;
        }

        public VMResponse<List<VMMWalletDefaultNominal>?> GetByFilter(int nominal)
        {
            VMResponse<List<VMMWalletDefaultNominal>?> response = new VMResponse<List<VMMWalletDefaultNominal>?>();

            try
            {
                response.Data = (
                    from wdm in db.MWalletDefaultNominals
                    where wdm.IsDelete == false && wdm.Nominal == nominal
                    select new VMMWalletDefaultNominal
                    {
                        Id = wdm.Id,
                        Nominal = wdm.Nominal,
                        CreatedBy = wdm.CreatedBy,
                        CreatedOn = wdm.CreatedOn,
                        ModifiedBy = wdm.ModifiedBy,
                        ModifiedOn = wdm.ModifiedOn,
                        DeletedBy = wdm.DeletedBy,
                        DeletedOn = wdm.DeletedOn,
                        IsDelete = wdm.IsDelete
                    }
                    ).ToList();

                response.Message = (response.Data.Count > 0)
                    ? $"{HttpStatusCode.OK} - {response.Data.Count} Wallet Default Nominal data(s) successfully fetched"
                    : $"{HttpStatusCode.NoContent} - No data is found within Wallet Default Nominal";

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

        public VMResponse<VMMWalletDefaultNominal?> GetById(long id)
        {
            VMResponse<VMMWalletDefaultNominal?> response = new VMResponse<VMMWalletDefaultNominal?>();

            try
            {
                response.Data = (
                    from wdm in db.MWalletDefaultNominals
                    where wdm.IsDelete == false && wdm.Id == id
                    select new VMMWalletDefaultNominal
                    {
                        Id = wdm.Id,
                        Nominal = wdm.Nominal,
                        CreatedBy = wdm.CreatedBy,
                        CreatedOn = wdm.CreatedOn,
                        ModifiedBy = wdm.ModifiedBy,
                        ModifiedOn = wdm.ModifiedOn,
                        DeletedBy = wdm.DeletedBy,
                        DeletedOn = wdm.DeletedOn,
                        IsDelete = wdm.IsDelete
                    }
                    ).FirstOrDefault();

                if (response.Data != null)
                {
                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = $"{HttpStatusCode.OK} - Wallet Default Nominal successfully fetch!";
                }
                else
                {
                    response.StatusCode = HttpStatusCode.NoContent;
                    response.Message = $"{HttpStatusCode.NoContent} - Wallet Default Nominal does not exist!";
                }
            }
            catch (Exception e)
            {
                response.Message = $"{HttpStatusCode.InternalServerError} - {e.Message}";
            }

            return response;
        }

        public VMResponse<VMMWalletDefaultNominal> Create(VMMWalletDefaultNominal data)
        {
            VMResponse<VMMWalletDefaultNominal> response = new VMResponse<VMMWalletDefaultNominal>();
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    MWalletDefaultNominal newData = new MWalletDefaultNominal();
                    newData.Nominal = data.Nominal;
                    newData.CreatedBy = data.CreatedBy;
                    newData.CreatedOn = DateTime.Now;

                    db.Add(newData);
                    db.SaveChanges();

                    dbTran.Commit();

                    response.Data = new VMMWalletDefaultNominal
                    {
                        Id = newData.Id,
                        Nominal = newData.Nominal,
                        CreatedBy = newData.CreatedBy,
                        CreatedOn = newData.CreatedOn,
                        ModifiedBy = newData.ModifiedBy,
                        ModifiedOn = newData.ModifiedOn,
                        DeletedBy = newData.DeletedBy,
                        DeletedOn = newData.DeletedOn,
                        IsDelete = newData.IsDelete
                    };

                    response.StatusCode = HttpStatusCode.Created;
                    response.Message = $"{HttpStatusCode.Created} - New Wallet Default Nominal has been successfully created!";
                }
                catch (Exception e)
                {
                    dbTran.Rollback();
                    response.Message = $"{HttpStatusCode.InternalServerError} - {e.Message}";
                }
                return response;
            } 
        }

        public VMResponse<VMMWalletDefaultNominal> Update(VMMWalletDefaultNominal data)
        {
            VMResponse<VMMWalletDefaultNominal> response = new VMResponse<VMMWalletDefaultNominal>();
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMMWalletDefaultNominal? existingData = GetById(data.Id).Data;
                    if (existingData != null)
                    {
                        MWalletDefaultNominal updatedData = new MWalletDefaultNominal()
                        {
                            Id = existingData.Id,
                            Nominal = data.Nominal,
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


                        response.StatusCode = HttpStatusCode.OK;
                        response.Message = $"{HttpStatusCode.OK} - The Wallet Default Nominal has been successfully updated";
                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.NotFound;
                        response.Message = $"{HttpStatusCode.NotFound} - Wallet Default Nominal does not exist";
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

        public VMResponse<VMMWalletDefaultNominal> Delete(long id, long deletedBy)
        {
            VMResponse<VMMWalletDefaultNominal> response = new VMResponse<VMMWalletDefaultNominal>();
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMMWalletDefaultNominal? existingData = GetById(id).Data;
                    if (existingData != null)
                    {
                        MWalletDefaultNominal updatedData = new MWalletDefaultNominal()
                        {
                            Id = existingData.Id,
                            Nominal = existingData.Nominal,
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


                        response.StatusCode = HttpStatusCode.OK;
                        response.Message = $"{HttpStatusCode.OK} - The Wallet Default Nominal has been successfully updated";
                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.NotFound;
                        response.Message = $"{HttpStatusCode.NotFound} - Wallet Default Nominal does not exist";
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

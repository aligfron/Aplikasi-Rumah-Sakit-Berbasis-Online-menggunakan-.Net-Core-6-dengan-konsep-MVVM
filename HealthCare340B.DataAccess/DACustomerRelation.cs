using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.EntityFrameworkCore.Storage;

namespace HealthCare340B.DataAccess
{
    public class DACustomerRelation
    {
        private readonly HealthCare340BContext _db;

        public DACustomerRelation(HealthCare340BContext db)
        {
            _db = db;
        }

        public VMResponse<List<VMMCustomerRelation>> GetByFilter(string filter)
        {
            VMResponse<List<VMMCustomerRelation>> response =
                new VMResponse<List<VMMCustomerRelation>>();

            try
            {
                response.Data = (
                    from cr in _db.MCustomerRelations
                    where cr.Name.Contains(filter) && cr.IsDelete == false
                    select new VMMCustomerRelation
                    {
                        Id = cr.Id,
                        Name = cr.Name,
                        CreatedBy = cr.CreatedBy,
                        CreatedOn = cr.CreatedOn,
                        ModifiedBy = cr.ModifiedBy,
                        ModifiedOn = cr.ModifiedOn,
                        DeletedBy = cr.DeletedBy,
                        DeletedOn = cr.DeletedOn,
                        IsDelete = cr.IsDelete,
                    }
                ).ToList();

                response.Message =
                    (response.Data.Count > 0)
                        ? $"{HttpStatusCode.OK} - {response.Data.Count} customer relations data(s) successfully fetched"
                        : $"{HttpStatusCode.NoContent} - No customer relations found";

                response.StatusCode =
                    (response.Data.Count > 0) ? HttpStatusCode.OK : HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
            }

            return response;
        }

        public VMResponse<VMMCustomerRelation> GetById(long id)
        {
            VMResponse<VMMCustomerRelation> response = new VMResponse<VMMCustomerRelation>();

            try
            {
                if (id > 0)
                {
                    response.Data = (
                        from cr in _db.MCustomerRelations
                        where cr.Id == id && cr.IsDelete == false
                        select new VMMCustomerRelation
                        {
                            Id = cr.Id,
                            Name = cr.Name,
                            CreatedBy = cr.CreatedBy,
                            CreatedOn = cr.CreatedOn,
                            ModifiedBy = cr.ModifiedBy,
                            ModifiedOn = cr.ModifiedOn,
                            DeletedBy = cr.DeletedBy,
                            DeletedOn = cr.DeletedOn,
                            IsDelete = cr.IsDelete,
                        }
                    ).FirstOrDefault();

                    if (response.Data != null)
                    {
                        response.Message =
                            $"{HttpStatusCode.OK} - Customer relation data successfully fetched";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.Message =
                            $"{HttpStatusCode.NoContent} - Customer relation not found";
                        response.StatusCode = HttpStatusCode.NoContent;
                    }
                }
                else
                {
                    response.Message =
                        $"{HttpStatusCode.BadRequest} - Invalid customer relation ID";
                    response.StatusCode = HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
            }

            return response;
        }

        public VMResponse<VMMCustomerRelation> Create(VMMCustomerRelation model)
        {
            VMResponse<VMMCustomerRelation> response = new VMResponse<VMMCustomerRelation>();

            using (IDbContextTransaction dbTran = _db.Database.BeginTransaction())
            {
                try
                {
                    if (model != null)
                    {
                        MCustomerRelation cr = new MCustomerRelation
                        {
                            Name = model.Name,
                            IsDelete = false,
                            CreatedBy = model.CreatedBy,
                            CreatedOn = DateTime.Now,
                        };

                        _db.Add(cr);
                        _db.SaveChanges();
                        dbTran.Commit();

                        response.Data = new VMMCustomerRelation {
                            Id = cr.Id,
                            Name = cr.Name,
                            CreatedBy = cr.CreatedBy,
                            CreatedOn = cr.CreatedOn,
                            ModifiedBy = cr.ModifiedBy,
                            ModifiedOn = cr.ModifiedOn,
                            DeletedBy = cr.DeletedBy,
                            DeletedOn = cr.DeletedOn,
                            IsDelete = cr.IsDelete,
                        };
                        response.Message =
                            $"{HttpStatusCode.Created} - Customer relation data successfully inserted";
                        response.StatusCode = HttpStatusCode.Created;
                    }
                    else
                    {
                        response.Message =
                            $"{HttpStatusCode.BadRequest} - Invalid customer relation data";
                        response.StatusCode = HttpStatusCode.BadRequest;
                    }
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                }

                return response;
            }
        }

        public VMResponse<VMMCustomerRelation> Update(VMMCustomerRelation model)
        {
            VMResponse<VMMCustomerRelation> response = new VMResponse<VMMCustomerRelation>();

            using (IDbContextTransaction dbTran = _db.Database.BeginTransaction())
            {
                try
                {
                    if (model != null)
                    {
                        MCustomerRelation? cr = _db.MCustomerRelations.Find(model.Id);

                        if (cr != null)
                        {
                            cr.Name = model.Name;
                            cr.ModifiedBy = model.ModifiedBy;
                            cr.ModifiedOn = DateTime.Now;

                            _db.Update(cr);
                            _db.SaveChanges();
                            dbTran.Commit();

                            response.Data = new VMMCustomerRelation
                            {
                                Id = cr.Id,
                                Name = cr.Name,
                                CreatedBy = cr.CreatedBy,
                                CreatedOn = cr.CreatedOn,
                                ModifiedBy = cr.ModifiedBy,
                                ModifiedOn = cr.ModifiedOn,
                                DeletedBy = cr.DeletedBy,
                                DeletedOn = cr.DeletedOn,
                                IsDelete = cr.IsDelete,
                            };
                            response.Message =
                                $"{HttpStatusCode.OK} - Customer relation data successfully updated";
                            response.StatusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            response.Message =
                                $"{HttpStatusCode.NotFound} - Customer relation not found";
                            response.StatusCode = HttpStatusCode.NotFound;
                        }
                    }
                    else
                    {
                        response.Message =
                            $"{HttpStatusCode.BadRequest} - Invalid customer relation data";
                        response.StatusCode = HttpStatusCode.BadRequest;
                    }
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                }

                return response;
            }
        }

        public VMResponse<VMMCustomerRelation> Delete(long id, long deletedBy)
        {
            VMResponse<VMMCustomerRelation> response = new VMResponse<VMMCustomerRelation>();

            using (IDbContextTransaction dbTran = _db.Database.BeginTransaction())
            {
                try
                {
                    if (id > 0)
                    {
                        MCustomerRelation? cr = _db.MCustomerRelations.Find(id);

                        if (cr != null)
                        {
                            cr.IsDelete = true;
                            cr.DeletedBy = deletedBy;
                            cr.DeletedOn = DateTime.Now;

                            _db.Update(cr);
                            _db.SaveChanges();
                            dbTran.Commit();

                            response.Data = new VMMCustomerRelation
                            {
                                Id = cr.Id,
                                Name = cr.Name,
                                CreatedBy = cr.CreatedBy,
                                CreatedOn = cr.CreatedOn,
                                ModifiedBy = cr.ModifiedBy,
                                ModifiedOn = cr.ModifiedOn,
                                DeletedBy = cr.DeletedBy,
                                DeletedOn = cr.DeletedOn,
                                IsDelete = cr.IsDelete,
                            };
                            response.Message =
                                $"{HttpStatusCode.OK} - Customer relation data successfully deleted";
                            response.StatusCode = HttpStatusCode.OK;
                        }
                        else
                        {
                            response.Message =
                                $"{HttpStatusCode.NotFound} - Customer relation not found";
                            response.StatusCode = HttpStatusCode.NotFound;
                        }
                    }
                    else
                    {
                        response.Message =
                            $"{HttpStatusCode.BadRequest} - Invalid customer relation ID";
                        response.StatusCode = HttpStatusCode.BadRequest;
                    }
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                }

                return response;
            }
        }
    }
}

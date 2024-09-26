using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.EntityFrameworkCore.Storage;

namespace HealthCare340B.DataAccess
{
    public class DACustomerMember
    {
        private readonly HealthCare340BContext _db;

        public DACustomerMember(HealthCare340BContext db)
        {
            _db = db;
        }

        public VMResponse<List<VMMCustomerMember>> GetByFilter(string filter)
        {
            VMResponse<List<VMMCustomerMember>> response =
                new VMResponse<List<VMMCustomerMember>>();

            try
            {
                List<VMMCustomerMember> customerMembers = (
                    from cm in _db.MCustomerMembers
                    join c in _db.MCustomers on cm.CustomerId equals c.Id
                    join cr in _db.MCustomerRelations on cm.CustomerRelationId equals cr.Id
                    join b in _db.MBiodata on c.BiodataId equals b.Id
                    where
                        b.Fullname!.Contains(filter)
                        && c.IsDelete == false
                        && cm.IsDelete == false
                        && cr.IsDelete == false
                        && b.IsDelete == false
                    select new VMMCustomerMember
                    {
                        Id = cm.Id,
                        ParentBiodataId = cm.ParentBiodataId,
                        CustomerId = c.Id,
                        Fullname = b.Fullname,
                        Dob = c.Dob,
                        Age = (int)((DateTime.Now - c.Dob!.Value).TotalDays / 365.242199),
                        Gender = c.Gender,
                        BloodGroupId = c.BloodGroupId,
                        RhesusType = c.RhesusType,
                        Height = c.Height,
                        Weight = c.Weight,
                        CustomerRelationId = cr.Id,
                        CustomerRelationName = cr.Name,
                        TotalChat = 0,
                        TotalAppointment = 0,
                        CreatedBy = cm.CreatedBy,
                        CreatedOn = cm.CreatedOn,
                        ModifiedBy = cm.ModifiedBy,
                        ModifiedOn = cm.ModifiedOn,
                        DeletedBy = cm.DeletedBy,
                        DeletedOn = cm.DeletedOn,
                        IsDelete = cm.IsDelete,
                    }
                ).ToList();

                foreach (var member in customerMembers)
                {
                    member.TotalChat = _db.TCustomerChats.Count(x => x.CustomerId == member.CustomerId);
                    member.TotalAppointment = _db.TAppointments.Count(x => x.CustomerId == member.CustomerId);
                }

                response.Data = customerMembers;
                response.Message = (response.Data.Count > 0) ? $"{HttpStatusCode.OK} - {response.Data.Count} customer member data(s) successfully fetched" : $"{HttpStatusCode.NoContent} - No customer member data found";
                response.StatusCode = (response.Data.Count > 0) ? HttpStatusCode.OK : HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
            }

            return response;
        }

        public VMResponse<VMMCustomerMember> GetById(long id)
        {
            VMResponse<VMMCustomerMember> response = new VMResponse<VMMCustomerMember>();

            try
            {
                if (id > 0)
                {
                    VMMCustomerMember? customerMembers = (
                        from cm in _db.MCustomerMembers
                        join c in _db.MCustomers on cm.CustomerId equals c.Id
                        join cr in _db.MCustomerRelations on cm.CustomerRelationId equals cr.Id
                        join b in _db.MBiodata on c.BiodataId equals b.Id
                        where
                            cm.Id == id
                            && c.IsDelete == false
                            && cm.IsDelete == false
                            && cr.IsDelete == false
                            && b.IsDelete == false
                        select new VMMCustomerMember
                        {
                            Id = cm.Id,
                            ParentBiodataId = cm.ParentBiodataId,
                            CustomerId = c.Id,
                            Fullname = b.Fullname,
                            Dob = c.Dob,
                            Age = (int)((DateTime.Now - c.Dob!.Value).TotalDays / 365.242199),
                            Gender = c.Gender,
                            BloodGroupId = c.BloodGroupId,
                            RhesusType = c.RhesusType,
                            Height = c.Height,
                            Weight = c.Weight,
                            CustomerRelationId = cr.Id,
                            CustomerRelationName = cr.Name,
                            TotalChat = 0,
                            TotalAppointment = 0,
                            CreatedBy = cm.CreatedBy,
                            CreatedOn = cm.CreatedOn,
                            ModifiedBy = cm.ModifiedBy,
                            ModifiedOn = cm.ModifiedOn,
                            DeletedBy = cm.DeletedBy,
                            DeletedOn = cm.DeletedOn,
                            IsDelete = cm.IsDelete,
                        }
                    ).FirstOrDefault();

                    if (customerMembers != null)
                    {
                        customerMembers.TotalChat = _db.TCustomerChats.Count(x => x.CustomerId == customerMembers.CustomerId);
                        customerMembers.TotalAppointment = _db.TAppointments.Count(x => x.CustomerId == customerMembers.CustomerId);
                    }

                    response.Data = customerMembers;
                    response.Message = (response.Data != null) ? $"{HttpStatusCode.OK} - Customer member data successfully fetched" : $"{HttpStatusCode.NoContent} - No customer member data found";
                    response.StatusCode = (response.Data != null) ? HttpStatusCode.OK : HttpStatusCode.NoContent;
                }
                else
                {
                    response.Message = $"{HttpStatusCode.BadRequest} - Id is required";
                    response.StatusCode = HttpStatusCode.BadRequest;
                }
            }
            catch (Exception ex)
            {
                response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
            }

            return response;
        }

        public VMResponse<List<VMMCustomerMember>?> GetByUserId(long id)
        {
            VMResponse<List<VMMCustomerMember>?> response = new VMResponse<List<VMMCustomerMember>?>();

            try
            {
                response.Data = (
                    from cm in _db.MCustomerMembers
                    join c in _db.MCustomers on cm.CustomerId equals c.Id
                    join cr in _db.MCustomerRelations on cm.CustomerRelationId equals cr.Id
                    join b in _db.MBiodata on cm.ParentBiodataId equals b.Id
                    join u in _db.MUsers on b.Id equals u.BiodataId
                    where cm.IsDelete == false && u.Id == id
                    select new VMMCustomerMember
                    {
                        Id = cm.Id,
                        ParentBiodataId = cm.ParentBiodataId,
                        CustomerId = c.Id,
                        Fullname = b.Fullname,
                        Dob = c.Dob,
                        Age = (int)((DateTime.Now - c.Dob!.Value).TotalDays / 365.242199),
                        Gender = c.Gender,
                        BloodGroupId = c.BloodGroupId,
                        RhesusType = c.RhesusType,
                        Height = c.Height,
                        Weight = c.Weight,
                        CustomerRelationId = cr.Id,
                        CustomerRelationName = cr.Name,
                        TotalChat = 0,
                        TotalAppointment = 0,
                        CreatedBy = cm.CreatedBy,
                        CreatedOn = cm.CreatedOn,
                        ModifiedBy = cm.ModifiedBy,
                        ModifiedOn = cm.ModifiedOn,
                        DeletedBy = cm.DeletedBy,
                        DeletedOn = cm.DeletedOn,
                        IsDelete = cm.IsDelete,
                    }
                    ).ToList();

                foreach (VMMCustomerMember member in response.Data)
                {
                    member.TotalChat = _db.TCustomerChats.Count(x => x.CustomerId == member.CustomerId);
                    member.TotalAppointment = _db.TAppointments.Count(x => x.CustomerId == member.CustomerId);
                }

                response.Message = (response.Data.Count > 0) ? $"{HttpStatusCode.OK} - {response.Data.Count} customer member data(s) successfully fetched" : $"{HttpStatusCode.NoContent} - No customer member data found";
                response.StatusCode = (response.Data.Count > 0) ? HttpStatusCode.OK : HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
            }

            return response;
        }

        public VMResponse<VMMCustomerMember> Create(VMMCustomerMember model)
        {
            VMResponse<VMMCustomerMember> response = new VMResponse<VMMCustomerMember>();

            using (IDbContextTransaction dbTran = _db.Database.BeginTransaction())
            {
                try
                {
                    if (model != null)
                    {
                        //Save biodata
                        MBiodatum biodata = new MBiodatum { Fullname = model.Fullname };

                        _db.Add(biodata);
                        _db.SaveChanges();

                        //Save customer
                        MCustomer customer = new MCustomer
                        {
                            BiodataId = biodata.Id,
                            Dob = model.Dob,
                            Gender = model.Gender,
                            BloodGroupId = model.BloodGroupId,
                            RhesusType = model.RhesusType,
                            Height = model.Height,
                            Weight = model.Weight,
                        };
                        _db.Add(customer);
                        _db.SaveChanges();

                        //Save customer member
                        MCustomerMember customerMember = new MCustomerMember
                        {
                            ParentBiodataId = model.ParentBiodataId,
                            CustomerId = customer.Id,
                            CustomerRelationId = model.CustomerRelationId,
                            CreatedBy = model.CreatedBy,
                            CreatedOn = DateTime.Now,
                            IsDelete = false,
                        };
                        _db.Add(customerMember);
                        _db.SaveChanges();
                        dbTran.Commit();

                        response.Data = new VMMCustomerMember
                        {
                            Id = customerMember.Id,
                            ParentBiodataId = customerMember.ParentBiodataId,
                            CustomerId = customerMember.Id,
                            Fullname = biodata.Fullname,
                            Dob = customer.Dob,
                            Age = (int)((DateTime.Now - customer.Dob!.Value).TotalDays / 365.242199),
                            Gender = customer.Gender,
                            BloodGroupId = customer.BloodGroupId,
                            RhesusType = customer.RhesusType,
                            Height = customer.Height,
                            Weight = customer.Weight,
                            CustomerRelationId = customerMember.CustomerRelationId,
                            CustomerRelationName = _db
                                .MCustomerRelations.Find(customerMember.CustomerRelationId)
                                ?.Name,
                            CreatedBy = customerMember.CreatedBy,
                            CreatedOn = customerMember.CreatedOn,
                            ModifiedBy = customerMember.ModifiedBy,
                            ModifiedOn = customerMember.ModifiedOn,
                            DeletedBy = customerMember.DeletedBy,
                            DeletedOn = customerMember.DeletedOn,
                            IsDelete = customerMember.IsDelete,
                        };

                        response.Message =
                            $"{HttpStatusCode.Created} - Customer member has been created";
                        response.StatusCode = HttpStatusCode.Created;
                    }
                    else
                    {
                        response.Message =
                            $"{HttpStatusCode.BadRequest} - Invalid customer member data";
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

        public VMResponse<VMMCustomerMember> Update(VMMCustomerMember model)
        {
            VMResponse<VMMCustomerMember> response = new VMResponse<VMMCustomerMember>();

            using (IDbContextTransaction dbTran = _db.Database.BeginTransaction())
            {
                try
                {
                    if (model != null)
                    {
                        //Update customer member
                        MCustomerMember customerMember = _db.MCustomerMembers.Find(model.Id);
                        if (customerMember != null)
                        {
                            customerMember.CustomerRelationId = model.CustomerRelationId;
                            customerMember.ModifiedBy = model.ModifiedBy;
                            customerMember.ModifiedOn = DateTime.Now;
                            _db.Update(customerMember);
                            _db.SaveChanges();

                            //Update customer
                            MCustomer customer = _db.MCustomers.Find(model.CustomerId);

                            if (customer != null)
                            {
                                //Update customer
                                customer.Dob = model.Dob;
                                customer.Gender = model.Gender;
                                customer.BloodGroupId = model.BloodGroupId;
                                customer.RhesusType = model.RhesusType;
                                customer.Height = model.Height;
                                customer.Weight = model.Weight;

                                _db.Update(customer);
                                _db.SaveChanges();

                                //Update biodata
                                MBiodatum biodata = _db.MBiodata.Find(customer.BiodataId);
                                if (biodata != null)
                                {
                                    biodata.Fullname = model.Fullname;
                                    _db.Update(biodata);
                                    _db.SaveChanges();
                                }

                                dbTran.Commit();

                                response.Data = new VMMCustomerMember
                                {
                                    Id = customerMember.Id,
                                    ParentBiodataId = customerMember.ParentBiodataId,
                                    CustomerId = customerMember.Id,
                                    Fullname = biodata.Fullname,
                                    Dob = customer.Dob,
                                    Age = (int)((DateTime.Now - customer.Dob!.Value).TotalDays / 365.242199),
                                    Gender = customer.Gender,
                                    BloodGroupId = customer.BloodGroupId,
                                    RhesusType = customer.RhesusType,
                                    Height = customer.Height,
                                    Weight = customer.Weight,
                                    CustomerRelationId = customerMember.CustomerRelationId,
                                    CustomerRelationName = _db
                                        .MCustomerRelations.Find(customerMember.CustomerRelationId)
                                        ?.Name,
                                    CreatedBy = customerMember.CreatedBy,
                                    CreatedOn = customerMember.CreatedOn,
                                    ModifiedBy = customerMember.ModifiedBy,
                                    ModifiedOn = customerMember.ModifiedOn,
                                    DeletedBy = customerMember.DeletedBy,
                                    DeletedOn = customerMember.DeletedOn,
                                    IsDelete = customerMember.IsDelete,
                                };
                                response.Message =
                                    $"{HttpStatusCode.OK} - Customer member has been updated";
                                response.StatusCode = HttpStatusCode.OK;
                            }
                        }
                    }
                    else
                    {
                        response.Message =
                            $"{HttpStatusCode.BadRequest} - Invalid customer member data";
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

        public VMResponse<VMMCustomerMember> Delete(long id, long deletedBy)
        {
            VMResponse<VMMCustomerMember> response = new VMResponse<VMMCustomerMember>();

            using (IDbContextTransaction dbTran = _db.Database.BeginTransaction())
            {
                try
                {
                    if (id > 0)
                    {
                        //Delete customer member
                        MCustomerMember customerMember = _db.MCustomerMembers.Find(id);
                        if (customerMember != null)
                        {
                            customerMember.DeletedBy = deletedBy;
                            customerMember.DeletedOn = DateTime.Now;
                            customerMember.IsDelete = true;
                            _db.Update(customerMember);
                            _db.SaveChanges();

                            //Delete customer
                            MCustomer customer = _db.MCustomers.Find(customerMember.CustomerId);
                            if (customer != null)
                            {
                                customer.DeletedBy = deletedBy;
                                customer.DeletedOn = DateTime.Now;
                                customer.IsDelete = true;
                                _db.Update(customer);
                                _db.SaveChanges();

                                //Delete biodata
                                MBiodatum biodata = _db.MBiodata.Find(customer.BiodataId);
                                if (biodata != null)
                                {
                                    biodata.DeletedBy = deletedBy;
                                    biodata.DeletedOn = DateTime.Now;
                                    biodata.IsDelete = true;
                                    _db.Update(biodata);
                                    _db.SaveChanges();
                                }

                                dbTran.Commit();

                                response.Data = new VMMCustomerMember
                                {
                                    Id = customerMember.Id,
                                    ParentBiodataId = customerMember.ParentBiodataId,
                                    CustomerId = customerMember.Id,
                                    Fullname = biodata.Fullname,
                                    Dob = customer.Dob,
                                    Age = (int)((DateTime.Now - customer.Dob!.Value).TotalDays / 365.242199),
                                    Gender = customer.Gender,
                                    BloodGroupId = customer.BloodGroupId,
                                    RhesusType = customer.RhesusType,
                                    Height = customer.Height,
                                    Weight = customer.Weight,
                                    CustomerRelationId = customerMember.CustomerRelationId,
                                    CustomerRelationName = _db
                                        .MCustomerRelations.Find(customerMember.CustomerRelationId)
                                        ?.Name,
                                    CreatedBy = customerMember.CreatedBy,
                                    CreatedOn = customerMember.CreatedOn,
                                    ModifiedBy = customerMember.ModifiedBy,
                                    ModifiedOn = customerMember.ModifiedOn,
                                    DeletedBy = customerMember.DeletedBy,
                                    DeletedOn = customerMember.DeletedOn,
                                    IsDelete = customerMember.IsDelete,
                                };

                                response.Message =
                                    $"{HttpStatusCode.OK} - Customer member has been deleted";
                                response.StatusCode = HttpStatusCode.OK;
                            }
                        }
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

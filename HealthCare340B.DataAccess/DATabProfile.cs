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
    public class DATabProfile
    {
        private readonly HealthCare340BContext db;

        public DATabProfile(HealthCare340BContext db)
        {
            this.db = db;
        }

        public VMResponse<VMMCustomer> GetById(long id)
        {
            VMResponse<VMMCustomer> response = new VMResponse<VMMCustomer>();
            try
            {
                response.Data = (
                    from m in db.MCustomers
                    join b in db.MBiodata on m.BiodataId equals b.Id
                    join o in db.MBloodGroups on m.BloodGroupId equals o.Id
                    join u in db.MUsers on b.Id equals u.BiodataId
                    where m.IsDelete == false && m.Id == id
                    select new VMMCustomer
                    {
                        Id = m.Id,
                        BiodataId = b.Id,
                        Fullname = b.Fullname,
                        MobilePhone = b.MobilePhone,
                        Email = u.Email,
                        Password = u.Password,
                        Dob = m.Dob,
                        Gender = m.Gender,
                        BloodGroupId = o.Id,
                        RhesusType = m.RhesusType,
                        Height = m.Height,
                        Weight = m.Weight,
                        CreatedBy = m.CreatedBy,
                        CreatedOn = m.CreatedOn,
                        ModifiedBy = m.ModifiedBy,
                        ModifiedOn = m.ModifiedOn,
                        DeletedOn = m.DeletedOn,
                        DeletedBy = m.DeletedBy,
                        IsDelete = m.IsDelete,
                    }
                ).FirstOrDefault();
                response.StatusCode = (response.Data != null)
                   ? HttpStatusCode.OK
                   : HttpStatusCode.NoContent;
                response.Message = (response.Data != null)
                    ? $"{HttpStatusCode.OK} Profile Customer successfully created!"
                    : $"{HttpStatusCode.NoContent} - Profile Customer does not exist!";
            }
            catch (Exception ex)
            {
                response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
            }
            return response;
        }

        public VMResponse<VMMCustomer> GetCustomerByBioId(long id)
        {
            VMResponse<VMMCustomer> response = new VMResponse<VMMCustomer>();
            try
            {
                response.Data = (
                    from m in db.MCustomers
                    join b in db.MBiodata on m.BiodataId equals b.Id
                    join o in db.MBloodGroups on m.BloodGroupId equals o.Id
                    join u in db.MUsers on b.Id equals u.BiodataId
                    where m.IsDelete == false && b.Id == id
                    select new VMMCustomer
                    {
                        Id = m.Id,
                        BiodataId = b.Id,
                        Fullname = b.Fullname,
                        MobilePhone = b.MobilePhone,
                        Email = u.Email,
                        Password = u.Password,
                        Dob = m.Dob,
                        Gender = m.Gender,
                        BloodGroupId = o.Id,
                        RhesusType = m.RhesusType,
                        Height = m.Height,
                        Weight = m.Weight,
                        CreatedBy = m.CreatedBy,
                        CreatedOn = m.CreatedOn,
                        ModifiedBy = m.ModifiedBy,
                        ModifiedOn = m.ModifiedOn,
                        DeletedOn = m.DeletedOn,
                        DeletedBy = m.DeletedBy,
                        IsDelete = m.IsDelete,
                    }
                ).FirstOrDefault();
                response.StatusCode = (response.Data != null)
                   ? HttpStatusCode.OK
                   : HttpStatusCode.NoContent;
                response.Message = (response.Data != null)
                    ? $"{HttpStatusCode.OK} Profile Customer successfully created!"
                    : $"{HttpStatusCode.NoContent} - Profile Customer does not exist!";
            }
            catch (Exception ex)
            {
                response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
            }
            return response;
        }

        public VMResponse<VMMCustomer> Update(VMMCustomer data)
        {
            VMResponse<VMMCustomer> response = new VMResponse<VMMCustomer>();
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMMCustomer? existingData = GetById(data.Id).Data;
                    if (existingData != null)
                    {
                        // Update MCustomer entity
                        MCustomer updateCustomerData = db.MCustomers.Find(data.Id);

                        updateCustomerData.Dob = data.Dob;
                        updateCustomerData.ModifiedBy = data.ModifiedBy;
                        updateCustomerData.ModifiedOn = DateTime.Now;

                        // Fetch and update MBiodata entity
                        MBiodatum? biodata = db.MBiodata.FirstOrDefault(b => b.Id == existingData.BiodataId);
                        if (biodata != null)
                        {
                            biodata.Fullname = data.Fullname;
                            biodata.MobilePhone = data.MobilePhone;
                        }

                        // Fetch and update MUsers entity
                        MUser? user = db.MUsers.FirstOrDefault(u => u.BiodataId == existingData.BiodataId);
                        if (user != null)
                        {
                            // Do not update Email and Password
                        }

                        db.Update(updateCustomerData);
                        if (biodata != null) db.Update(biodata);
                        if (user != null) db.Update(user);

                        db.SaveChanges();
                        dbTran.Commit();

                        response.Data = GetById(updateCustomerData.Id).Data;
                        response.StatusCode = HttpStatusCode.OK;
                        response.Message = $"{response.StatusCode} - Customer Profile {response.Data?.Id} has been successfully updated!";
                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.NoContent;
                        response.Message = $"{response.StatusCode} - Customer profile does not exist!";
                    }
                }
                catch (Exception ex)
                {
                    response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                    if (ex.InnerException != null)
                    {
                        response.Message += $" Inner Exception: {ex.InnerException.Message}";
                    }
                    dbTran.Rollback();
                }
                return response;
            }
        }
    }
}

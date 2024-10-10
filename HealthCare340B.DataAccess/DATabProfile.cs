using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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

        public VMResponse<VMMCustomer> GetBioById(long id)
        {
            VMResponse<VMMCustomer> response = new VMResponse<VMMCustomer>();
            try
            {
                response.Data = (
                    from b in db.MBiodata
                    join u in db.MUsers on b.Id equals u.BiodataId
                    join m in db.MCustomers on b.Id equals m.BiodataId into temp
                    from m in temp.DefaultIfEmpty()
                    where b.IsDelete == false && b.Id == id
                    select new VMMCustomer
                    {
                        Id = m.Id != null ? m.Id : 0 ,
                        BiodataId = b.Id,
                        Fullname = b.Fullname,
                        MobilePhone = b.MobilePhone,
                        Email = u.Email,
                        Password = u.Password,
                        CreatedBy = m.CreatedBy != null ? m.CreatedBy : 0,
                        CreatedOn = m.CreatedOn != null ? m.CreatedOn : DateTime.Now,
                        ModifiedBy = m.ModifiedBy != null ? m.ModifiedBy : 0,
                        ModifiedOn = m.ModifiedOn != null ? m.ModifiedOn : DateTime.Now,
                        DeletedOn = m.DeletedOn != null ? m.DeletedOn : DateTime.Now,
                        DeletedBy = m.DeletedBy != null ? m.DeletedBy : 0,
                        IsDelete = m.IsDelete != null ? m.IsDelete : false
                    }
                ).FirstOrDefault();
                response.StatusCode = (response.Data != null)
                   ? HttpStatusCode.OK
                   : HttpStatusCode.NoContent;
                response.Message = (response.Data != null)
                    ? $"{HttpStatusCode.OK} Profile Customer successfully fetched!"
                    : $"{HttpStatusCode.NoContent} - Profile Customer does not exist!";
            }
            catch (Exception ex)
            {
                response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
            }
            return response;
        }

        public VMResponse<VMMUser> GetUserById(long id)
        {
            VMResponse<VMMUser> response = new VMResponse<VMMUser>();
            try
            {
                response.Data = (
                    from m in db.MUsers
                    where m.IsDelete == false && m.Id == id
                    select new VMMUser
                    {
                        Id = m.Id,
                        BiodataId = m.BiodataId,
                        RoleId = m.RoleId,
                        Email = m.Email,
                        Password = m.Password,
                        LoginAttempt = m.LoginAttempt,
                        IsLocked = m.IsLocked,
                        LastLogin = m.LastLogin,
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
                    ? $"{HttpStatusCode.OK}  user successfully created!"
                    : $"{HttpStatusCode.NoContent} - user does not exist!";
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

        public VMResponse<VMMUser> UpdateEmail(VMMUser data)
        {
            VMResponse<VMMUser> response = new VMResponse<VMMUser>();
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMMUser? existingData = GetUserById(data.Id).Data;
                    if (existingData != null)
                    {
                        MUser updatedData = new MUser()
                        {
                            Id = existingData.Id,
                            Email = data.Email,
                            BiodataId = existingData.BiodataId,
                            RoleId = existingData.RoleId,
                            Password = existingData.Password,
                            LoginAttempt = existingData.LoginAttempt,
                            IsLocked = existingData.IsLocked,
                            LastLogin = existingData.LastLogin,
                            CreatedBy = existingData.CreatedBy,
                            CreatedOn = existingData.CreatedOn,
                            ModifiedBy = 1,
                            ModifiedOn = DateTime.Now,
                            DeletedBy = existingData?.DeletedBy,
                            DeletedOn = existingData?.DeletedOn,
                            IsDelete = existingData.IsDelete
                        };

                        db.Update(updatedData);
                        db.SaveChanges();

                        dbTran.Commit();

                        response.Data = new VMMUser()
                        {
                            Id = updatedData.Id,
                            Email = updatedData.Email,
                            BiodataId = updatedData.BiodataId,
                            RoleId = updatedData.RoleId,
                            Password = updatedData.Password,
                            LoginAttempt = updatedData.LoginAttempt,
                            IsLocked = updatedData.IsLocked,
                            LastLogin = updatedData.LastLogin,
                            CreatedBy = updatedData.CreatedBy,
                            CreatedOn = updatedData.CreatedOn,
                            ModifiedBy = updatedData.ModifiedBy,
                            ModifiedOn = updatedData.ModifiedOn,
                            DeletedBy = updatedData?.DeletedBy,
                            DeletedOn = updatedData?.DeletedOn,
                            IsDelete = updatedData.IsDelete
                        };

                        response.Message = "Email updated successfully.";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.Message = "User not found.";
                        response.StatusCode = HttpStatusCode.NotFound;
                    }
                }
                catch (Exception ex)
                {
                    response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                    if (ex.InnerException != null)
                    {
                        response.Message += $" Inner Exception: {ex.InnerException.Message}";
                    }
                    response.StatusCode = HttpStatusCode.InternalServerError;
                }
            }
                
            return response;
        }

        public VMResponse<VMMUser> UpdatePassword(VMMUser data)
        {
            VMResponse<VMMUser> response = new VMResponse<VMMUser>();
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMMUser? existingData = GetUserById(data.Id).Data;
                    if (existingData != null)
                    {
                        MUser updatedData = new MUser()
                        {
                            Id = existingData.Id,
                            Email = existingData.Email,
                            BiodataId = existingData.BiodataId,
                            RoleId = existingData.RoleId,
                            Password = data.Password,
                            LoginAttempt = existingData.LoginAttempt,
                            IsLocked = existingData.IsLocked,
                            LastLogin = existingData.LastLogin,
                            CreatedBy = existingData.CreatedBy,
                            CreatedOn = existingData.CreatedOn,
                            ModifiedBy = 1,
                            ModifiedOn = DateTime.Now,
                            DeletedBy = existingData?.DeletedBy,
                            DeletedOn = existingData?.DeletedOn,
                            IsDelete = existingData.IsDelete
                        };

                        db.Update(updatedData);
                        db.SaveChanges();

                        dbTran.Commit();

                        response.Data = new VMMUser()
                        {
                            Id = updatedData.Id,
                            Email = updatedData.Email,
                            BiodataId = updatedData.BiodataId,
                            RoleId = updatedData.RoleId,
                            Password = updatedData.Password,
                            LoginAttempt = updatedData.LoginAttempt,
                            IsLocked = updatedData.IsLocked,
                            LastLogin = updatedData.LastLogin,
                            CreatedBy = updatedData.CreatedBy,
                            CreatedOn = updatedData.CreatedOn,
                            ModifiedBy = updatedData.ModifiedBy,
                            ModifiedOn = updatedData.ModifiedOn,
                            DeletedBy = updatedData?.DeletedBy,
                            DeletedOn = updatedData?.DeletedOn,
                            IsDelete = updatedData.IsDelete
                        };

                        response.Message = "Password updated successfully.";
                        response.StatusCode = HttpStatusCode.OK;
                    }
                    else
                    {
                        response.Message = "User not found.";
                        response.StatusCode = HttpStatusCode.NotFound;
                    }
                }
                catch (Exception ex)
                {
                    response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                    if (ex.InnerException != null)
                    {
                        response.Message += $" Inner Exception: {ex.InnerException.Message}";
                    }
                    response.StatusCode = HttpStatusCode.InternalServerError;
                }
            }

            return response;
        }

        public VMResponse<VMTToken> GetByOTP(string token)
        {
            VMResponse<VMTToken?> response = new VMResponse<VMTToken?>();
            try
            {
                response.Data = (
                    from t in db.TTokens
                    where t.Token == token &&
                    t.IsDelete == false
                    select new VMTToken
                    {
                        Id = t.Id,
                        Email = t.Email,
                        UserId = t.UserId,
                        Token = t.Token,
                        ExpiredOn = t.ExpiredOn,
                        IsExpired = t.IsExpired,
                        UsedFor = t.UsedFor,
                        CreatedBy = t.CreatedBy,
                        CreatedOn = t.CreatedOn,
                        ModifiedBy = t.ModifiedBy,
                        ModifiedOn = t.ModifiedOn,
                        DeletedBy = t.DeletedBy,
                        DeletedOn = t.DeletedOn,
                        IsDelete = t.IsDelete,
                    }
                    ).FirstOrDefault();
                response.StatusCode = (response.Data != null) ?
                        HttpStatusCode.OK :
                        HttpStatusCode.NotFound;
                response.Message = (response.Data != null) ?
                    $"{HttpStatusCode.OK} - Token succesfully fetched!"
                    : $"{HttpStatusCode.NotFound} - Token does not exist!";
            }
            catch (Exception ex)
            {
                response.Message = $"{HttpStatusCode.NoContent} - {ex.Message}";
            }
            return response;
        }


        public VMResponse<VMMUser> GetByEmail(string Email)
        {
            VMResponse<VMMUser?> response = new VMResponse<VMMUser?>();
            try
            {
                response.Data = (
                    from u in db.MUsers
                    join b in db.MBiodata
                    on u.BiodataId equals b.Id
                    join r in db.MRoles on u.RoleId equals r.Id
                    where u.IsDelete == false && u.Email == Email
                    select new VMMUser()
                    {
                        Id = u.Id,
                        BiodataId = u.BiodataId,
                        ImagePath = b.ImagePath,
                        Name = b.Fullname,
                        RoleName = r.Name,
                        RoleCode = r.Code,
                        RoleId = u.RoleId,
                        Email = u.Email,
                        Password = u.Password,
                        LoginAttempt = u.LoginAttempt,
                        IsLocked = u.IsLocked,
                        LastLogin = u.LastLogin,
                        CreatedBy = u.CreatedBy,
                        CreatedOn = u.CreatedOn,
                        ModifiedBy = u.ModifiedBy,
                        ModifiedOn = u.ModifiedOn,
                        DeletedBy = u.DeletedBy,
                        DeletedOn = u.DeletedOn,
                        IsDelete = false,
                    }
                    ).FirstOrDefault();
                response.StatusCode = (response.Data != null) ?
                        HttpStatusCode.OK :
                        HttpStatusCode.NotFound;
                response.Message = (response.Data != null) ?
                    $"{HttpStatusCode.OK} - User succesfully fetched!"
                    : $"{HttpStatusCode.NotFound} - User does not exist!";

            }
            catch (Exception ex)
            {
                response.Message = $"{HttpStatusCode.NoContent} - {ex.Message}";
            }
            return response!;
        }



        public VMResponse<VMTToken> GenerateOTP(string email)
        {
            VMResponse<VMTToken> response = new VMResponse<VMTToken>();
            var userExist = GetByEmail(email).Data;
            if (userExist != null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Message = $"{HttpStatusCode.NotFound} - Email not found";
                return response;
            }
            var OTP = new Random().Next(1000000, 9999999).ToString();
            var Expire = DateTime.Now.AddMinutes(10);
            bool isExpire = false;
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    TToken OTPToken = new TToken()
                    {
                        Email = email,
                        Token = OTP,
                        UsedFor = "Create Account",
                        ExpiredOn = Expire,
                        IsExpired = isExpire,
                        CreatedBy = 1,
                        CreatedOn = DateTime.Now
                    };
                    db.Add(OTPToken);
                    db.SaveChanges(); // Simpan perubahan
                    dbTran.Commit();
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                }
            }
            response.Data = GetByOTP(OTP).Data;
            response.StatusCode = HttpStatusCode.Created;
            response.Message = $"{HttpStatusCode.Created} - OTP created";
            return response;
        }

        public VMResponse<VMTToken> GenerateOTPPassword(string email)
        {
            VMResponse<VMTToken> response = new VMResponse<VMTToken>();
            var userExist = GetByEmail(email).Data;
            if (userExist == null)
            {
                response.StatusCode = HttpStatusCode.NotFound;
                response.Message = $"{HttpStatusCode.NotFound} - Email not found";
                return response;
            }
            var OTP = new Random().Next(1000000, 9999999).ToString();
            var Expire = DateTime.Now.AddMinutes(10);
            bool isExpire = false;
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    TToken OTPToken = new TToken()
                    {
                        Email = email,
                        Token = OTP,
                        UsedFor = "Create Account",
                        ExpiredOn = Expire,
                        IsExpired = isExpire,
                        CreatedBy = 1,
                        CreatedOn = DateTime.Now
                    };
                    db.Add(OTPToken);
                    db.SaveChanges(); // Simpan perubahan
                    dbTran.Commit();
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                }
            }
            response.Data = GetByOTP(OTP).Data;
            response.StatusCode = HttpStatusCode.Created;
            response.Message = $"{HttpStatusCode.Created} - OTP created";
            return response;
        }

        public VMResponse<VMTToken> VerifyOTP(string OTP)
        {
            VMResponse<VMTToken> response = new VMResponse<VMTToken>();
            var OTPExist = GetByOTP(OTP).Data;
            if (OTPExist == null)
            {
                response.Message = $"{HttpStatusCode.NotFound} - OTP is wrong";
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
            }
            OTPExist.IsExpired = (DateTime.Now >= OTPExist.ExpiredOn) ? true : false;
            //Update IsExpire in database
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    TToken updateIsExpire = new TToken()
                    {
                        Id = OTPExist.Id,
                        Email = OTPExist.Email,
                        UserId = OTPExist.UserId,
                        Token = OTPExist.Token,
                        ExpiredOn = OTPExist.ExpiredOn,
                        IsExpired = OTPExist.IsExpired,
                        UsedFor = OTPExist.UsedFor,
                        CreatedBy = OTPExist.CreatedBy,
                        CreatedOn = OTPExist.CreatedOn,
                        ModifiedBy = OTPExist.ModifiedBy,
                        ModifiedOn = OTPExist.ModifiedOn,
                        DeletedBy = OTPExist.DeletedBy,
                        DeletedOn = OTPExist.DeletedOn,
                        IsDelete = OTPExist.IsDelete,
                    };
                    db.Update(updateIsExpire);
                    db.SaveChanges(); // Simpan perubahan
                    dbTran.Commit();
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    //response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                }
            }


            if (OTPExist.IsExpired == true)
            {
                response.Message = $"{HttpStatusCode.NotFound} - OTP is Expired";
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
                //throw new Exception("OTP is wrong or OTP is Expired");
            }

            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    MUser user = new MUser()
                    {
                        Email = OTPExist.Email,
                        Password = "1",
                        CreatedBy = OTPExist.CreatedBy,
                        CreatedOn = DateTime.Now,
                    };
                    db.Add(user);
                    db.SaveChanges(); // Simpan perubahan
                    dbTran.Commit();
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    //response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                }
            }

            response.Data = OTPExist;
            response.Message = $"{HttpStatusCode.OK} - OTP felched!";
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }


        public VMResponse<VMTResetPassword> GetResetPasswordById(long id)
        {
            VMResponse<VMTResetPassword> response = new VMResponse<VMTResetPassword>();
            try
            {
                response.Data = (
                    from m in db.TResetPasswords
                    
                    where m.IsDelete == false && m.Id == id
                    select new VMTResetPassword
                    {
                        Id = m.Id,
                        OldPassword = m.OldPassword,
                        NewPassword = m.NewPassword,
                        ResetFor = m.ResetFor,
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
                    ? $"{HttpStatusCode.OK} Data Reset Password successfully created!"
                    : $"{HttpStatusCode.NoContent} - Data Reset Password does not exist!";
            }
            catch (Exception ex)
            {
                response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
            }
            return response;
        }

        public VMResponse<VMTResetPassword> CreateResetPassword(VMTResetPassword data)
        {
            VMResponse<VMTResetPassword> response = new VMResponse<VMTResetPassword>();
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {

                    TResetPassword newData = new TResetPassword();
                    newData.OldPassword = data.OldPassword;
                    newData.NewPassword = data.NewPassword;
                    newData.ResetFor = "Reset Password";
                    newData.CreatedBy = data.CreatedBy;
                    newData.CreatedOn = DateTime.Now;
                    newData.IsDelete = false;

                    db.Add(newData);
                    db.SaveChanges();

                    dbTran.Commit();

                    response.Data = GetResetPasswordById(newData.Id).Data;

                    response.StatusCode = HttpStatusCode.Created;
                    response.Message = $"{HttpStatusCode.Created} - New password reset has been successfully created";
                }
                catch (DbUpdateException dbEx)
                {
                    dbTran.Rollback(); // Rollback transaction on error
                    var innerException = dbEx.InnerException?.Message;
                    response.Message = $"{HttpStatusCode.InternalServerError} - {dbEx.Message} - {innerException}";
                    response.StatusCode = HttpStatusCode.InternalServerError;
                }
                catch (Exception ex)
                {
                    dbTran.Rollback(); // Rollback transaction on error
                    response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                    response.StatusCode = HttpStatusCode.InternalServerError;
                }
                return response;
            }
        }
    }
}

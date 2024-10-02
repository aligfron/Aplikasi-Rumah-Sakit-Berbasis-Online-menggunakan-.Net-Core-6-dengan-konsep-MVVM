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
    public class DARegister
    {
        private readonly HealthCare340BContext db;

        public DARegister(HealthCare340BContext _db)
        {
            db = _db;
        }

        public VMResponse<VMMUser> GetById(long Id)
        {
            VMResponse<VMMUser?> response = new VMResponse<VMMUser?>();
            try
            {

                response.Data = (
                    from u in db.MUsers
                    join b in db.MBiodata
                    on u.BiodataId equals b.Id
                    where u.IsDelete == false && u.Id == Id
                    select new VMMUser()
                    {
                        Id = u.Id,
                        BiodataId = u.BiodataId,
                        Name = b.Fullname,
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

        public VMResponse<VMMUser> GetPasswordNull(string? filter)
        {
            VMResponse<VMMUser?> response = new VMResponse<VMMUser?>();
            try
            {
                response.Data = (
                    from u in db.MUsers
                    where u.IsDelete == false && u.Password == filter
                    select new VMMUser()
                    {
                        Id = u.Id,
                        BiodataId = u.BiodataId,
                        ImagePath = null,
                        Name = null,
                        RoleName = null,
                        RoleCode = null,
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

        public VMResponse<VMMUser> GetBiodata(long? biodataId)
        {
            VMResponse<VMMUser?> response = new VMResponse<VMMUser?>();
            try
            {
                response.Data = (
                    from u in db.MUsers
                    where u.IsDelete == false && u.BiodataId == biodataId
                    select new VMMUser()
                    {
                        Id = u.Id,
                        BiodataId = u.BiodataId,
                        ImagePath = null,
                        Name = null,
                        RoleName = null,
                        RoleCode = null,
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


        public VMResponse<VMMUser> ConfirmPassword(string password, string confirmPassword) 
        {
            if (password == null || confirmPassword == null || password !=confirmPassword) 
            {
                throw new Exception("password does not match");
            }

            //var userCheck = db.MUsers.FirstOrDefault(t => t.Password == null);
            //var userData = GetById(userCheck.Id).Data;
            var userCheck = GetPasswordNull("1").Data;
            VMResponse<VMMUser> response = new VMResponse<VMMUser>();
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    MUser user = new MUser()
                    {
                        Id = userCheck.Id,
                        BiodataId = userCheck.BiodataId,
                        Email = userCheck.Email, //ngambil dari OTP
                        Password = password,
                        RoleId = userCheck.RoleId,
                        LoginAttempt = userCheck.LoginAttempt,
                        IsLocked = userCheck.IsLocked,
                        LastLogin = userCheck.LastLogin,
                        CreatedBy = 1,
                        CreatedOn = DateTime.Now,
                        ModifiedBy = userCheck.ModifiedBy,
                        ModifiedOn = DateTime.Now,
                        DeletedBy = userCheck.DeletedBy,
                        DeletedOn = userCheck.DeletedOn,
                        IsDelete = userCheck.IsDelete
                    };
                    db.Update(user);
                    db.SaveChanges(); // Simpan perubahan
                    dbTran.Commit();
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    //response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                }
            }
            response.Data = null;
            response.Message = $"{HttpStatusCode.OK} - Password Match!";
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }
        public VMResponse<VMMUser> register(VMMUser data)
        {
            VMResponse<VMMUser> response = new VMResponse<VMMUser>();
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {

                    MBiodatum biodata = new MBiodatum()
                    {
                        Fullname = data.Name,
                        MobilePhone = data.MobilePhone,
                        CreatedBy = 1,
                        CreatedOn = DateTime.Now,
                    };
                    db.Add(biodata);
                    db.SaveChanges();

                    VMMUser userBio = GetBiodata(null).Data;
                    MUser user = new MUser()
                    {
                        Id = userBio.Id,
                        BiodataId = biodata.Id,
                        Email = userBio.Email, //ngambil dari OTP
                        Password = userBio.Password,
                        RoleId = data.RoleId,
                        LoginAttempt = 0,
                        IsLocked = false,
                        LastLogin = userBio.LastLogin,
                        CreatedBy = 1,
                        CreatedOn = DateTime.Now,
                        ModifiedBy = userBio.ModifiedBy,
                        ModifiedOn = DateTime.Now,
                        DeletedBy = userBio.DeletedBy,
                        DeletedOn = userBio.DeletedOn,
                        IsDelete = userBio.IsDelete

                    };
                    db.Update(user);
                    db.SaveChanges();

                    if (data.RoleId == 1)
                    {
                        MAdmin admin = new MAdmin()
                        {
                            BiodataId = biodata.Id,
                            CreatedBy = 1,
                            CreatedOn = DateTime.Now,
                        };
                        db.Add(admin);
                        db.SaveChanges();
                    }
                    else if (data.RoleId == 2)
                    {
                        MCustomer customer = new MCustomer()
                        {
                            BiodataId = biodata.Id,
                            CreatedBy = 1,
                            CreatedOn = DateTime.Now,
                        };
                        db.Add(customer);
                        db.SaveChanges();
                    }
                    else if (data.RoleId == 3)
                    {
                        MDoctor doctor = new MDoctor()
                        {
                            BiodataId = biodata.Id,
                            CreatedBy = 1,
                            CreatedOn = DateTime.Now,
                        };
                        db.Add(doctor);
                        db.SaveChanges();
                    }
                    else if (data.RoleId == 4)
                    {
                        MMedicalFacility medical = new MMedicalFacility()
                        {
                            Name = data.Name,
                            Email = data.Email,
                            CreatedBy = 1,
                            CreatedOn = DateTime.Now,
                        };
                        db.Add(medical);
                        db.SaveChanges();
                    }
                    dbTran.Commit();
                    response.Data = GetByEmail(userBio.Email).Data;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = $"{HttpStatusCode.OK} - new User successfully Created";
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                }
            }

            return response;
        }

    }
}

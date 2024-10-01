using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare340B.DataAccess
{
    public class DAUser
    {
        private HealthCare340BContext db;
        private int jumlah_attempt = 0;
        //private readonly IEmailSender emailSender;
        public DAUser(HealthCare340BContext _db)
        {
            db = _db;
            //emailSender = _emailSender;
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








        //for register
        public async Task GenerateOTP(string email) 
        {
            var userExist = GetByEmail(email).Data;
            if (userExist != null) 
            {
                throw new Exception("Email already register");
            }
            var OTP = new Random().Next(1000000,9999999).ToString();
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

                    /*MUser user = new MUser() 
                    {
                        Email = email,
                        CreatedBy = 1,
                        CreatedOn = DateTime.Now
                    };*/

                    dbTran.Commit();
                }
                catch(Exception ex) 
                {
                    dbTran.Rollback();
                }   
            }
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
                    $"{HttpStatusCode.OK} - User succesfully fetched!"
                    : $"{HttpStatusCode.NotFound} - User does not exist!";
            }
            catch (Exception ex) 
            {
                response.Message = $"{HttpStatusCode.NoContent} - {ex.Message}";
            }
            return response;
        }
        public VMResponse<VMTToken> VerifyOTP(string OTP) 
        {
            var OTPExist = GetByOTP(OTP).Data;
            OTPExist.IsExpired = (DateTime.Now >= OTPExist.ExpiredOn)? true:false;

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


            VMResponse<VMTToken> response = new VMResponse<VMTToken>();
            if (OTPExist == null || OTPExist.IsExpired == true)
            {
                response.Message = $"{HttpStatusCode.NotFound} - OTP is wrong or Expired";
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
                    
                    MUser user = new MUser() 
                    {
                        BiodataId = data.BiodataId,
                        Email = data.Email, //ngambil dari OTP
                        RoleId = data.RoleId,
                        CreatedBy = 1,
                        CreatedOn = DateTime.Now,
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
                    response.Data = GetByEmail(data.Email).Data;
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











        //for forgot password

        public async Task OTPForgotPassword(string email)
        {
            var userExist = GetByEmail(email).Data;
            if (userExist == null)
            {
                throw new Exception("Email not Found");
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
                        UserId = userExist.Id,
                        ExpiredOn = Expire,
                        UsedFor = "Forget Password",
                        IsExpired = isExpire,
                        CreatedBy = userExist.Id,
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

        }
        
        public VMResponse<VMTToken> VerifyForgotPassword(string OTP)
        {
            var OTPExist = GetByOTP(OTP).Data;
            var EmailExist = GetByEmail(OTPExist.Email);
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
                        ModifiedOn = DateTime.Now,
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


            VMResponse<VMTToken> response = new VMResponse<VMTToken>();
            if (OTPExist == null || OTPExist.IsExpired == true)
            {
                response.Message = $"{HttpStatusCode.NotFound} - OTP is wrong or Expired";
                response.StatusCode = HttpStatusCode.NotFound;
                return response;
                //throw new Exception("OTP is wrong or OTP is Expired");
            }

            response.Data = OTPExist;
            response.Message = $"{HttpStatusCode.OK} - OTP felched!";
            response.StatusCode = HttpStatusCode.OK;
            return response;
        }

        public VMResponse<VMMUser> EditPassword(VMMUser data) 
        {
            VMResponse<VMMUser> response = new VMResponse<VMMUser>();
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction()) 
            {
                try
                {
                    VMMUser existingData = GetByEmail(data.Email).Data!;
                    MUser edit = new MUser
                    {
                        Id = existingData.Id,
                        BiodataId = existingData.BiodataId,
                        RoleId = existingData.RoleId,
                        Email = existingData.Email,
                        Password = data.Password,
                        LoginAttempt = existingData.LoginAttempt,
                        IsLocked = existingData.IsLocked,
                        LastLogin = existingData.LastLogin,
                        CreatedBy = existingData.CreatedBy,
                        CreatedOn = DateTime.Now,
                        ModifiedBy = existingData.ModifiedBy,
                        ModifiedOn = DateTime.Now,
                        DeletedBy = existingData.DeletedBy,
                        DeletedOn = existingData.DeletedOn,
                        IsDelete = existingData.IsDelete
                    };
                    db.Update(edit);
                    db.SaveChanges(); // Simpan perubahan
                    dbTran.Commit();
                    response.Data = GetByEmail(data.Email).Data;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = $"{HttpStatusCode.OK}- Password successfully Updated";
                }
                catch (Exception ex) 
                {
                    dbTran.Rollback();
                    response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                }
            }
            return response;
        }














        //for Login
        public VMResponse<VMMUser> Login(VMMUser data) 
        {
            VMResponse<VMMUser> response = new VMResponse<VMMUser>();
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction()) 
            {
                try 
                {
                    VMMUser existingData = GetByEmail(data.Email).Data!;
                    if (existingData == null)
                    {

                        response.Data = null;
                        response.StatusCode = HttpStatusCode.NotFound;
                        response.Message = $"{HttpStatusCode.NotFound} - User not found";
                        return response;
                    }
                    if (existingData.IsLocked==true)
                    {
                        existingData.LoginAttempt = 5;
                        response.StatusCode = HttpStatusCode.Locked;
                        response.Message = $"{HttpStatusCode.Forbidden} - Account is locked due to multiple failed login attempts.";
                        response.Data = existingData;
                        return response;
                    }
                    if (existingData.Password != data.Password)
                    {

                        existingData.LoginAttempt++;
                        existingData.IsLocked = false;
                        if (existingData.LoginAttempt >= 5)
                        {
                            existingData.IsLocked = true;
                        }
                        MUser userLogin = new MUser()
                        {
                            Id = existingData.Id,
                            BiodataId = existingData.BiodataId,
                            RoleId = existingData.RoleId,
                            Email = data.Email,
                            Password = existingData.Password,
                            LoginAttempt = existingData.LoginAttempt,
                            IsLocked = existingData.IsLocked,
                            LastLogin = existingData.LastLogin,
                            CreatedBy = existingData.CreatedBy,
                            CreatedOn = DateTime.Now,
                            ModifiedBy = existingData.ModifiedBy,
                            ModifiedOn = DateTime.Now,
                            DeletedBy = existingData.DeletedBy,
                            DeletedOn = existingData.DeletedOn,
                            IsDelete = existingData.IsDelete
                        };
                        // Tandai entitas sebagai diubah
                        db.Update(userLogin);
                        db.SaveChanges(); // Simpan perubahan
                        dbTran.Commit();
                        response.Data = GetByEmail(data.Email).Data;
                        response.StatusCode = HttpStatusCode.OK;
                        response.Message = $"{HttpStatusCode.OK}- successfully login";
                        return response;
                    }
                    response.Data = GetByEmail(data.Email).Data;
                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = $"{HttpStatusCode.Unauthorized}-Invalid Password";
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

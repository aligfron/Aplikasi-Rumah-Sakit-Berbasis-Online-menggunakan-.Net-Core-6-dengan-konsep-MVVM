using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare340B.DataAccess
{
    public class DAUser
    {
        private HealthCare340BContext db;
        private int jumlah_attempt = 0;
        public DAUser(HealthCare340BContext _db)
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
        public VMResponse<VMMUser> Create(VMMUser data) 
        {
            VMResponse<VMMUser> response = new VMResponse<VMMUser> ();
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction()) 
            {
                try
                {
                    MBiodatum mBiodatum = new MBiodatum()
                    {
                        Id = data.Id,
                        Fullname = data.Name,
                    };
                    db.Add(mBiodatum);
                    db.SaveChanges();
                    MUser dataUser = new MUser()
                    {
                        BiodataId = mBiodatum.Id,
                        Password = data.Password,
                        RoleId = data.RoleId,
                        LoginAttempt = data.LoginAttempt,
                        IsLocked = data.IsLocked,
                        LastLogin = data.LastLogin,
                        Email = data.Email,
                        CreatedBy = 0,
                        CreatedOn = DateTime.Now,
                    };
                    db.Add(dataUser);
                    db.SaveChanges();

                    dataUser.CreatedBy = dataUser.Id;
                    db.Update(dataUser);
                    db.SaveChanges();

                    dbTran.Commit();

                    response.Data = GetById(dataUser.Id).Data;
                    response.StatusCode =
                        (response.Data != null) ? HttpStatusCode.Created : HttpStatusCode.NoContent;

                    response.Message =
                        (response.Data != null)
                            ? $"{HttpStatusCode.Created} - User created successfully"
                            : $"{HttpStatusCode.NoContent} - User not created";
                }
                catch (Exception ex) 
                {
                    dbTran.Rollback();
                    response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";

                }

            }
            return response;
        }
        public VMResponse<VMMUser> Update(VMMUser data)
        {
            VMResponse<VMMUser> response = new VMResponse<VMMUser>();
            using (IDbContextTransaction dbTran = db.Database.BeginTransaction())
            {
                try
                {
                    VMMUser existingData = GetById(data.Id).Data!;
                    if (existingData != null)
                    {
                        MUser dataUser = new MUser()
                        {
                            BiodataId = data.BiodataId,
                            Password = existingData.Password,
                            RoleId = data.RoleId,
                            LoginAttempt = (existingData.Password == data.Password) ? jumlah_attempt++ : jumlah_attempt,
                            IsLocked = (data.LoginAttempt > 5) ? true : false,
                            //LastLogin = DateTime.Now,

                            Email = existingData.Email,
                            ModifiedBy = existingData.ModifiedBy,
                            //ModifiedOn = DateTime.Now,
                        };
                        db.Update(dataUser);
                        db.SaveChanges();
                        dbTran.Commit();

                        response.Data = GetById(dataUser.Id).Data;
                        response.StatusCode =
                            (response.Data != null) ? HttpStatusCode.OK : HttpStatusCode.NoContent;

                        response.Message =
                            (response.Data != null)
                                ? $"{HttpStatusCode.OK} - User updated successfully"
                                : $"{HttpStatusCode.NoContent} - User not updated";
                    }
                    else
                    {
                        response.Message = $"{HttpStatusCode.NotFound} - User not found";
                    }
                }
                catch (Exception ex)
                {
                    dbTran.Rollback();
                    response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                }
            }
            return response;
        }
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

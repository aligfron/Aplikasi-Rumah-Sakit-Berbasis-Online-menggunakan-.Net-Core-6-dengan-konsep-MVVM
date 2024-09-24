using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HealthCare340B.DataAccess
{
    public class DAMenu
    {
        private HealthCare340BContext db;

        public DAMenu(HealthCare340BContext _db)
        {
            db = _db;
        }

        public VMResponse<VMMMenuRole?> GetById(int id)
        {
            VMResponse<VMMMenuRole?> response = new VMResponse<VMMMenuRole?>();
            try
            {
                response.Data = (
                    from mr in db.MMenuRoles
                    join m in db.MMenus on mr.MenuId equals m.Id
                    where mr.IsDelete == false && mr.Id == id
                    select new VMMMenuRole()
                    {
                        Id = mr.Id,
                        Name = m.Name,
                        MenuId = mr.MenuId,
                        RoleId = mr.RoleId,
                        ParentId = m.ParentId,
                        Url = m.Url,
                        SmallIcon = m.SmallIcon,
                        BigIcon = m.BigIcon,
                        CreatedBy = mr.CreatedBy,
                        CreatedOn = mr.CreatedOn,
                        ModifiedBy = mr.ModifiedBy,
                        ModifiedOn = mr.ModifiedOn,
                        DeletedBy = mr.DeletedBy,
                        DeletedOn = mr.DeletedOn,
                        IsDelete = false,
                    }
                    ).FirstOrDefault();
                response.StatusCode = (response.Data != null) ?
                    HttpStatusCode.OK :
                    HttpStatusCode.NotFound;
                response.Message = (response.Data != null) ?
                    $"{HttpStatusCode.OK} - Menu succesfully fetched!"
                    : $"{HttpStatusCode.NotFound} - Menu does not exist!";
            }
            catch (Exception ex)
            {
                response.Message = $"{HttpStatusCode.NoContent} - {ex.Message}";
            }

            return response;
        }
        public VMResponse<List<VMMMenuRole?>> GetByFilter(string filter)
        {
            VMResponse<List<VMMMenuRole?>> response = new VMResponse<List<VMMMenuRole?>>();
            try
            {
                response.Data = (
                    from mr in db.MMenuRoles
                    join m in db.MMenus on mr.MenuId equals m.Id
                    where mr.IsDelete == false && m.Name!.Contains(filter)
                    select new VMMMenuRole()
                    {
                        Id = mr.Id,
                        Name = m.Name,
                        MenuId = mr.MenuId,
                        RoleId = mr.RoleId,
                        ParentId = m.ParentId,
                        Url = m.Url,
                        SmallIcon = m.SmallIcon,
                        BigIcon = m.BigIcon,
                        CreatedBy = mr.CreatedBy,
                        CreatedOn = mr.CreatedOn,
                        ModifiedBy = mr.ModifiedBy,
                        ModifiedOn = mr.ModifiedOn,
                        DeletedBy = mr.DeletedBy,
                        DeletedOn = mr.DeletedOn,
                        IsDelete = false,
                    }
                    ).ToList();
                response.StatusCode = (response.Data != null) ?
                    HttpStatusCode.OK :
                    HttpStatusCode.NotFound;
                response.Message = (response.Data != null) ?
                    $"{HttpStatusCode.OK} - Menu succesfully fetched!"
                    : $"{HttpStatusCode.NotFound} - Menu does not exist!";
            }
            catch (Exception ex)
            {
                response.Message = $"{HttpStatusCode.NoContent} - {ex.Message}";
            }
            return response;
        }

    }
}
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace HealthCare340B.DataAccess
{
    public class DAMenu
    {
        private HealthCare340BContext db;

        public DAMenu(HealthCare340BContext _db)
        {
            db = _db;
        }

        public VMResponse<List<VMMMenu>> GetByFilter(string filter) 
        {
            VMResponse<List<VMMMenu?>> response = new VMResponse<List<VMMMenu?>>();
            try 
            {
                response.Data = (
                    from m in db.MMenus
                    where m.IsDelete == false && m.Name!.Contains(filter)
                    select new VMMMenu()
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Url = m.Url,
                        ParentId = m.ParentId,
                        BigIcon = m.BigIcon,
                        SmallIcon = m.SmallIcon,
                        CreatedBy = m.CreatedBy,
                        CreatedOn = m.CreatedOn,
                        ModifiedBy = m.ModifiedBy,
                        ModifiedOn = m.ModifiedOn,
                        DeletedBy = m.DeletedBy,
                        DeletedOn = m.DeletedOn,
                        IsDelete = m.IsDelete,
                    }).ToList();
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

        public VMResponse<VMMMenu?> GetById(int id)
        {
            VMResponse<VMMMenu?> response = new VMResponse<VMMMenu?>();
            try
            {
                response.Data = (
                    from m in db.MMenus
                    where m.IsDelete == false && m.Id == id
                    select new VMMMenu()
                    {
                        Id = m.Id,
                        Name = m.Name,
                        Url = m.Url,
                        ParentId = m.ParentId,
                        BigIcon = m.BigIcon,
                        SmallIcon = m.SmallIcon,
                        CreatedBy = m.CreatedBy,
                        CreatedOn = m.CreatedOn,
                        ModifiedBy = m.ModifiedBy,
                        ModifiedOn = m.ModifiedOn,
                        DeletedBy = m.DeletedBy,
                        DeletedOn = m.DeletedOn,
                        IsDelete = m.IsDelete,
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
    }
}

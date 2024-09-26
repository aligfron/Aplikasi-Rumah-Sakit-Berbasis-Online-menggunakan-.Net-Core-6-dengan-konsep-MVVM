using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;

namespace HealthCare340B.DataAccess
{
    public class DABloodGroup
    {
        private readonly HealthCare340BContext _db;

        public DABloodGroup(HealthCare340BContext db)
        {
            _db = db;
        }

        public VMResponse<List<VMMBloodGroup>> GetByFilter(string filter)
        {
            VMResponse<List<VMMBloodGroup>> response = new VMResponse<List<VMMBloodGroup>>();

            try
            {
                response.Data = (
                    from bg in _db.MBloodGroups
                    where
                        bg.IsDelete == false
                        && (bg.Code.Contains(filter) || bg.Descrtiption.Contains(filter))
                    select new VMMBloodGroup
                    {
                        Id = bg.Id,
                        Code = bg.Code,
                        Descrtiption = bg.Descrtiption,
                        CreatedBy = bg.CreatedBy,
                        CreatedOn = bg.CreatedOn,
                        ModifiedBy = bg.ModifiedBy,
                        ModifiedOn = bg.ModifiedOn,
                        DeletedBy = bg.DeletedBy,
                        DeletedOn = bg.DeletedOn,
                        IsDelete = bg.IsDelete,
                    }
                ).ToList();

                response.Message =
                    (response.Data.Count > 0)
                        ? $"{HttpStatusCode.OK} - {response.Data.Count} blood group data(s) successfully fetched"
                        : $"{HttpStatusCode.NoContent} - No blood group found";

                response.StatusCode =
                    (response.Data.Count > 0) ? HttpStatusCode.OK : HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
            }

            return response;
        }
    }
}

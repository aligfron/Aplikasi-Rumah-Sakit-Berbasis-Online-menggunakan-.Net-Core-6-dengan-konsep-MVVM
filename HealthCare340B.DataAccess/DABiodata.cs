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
    public class DABiodata
    {
        private readonly HealthCare340BContext _db;

        public DABiodata(HealthCare340BContext db)
        {
            _db = db;
        }

        public VMResponse<VMMBiodatum?> UpdateImagePath(VMMBiodatum data)
        {
            var response = new VMResponse<VMMBiodatum?>();
            using (IDbContextTransaction dbTrans = _db.Database.BeginTransaction())
            {
                try
                {
                    MBiodatum? existingData = _db.MBiodata.Find(data.Id);

                    if (existingData == null)
                    {
                        response.StatusCode = HttpStatusCode.NotFound;
                        response.Message = $"{HttpStatusCode.NotFound} - Biodata Not Found";
                        return response;
                    }

                    existingData.ImagePath = data.ImagePath;
                    existingData.ModifiedBy = data.ModifiedBy;
                    existingData.ModifiedOn = DateTime.Now;

                    _db.Update(existingData);
                    _db.SaveChanges();
                    dbTrans.Commit();

                    response.Data = new VMMBiodatum(existingData);
                    response.StatusCode = HttpStatusCode.OK;
                    response.Message = $"{HttpStatusCode.OK} - Image Profil Been Updated";
                }
                catch (Exception ex)
                {
                    dbTrans.Rollback();
                    response.StatusCode = HttpStatusCode.InternalServerError;
                    response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                }
            }
            return response;
        }
    }
}

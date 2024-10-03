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
    public class DAMedicalItem
    {
        private readonly HealthCare340BContext db;

        public DAMedicalItem(HealthCare340BContext _db)
        {
            db = _db;
        }

        public VMResponse<List<VMMMedicalItemCategory>?> GetAllCategory()
        {
            VMResponse<List<VMMMedicalItemCategory>?> response = new VMResponse<List<VMMMedicalItemCategory>?>();
            try
            {
                var query = from m in db.MMedicalItemCategories
                            where m.IsDelete == false
                            select new VMMMedicalItemCategory
                            {
                                Id = m.Id,
                                Name = m.Name,
                                CreatedBy = m.CreatedBy,
                                CreatedOn = m.CreatedOn,
                                ModifiedBy = m.ModifiedBy,
                                ModifiedOn = m.ModifiedOn,
                                DeletedBy = m.DeletedBy,
                                DeletedOn = m.DeletedOn,
                                IsDelete = m.IsDelete,
                            };
                var result = query.ToList();
                Console.WriteLine($"Query returned {result.Count} medical Item Category");

                response.Data = result;
                response.Message = (response.Data.Count > 0)
                    ? $"{HttpStatusCode.OK} - {response.Data.Count} medical Item Category(s) successfully fetched"
                    : $"{HttpStatusCode.NoContent} - No medical Item Category is found";
                response.StatusCode = (response.Data.Count > 0)
                    ? HttpStatusCode.OK
                    : HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                response.StatusCode = HttpStatusCode.InternalServerError;
            }
            return response;
        }

        public VMResponse<List<VMMMedicalItemSegmentation>?> GetAllSegmentation()
        {
            VMResponse<List<VMMMedicalItemSegmentation>?> response = new VMResponse<List<VMMMedicalItemSegmentation>?>();
            try
            {
                var query = from m in db.MMedicalItemSegmentations
                            where m.IsDelete == false
                            select new VMMMedicalItemSegmentation
                            {
                                Id = m.Id,
                                Name = m.Name,
                                CreatedBy = m.CreatedBy,
                                CreatedOn = m.CreatedOn,
                                ModifiedBy = m.ModifiedBy,
                                ModifiedOn = m.ModifiedOn,
                                DeletedBy = m.DeletedBy,
                                DeletedOn = m.DeletedOn,
                                IsDelete = m.IsDelete,
                            };
                var result = query.ToList();
                Console.WriteLine($"Query returned {result.Count} medical Item Segmentation");

                response.Data = result;
                response.Message = (response.Data.Count > 0)
                    ? $"{HttpStatusCode.OK} - {response.Data.Count} medical Item Segementation(s) successfully fetched"
                    : $"{HttpStatusCode.NoContent} - No medical Item Segementation is found";
                response.StatusCode = (response.Data.Count > 0)
                    ? HttpStatusCode.OK
                    : HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                response.StatusCode = HttpStatusCode.InternalServerError;
            }
            return response;
        }

        public VMResponse<List<VMMMedicalItem>?> GetByFilter(long? categoryId, bool? segmentation, int? priceMax, int? priceMin, string? name, string? indication)
        {
            string filter = null;
            if(name == indication)
            {
                filter = name;
            }

            VMResponse<List<VMMMedicalItem>?> response = new VMResponse<List<VMMMedicalItem>?>();
            try
            {
                var query = from m in db.MMedicalItems
                            join i in db.MMedicalItemCategories on m.MedicalItemCategoryId equals i.Id
                            join s in db.MMedicalItemSegmentations on m.MedicalItemSegmentationId equals s.Id
                            where m.IsDelete == false
                            && (!categoryId.HasValue || i.Id == categoryId)
                            && (!segmentation.HasValue || (segmentation.Value ? s.Id != 5 : true))
                            && (!priceMax.HasValue || m.PriceMax <= priceMax)
                            && (!priceMin.HasValue || m.PriceMin >= priceMin)
                            && (string.IsNullOrEmpty(filter) || m.Name.ToLower().Contains(filter) || m.Indication.Contains(filter))
                            select new VMMMedicalItem
                            {
                                Id = m.Id,
                                Name = m.Name,
                                MedicalItemCategoryId = i.Id,
                                Composition = m.Composition,
                                MedicalItemSegmentationId = s.Id,
                                Manufacturer = m.Manufacturer,
                                Indication = m.Indication,
                                Dosage = m.Dosage,
                                Directions = m.Directions,
                                Contraindication = m.Contraindication,
                                Caution = m.Caution,
                                CategoryName = i.Name,
                                SegmentationName = s.Name,
                                Packaging = m.Packaging,
                                PriceMax = m.PriceMax,
                                PriceMin = m.PriceMin,
                                ImagePath = m.ImagePath,
                                CreatedBy = m.CreatedBy,
                                CreatedOn = m.CreatedOn,
                                ModifiedBy = m.ModifiedBy,
                                ModifiedOn = m.ModifiedOn,
                                DeletedBy = m.DeletedBy,
                                DeletedOn = m.DeletedOn,
                                IsDelete = m.IsDelete,
                            };

                List<VMMMedicalItem> result = query.ToList();
                Console.WriteLine($"Query returned {result.Count} Medical Item(s)");

                response.Data = result;
                response.Message = (response.Data.Count > 0)
                    ? $"{HttpStatusCode.OK} - {response.Data.Count} Medical Item(s) successfully fetched"
                    : $"{HttpStatusCode.NoContent} - No Medical Item is found";
                response.StatusCode = (response.Data.Count > 0)
                    ? HttpStatusCode.OK
                    : HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

        public VMResponse<VMMMedicalItemCategory?> GetById(long Id)
        {
            VMResponse<VMMMedicalItemCategory?> response = new VMResponse<VMMMedicalItemCategory?>();

            try
            {
                var data = (
                    from mc in db.MMedicalItemCategories
                    where mc.IsDelete == false && mc.Id == Id
                    select new VMMMedicalItemCategory
                    {
                        Id = mc.Id,
                        Name = mc.Name,
                        CreatedBy = mc.CreatedBy,
                        CreatedOn = mc.CreatedOn,
                        ModifiedBy = mc.ModifiedBy,
                        ModifiedOn = mc.ModifiedOn,
                        DeletedBy = mc.DeletedBy,
                        DeletedOn = mc.DeletedOn,
                        IsDelete = mc.IsDelete
                    }
                ).FirstOrDefault(); // Use FirstOrDefault to get a single item or null

                response.Data = data;

                response.Message = (response.Data != null)
                   ? $"{HttpStatusCode.OK} - Medical category successfully fetched"
                   : $"{HttpStatusCode.NoContent} - No medical category is found";

                response.StatusCode = (response.Data != null)
                    ? HttpStatusCode.OK
                    : HttpStatusCode.NoContent;
            }
            catch (Exception ex)
            {
                response.Message = $"{HttpStatusCode.InternalServerError} - {ex.Message}";
                response.StatusCode = HttpStatusCode.InternalServerError;
            }

            return response;
        }

    }
}

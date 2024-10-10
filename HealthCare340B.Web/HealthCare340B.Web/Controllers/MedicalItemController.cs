using HealthCare340B.ViewModel;
using HealthCare340B.Web.AddOns;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.Web.Controllers
{
    public class MedicalItemController : Controller
    {
        private readonly MedicalItemModel medItem;
        private readonly string imageFolder;
        private readonly int pageSize;

        public MedicalItemController(IConfiguration _config, IWebHostEnvironment _webHostEnv)
        {
            medItem = new MedicalItemModel(_config, _webHostEnv);
            imageFolder = _config["ImageFolder"];
            pageSize = int.Parse(_config["PageSize"]);
        }


        public async Task<IActionResult> SearchMedicalItem()
        {
            ViewBag.Title = "Cari Obat";

            var medItemCat = await medItem.GetAllCategory();
            ViewBag.MedicalItemCategory = medItemCat ?? new List<VMMMedicalItemCategory>();

            var medItemSeg = await medItem.GetAllSegementation();
            ViewBag.MedicalItemSegmentation = medItemSeg ?? new List<VMMMedicalItemSegmentation>();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResultSearchMedicalItem(VMMMedicalItem dataFilter, int? pageNumber, int? currentPageSize)
        {
            List<VMMMedicalItem>? data = await medItem.GetByFilter(
                dataFilter.MedicalItemCategoryId,
                dataFilter.isSegmentation,
                dataFilter.PriceMax,
                dataFilter.PriceMin,
                dataFilter.Name?.Trim(),
                dataFilter.Indication?.Trim()
            );

            if (data != null)
            {
                // Log or Debug the received data
                foreach (var item in data)
                {
                    Console.WriteLine($"Category: {item.CategoryName}, Segmentation: {item.SegmentationName}, PriceMax: {item.PriceMax}, PriceMin: {item.PriceMin}, Name : {item.Name}, Indication : {item.Indication}");
                }
            }

            ViewBag.MedicalItemCategoryId = dataFilter.MedicalItemCategoryId;
            if(ViewBag.MedicalItemCategoryId != null)
            {
                VMMMedicalItemCategory category = await medItem.GetById(ViewBag.MedicalItemCategoryId);
                ViewBag.CategoryName = category.Name;
            }
            ViewBag.MedicalItemCategoryId = dataFilter.MedicalItemCategoryId;
            ViewBag.Segmentation = dataFilter.isSegmentation;
            ViewBag.PriceMax = dataFilter.PriceMax;
            ViewBag.PriceMin = dataFilter.PriceMin;
            ViewBag.Name = dataFilter.Name;
            ViewBag.Indication = dataFilter.Indication;
            ViewBag.ImgFolder = imageFolder;
            ViewBag.PageSize = currentPageSize ?? 2;

            if (data == null || !data.Any())
            {
                ViewBag.Message = "Tidak ada medical Item ditemukan berdasarkan pencarian Anda.";
                data = new List<VMMMedicalItem>();
            }

            return View(Pagination<VMMMedicalItem>.Create(data ?? new List<VMMMedicalItem>(), pageNumber ?? 1, ViewBag.PageSize));
        }
        

    }
}

using HealthCare340B.DataModel;
using HealthCare340B.ViewModel;
using HealthCare340B.Web.AddOns;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HealthCare340B.Web.Controllers
{
    public class SpecializationController : Controller
    {
        private readonly SpecializationModel spesialisasi;
        private readonly int pageSize;
        
        public SpecializationController(IConfiguration _config)
        {
            spesialisasi = new SpecializationModel(_config);
            pageSize = int.Parse(_config["PageSize"]);
        }
        public async Task<IActionResult> Index(string? filter, int? pageNumber, int? currPageSize)
        {
            
            List<VMMSpecialization>? data = new List<VMMSpecialization>();
            
                data = (string.IsNullOrEmpty(filter)) ? await spesialisasi.getByFilter("") : await spesialisasi.getByFilter(filter);
           
            ViewBag.Title = "Spesialisasi Index";
            ViewBag.filter = filter;
            ViewBag.PageSize = (currPageSize ?? pageSize);

            return View(Pagination<VMMSpecialization>.Create(data ?? new List<VMMSpecialization>(), pageNumber ?? 1, ViewBag.PageSize));
        }
        public IActionResult Create()
        {
            ViewBag.Title = "New Spesialisasi";

            return View();
        }
        [HttpPost]
        public async Task<VMResponse<VMMSpecialization>?> CreateAsync(VMMSpecialization data)
        {
            return (await spesialisasi.CreateAsync(data));
        }
        public async Task<IActionResult> Edit(int id)
        {
            VMMSpecialization? data = await spesialisasi.getById(id);

            ViewBag.Title = "Spesialisasi Edit";
            return View(data);
        }
        [HttpPost]
        public async Task<VMResponse<VMMSpecialization>?> EditAsync(VMMSpecialization data)
        {
            return (await spesialisasi.UpdateAsync(data));
        }
        public IActionResult Delete(int id)
        {

            ViewBag.Title = "Delete Spesialisasi";

            return View(id);
        }
        [HttpPost]
        public async Task<VMResponse<VMMSpecialization>?> DeleteAsync(int id, int userId)
        {
            return (await spesialisasi.DeleteAsync(id, userId));
        }
    }
}

using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;
using HealthCare340B.ViewModel;


namespace HealthCare340B.Web.Controllers
{
    public class DoctorController : Controller
    {
        private readonly DokterModel dokter;

        public DoctorController(IConfiguration _config)
        {
            dokter = new DokterModel(_config);
        }
        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> CariDokter(VMMDoctor dataFilter)
        {
            List<VMMDoctor>? data = await dokter.GetByFilter(dataFilter);
            ViewBag.Title = "Cari Dokter";
            return View();
        }

        public IActionResult HasilCariDokter()
        {
            ViewBag.Title = "Hasil Cari Dokter";
            return View();
        }


    }
}

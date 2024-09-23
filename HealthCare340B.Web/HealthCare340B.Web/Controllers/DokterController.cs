using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;
using HealthCare340B.ViewModel;


namespace HealthCare340B.Web.Controllers
{
    public class DokterController : Controller
    {
        private readonly DokterModel dokter;

        public DokterController(IConfiguration _config)
        {
            dokter = new DokterModel(_config);
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult CariDokter()
        {
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

using HealthCare340B.ViewModel;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HealthCare340B.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string imageFolder;
        private readonly HomeModel home;
        public HomeController(ILogger<HomeController> logger, IConfiguration _config)
        {
            _logger = logger;
            imageFolder = _config["ImageFolder"];
            home = new HomeModel(_config);
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.imgFolder = imageFolder;
            List<VMMMenuRole> dataCoba = new List<VMMMenuRole>();
            try
            {
                dataCoba = await home.GetByFilter("");
                ViewBag.dataCoba = dataCoba;
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            return View(dataCoba);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HealthCare340B.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string imageFolder;

        public HomeController(ILogger<HomeController> logger, IConfiguration _config)
        {
            _logger = logger;
            imageFolder = _config["ImageFolder"];
        }

        public IActionResult Index()
        {
            ViewBag.imgFolder = imageFolder;

            return View();
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
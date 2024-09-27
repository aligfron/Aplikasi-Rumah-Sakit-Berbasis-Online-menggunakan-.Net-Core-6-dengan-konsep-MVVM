using HealthCare340B.ViewModel;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.Web.Controllers
{
    public class AppointmentController : Controller
    {
        private AppointmentModel appointment;
        private readonly string imageFolder;

        public AppointmentController(IConfiguration _config, IWebHostEnvironment _webHostEnv)
        {
            appointment = new AppointmentModel(_config);
            imageFolder = _config["ImageFolder"];
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create()
        {
            VMMDoctor? dataDoctor = await appointment.GetDoctor((long)2);
            ViewBag.Title = "Buat Janji";
            ViewBag.imgFolder = imageFolder;
            return View(dataDoctor);
        }

    }
}

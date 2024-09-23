using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.Web.Controllers
{
    public class ProfilController : Controller
    {
        public IActionResult Index()
        {
            // Mengirim role ke view
            ViewBag.Title = "Profil";
            ViewBag.Role = "ROLE_PASIEN";

            return View();
        }
    }
}

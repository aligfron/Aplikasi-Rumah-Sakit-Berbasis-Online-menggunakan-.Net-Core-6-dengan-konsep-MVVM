using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.Web.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

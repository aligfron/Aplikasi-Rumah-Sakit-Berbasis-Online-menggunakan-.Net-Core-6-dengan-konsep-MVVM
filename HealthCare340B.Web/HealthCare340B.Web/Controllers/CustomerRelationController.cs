using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.Web.Controllers
{
    public class CustomerRelationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

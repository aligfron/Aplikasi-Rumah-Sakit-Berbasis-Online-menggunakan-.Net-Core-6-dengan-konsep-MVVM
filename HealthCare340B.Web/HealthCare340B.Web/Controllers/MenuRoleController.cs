using HealthCare340B.ViewModel;
using HealthCare340B.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace HealthCare340B.Web.Controllers
{
    public class MenuRoleController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly string imageFolder;
        private readonly HomeModel menuRole;
        public MenuRoleController(IConfiguration _config)
        {
            
            //imageFolder = _config["ImageFolder"];
            menuRole = new HomeModel(_config);
        }
        public async Task<IActionResult> Index()
        {
           
            List<VMMMenuRole> dataMenu = new List<VMMMenuRole>();
            try
            {
                dataMenu = await menuRole.GetByFilter("");
                ViewBag.dataCoba = dataMenu;
            }
            catch (Exception ex)
            {
                HttpContext.Session.SetString("errMsg", ex.Message);
            }

            return View(dataMenu);
        }
    }
}

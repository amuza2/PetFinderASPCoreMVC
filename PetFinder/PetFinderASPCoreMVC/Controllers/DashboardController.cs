using Microsoft.AspNetCore.Mvc;

namespace PetFinderASPCoreMVC.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

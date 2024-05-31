using Microsoft.AspNetCore.Mvc;

namespace PetFinderASPCoreMVC.Controllers
{
    public class SignUpController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

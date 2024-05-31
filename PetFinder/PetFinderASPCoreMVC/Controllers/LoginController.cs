using Microsoft.AspNetCore.Mvc;

namespace PetFinderASPCoreMVC.Controllers
{
	public class LoginController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}

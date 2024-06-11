using Microsoft.AspNetCore.Mvc;
using PetFinderASPCoreMVC.Services;

namespace PetFinderASPCoreMVC.Controllers
{
    public class DashboardController : Controller
    {
        private readonly PetService _petService;
        public DashboardController()
        {
            _petService = new PetService("https://petfinderplatform-894-default-rtdb.firebaseio.com");
        }
        public async Task<IActionResult> Index()
        {
            var petsWithUsers = await _petService.GetPetsWithUsersAsync();
            return View(petsWithUsers);
        }

        public ActionResult Delete(int id)
        {
           
            return RedirectToAction("Index");
        }
    }
}

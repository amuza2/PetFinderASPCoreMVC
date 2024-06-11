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

        public async Task<IActionResult> Delete(string id)
        {
            await _petService.DeletePetAsync(id);
            return RedirectToAction("Index");
        }
    }
}

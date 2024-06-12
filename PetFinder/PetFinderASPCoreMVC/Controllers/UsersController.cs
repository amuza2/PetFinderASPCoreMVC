using Microsoft.AspNetCore.Mvc;
using PetFinderASPCoreMVC.Services;
using PetFinderASPCoreMVC.Utils;

namespace PetFinderASPCoreMVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserService _userService;
        public UsersController()
        {
            _userService = new UserService(Configs.FirebaseDbUrl);
        }
        public async Task<IActionResult> Index()
        {
            var petsWithUsers = await _userService.GetUsers();
            return View(petsWithUsers);
        }
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.DeleteUserAsync(id);
            return RedirectToAction("Index");
        }
    }
}

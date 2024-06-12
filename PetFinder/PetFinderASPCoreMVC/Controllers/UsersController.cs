using Microsoft.AspNetCore.Mvc;
using PetFinderASPCoreMVC.Services;
using PetFinderASPCoreMVC.Utils;
using ServiceReference1;

namespace PetFinderASPCoreMVC.Controllers
{
    public class UsersController : Controller
    {
        public DateTime services;
        public WebServiceSoapClient service;
        private readonly UserService _userService;
        public UsersController()
        {
            _userService = new UserService(Configs.FirebaseDbUrl);
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.services = DateTime.Now;
            var Users = await _userService.GetUsers();
            return View(Users);
        }
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.DeleteUserAsync(id);
            return RedirectToAction("Index");
        }
    }
}

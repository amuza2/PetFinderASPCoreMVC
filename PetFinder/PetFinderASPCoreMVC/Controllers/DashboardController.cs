using Microsoft.AspNetCore.Mvc;
using PetFinderASPCoreMVC.Services;

using ServiceReference1;
using System.Data.SqlTypes;

using PetFinderASPCoreMVC.Utils;

namespace PetFinderASPCoreMVC.Controllers
{
    public class DashboardController : Controller
    {
        public DateTime services;
        public WebServiceSoapClient service;
        private readonly PetService _petService;
        public DashboardController()
        {
            _petService = new PetService(Configs.FirebaseDbUrl);
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.services = DateTime.Now;
            var petsWithUsers = await _petService.GetPetsWithUsersAsync();
            return View(petsWithUsers);
        }

        public async Task<IActionResult> Delete(string id)
        {
            await _petService.DeletePetAsync(id);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> TogglePetStatus(string id)
        {
            var newStatus = await _petService.TogglePetStatus(id);
            return Json(new { success = true, status = newStatus });
        }
    }
}

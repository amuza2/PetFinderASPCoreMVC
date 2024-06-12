using Microsoft.AspNetCore.Mvc;
using PetFinderASPCoreMVC.Services;
using ServiceReference1;
using System.Data.SqlTypes;


namespace PetFinderASPCoreMVC.Controllers
{
    public class DashboardController : Controller
    {
        public DateTime services;
        public WebServiceSoapClient service;
        private readonly PetService _petService;
        public DashboardController()
        {
            _petService = new PetService("https://petfinderplatform-894-default-rtdb.firebaseio.com");
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
            await _petService.TogglePetStatus(id);
            return RedirectToAction("Index");
        }
    }
}

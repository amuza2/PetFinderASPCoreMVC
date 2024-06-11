using System.Diagnostics;
using Firebase.Auth;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetFinderASPCoreMVC.Models;
using PetFinderMAUI.Utils;

namespace PetFinderASPCoreMVC.Controllers
{
    public class SignUpController : Controller
    {
        private readonly ILogger<SignUpController> _logger;
        FirebaseAuthProvider _firebaseAuth;

        public SignUpController(ILogger<SignUpController> logger)
        {
            _logger = logger;
            _firebaseAuth = new FirebaseAuthProvider(new FirebaseConfig(Configs.WebApiKey));
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> RegisterUser(RegisterModel vm)
        {
            try
            {
                //create the user
                await _firebaseAuth.CreateUserWithEmailAndPasswordAsync(vm.EmailAddress, vm.Password);

                // Redirect to SignIn page after successful registration
                return RedirectToAction("Index", "Login");
            }
            catch (FirebaseAuthException ex)
            {
                var firebesex = JsonConvert.DeserializeObject<ErrorModel>(ex.RequestData);
                ModelState.AddModelError(string.Empty, firebesex.message);
                return View(vm);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
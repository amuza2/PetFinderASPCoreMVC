using System.Diagnostics;
using Firebase.Auth;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PetFinderASPCoreMVC.Models;
using PetFinderASPCoreMVC.Utils;

namespace PetFinderASPCoreMVC.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogger<LoginController> _logger;
        FirebaseAuthProvider _firebaseAuth;

        public LoginController(ILogger<LoginController> logger)
        {
            _logger = logger;
            _firebaseAuth = new FirebaseAuthProvider(new FirebaseConfig(Configs.WebApiKey));
        }

        public IActionResult Index()
        {
            return View();
        }


        public async Task<IActionResult> SignInUser(LoginModel vm)
        {
            try
            {
                var fribaselink = await _firebaseAuth.SignInWithEmailAndPasswordAsync(vm.EmailAddress, vm.Password);
                string accestoken = fribaselink.FirebaseToken;
                if (accestoken != null)
                {
                    HttpContext.Session.SetString("AccessToken", accestoken);
                    // Redirect to Dashboard page after successful login
                    return RedirectToAction("Index", "Dashboard");
                }
                else
                {
                    return View("Index",vm);
                }
            }
            catch (FirebaseAuthException ex)
            {
                var firebesex = JsonConvert.DeserializeObject<ErrorModel>(ex.RequestData);
                ModelState.AddModelError(string.Empty, firebesex.message);
                return View("Index",vm);
            }
        }
    }
}
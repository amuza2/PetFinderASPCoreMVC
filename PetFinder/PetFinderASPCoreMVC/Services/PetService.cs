using Firebase.Database;
using Firebase.Database.Query;
using PetFinderASPCoreMVC.Models;
namespace PetFinderASPCoreMVC.Services
{
    public class PetService
    {
        private readonly FirebaseClient _firebaseClient;
        public PetService(string firebaseDbUrl)
        {
            _firebaseClient = new FirebaseClient(firebaseDbUrl);
        }
        public async Task<User> GetUserByPublisherId(string key, string publisherId)
        {
            var user = await _firebaseClient.Child("Users").Child(key).Child(publisherId).OnceSingleAsync<User>();
            return user;
        }
        public async Task<List<PetWithUserViewModel>> GetPetsWithUsersAsync()
        {
            var pets = await _firebaseClient.Child("Pets").OnceAsync<Pet>();
            var petList = new List<PetWithUserViewModel>();

            foreach (var pet in pets)
            {
                var petObject = pet.Object;
                var petkey = pet.Key;
                petObject.PublisherId = pet.Object.PublisherId;
                User user = null;
                if (!string.IsNullOrEmpty(petObject.PublisherId))
                {
                    user = await GetUserByPublisherId(petkey, petObject.PublisherId);
                }
                if(user == null)
                {
                    user = new User { FullName = "Unknown", Username= "User" };
                }
                petList.Add(new PetWithUserViewModel
                {
                    Pet = petObject,
                    Publisher = user
                });
            }
            return petList;
        }
        
    }
}

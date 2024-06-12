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
        public async Task<User> GetUserByPublisherId(string publisherId)
        {
            var user = await _firebaseClient.Child("Users").Child(publisherId).OnceSingleAsync<User>();
            return user;
        }
        public async Task<List<PetWithUserViewModel>> GetPetsWithUsersAsync()
        {
            var pets = await _firebaseClient.Child("Pets").OnceAsync<Pet>();
            var petList = new List<PetWithUserViewModel>();

            foreach (var pet in pets)
            {
                var petObject = pet.Object;
                var petKey = pet.Key;
                petObject.PetId = petKey;
                Console.WriteLine($"Pet ID: {petObject.PetId}, Publisher ID: {petObject.PublisherId}");
                User user = null;
                if (!string.IsNullOrEmpty(petObject.PublisherId))
                {
                    user = await GetUserByPublisherId(petObject.PublisherId);
                }
                if(user == null)
                {
                    user = new User { FullName = "Unknown", Username= "User" };
                }
                Console.WriteLine($"User Full Name: {user.FullName}, Username: {user.Username}");
                petList.Add(new PetWithUserViewModel
                {
                    Pet = petObject,
                    Publisher = user
                });
            }
            return petList;
        }
        // delete pet
        public async Task DeletePetAsync(string petId)
        {
            await _firebaseClient.Child("Pets").Child(petId).DeleteAsync();
        }
        // status toggle button
        public async Task<string> TogglePetStatus(string petId)
        {
            var pets = await _firebaseClient.Child("Pets").OnceAsync<Pet>();
            foreach (var pet in pets)
            {
                Pet petObj = pet.Object;
                if(petObj.PetId == petId)
                {
                    if (petObj.PetStatus == "false")
                        petObj.PetStatus = "true";
                    else
                        petObj.PetStatus = "false";
                    
                    await _firebaseClient.Child("Pets").Child(pet.Key).PutAsync(petObj);
                    return petObj.PetStatus;
                }
            }
            return "false";
        }
    }
}

using Firebase.Database;
using Firebase.Database.Query;
using PetFinderASPCoreMVC.Models;

namespace PetFinderASPCoreMVC.Services
{
    public class UserService
    {
        private readonly FirebaseClient _firebaseClient;
        public UserService(string firebaseDbUrl)
        {
            _firebaseClient = new FirebaseClient(firebaseDbUrl);
        }
        public async Task<List<UsersViewModel>> GetUsers()
        {
            var users = await _firebaseClient.Child("Users").OnceAsync<PetFinderASPCoreMVC.Models.User>();
            var usersList = new List<UsersViewModel>();
            foreach (var user in users)
            {
                var userObject = user.Object;
                usersList.Add(new UsersViewModel { Publisher = userObject });
            }
            return usersList;
        }
        // delete user
        public async Task DeleteUserAsync(string userId)
        {
            await _firebaseClient.Child("Users").Child(userId).DeleteAsync();
        }
    }
}

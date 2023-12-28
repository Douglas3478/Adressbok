using Adressbok.Models;

namespace Adressbok.Service

{

    /// <summary>
    /// Service for managing user data, including adding, retrieving, and removing users.
    /// </summary>
    /// 

    public class UserService
    {
        private readonly List<User> _users = new List<User>();
        private readonly FileService _fileService;

        /// <summary>
        /// Initializes a new instance of the UserService class.
        /// </summary>
        /// 

        public UserService() 
        {
            _fileService = new FileService();
            _users = _fileService.LoadUsersFromFile();
        }

        /// <summary>
        /// Adds a user to the list if the user's email is not already present.
        /// </summary>
        /// <param name="user">The user to be added.</param>
        /// 

        public void AddUserToList(User user)
        {
            if (!_users.Any(x => x.Email == user.Email))
            {
                _users.Add(user);
                _fileService.SaveUsersToFile(_users);

            }
                
        }

        /// <summary>
        /// Retrieves a user from the list based on the provided email.
        /// </summary>
        /// <param name="email">The email of the user to retrieve.</param>
        /// <returns>The user with the specified email, or null if not found.</returns>
        /// 

        public User GetUserFromList(string email)
        {
            var user = _users.FirstOrDefault(x => x.Email == email);
            return user ??= null!;
        }

        /// <summary>
        /// Retrieves all users from the list.
        /// </summary>
        /// <returns>A list of all users.</returns>
        /// 

        public List<User> GetUsersFromList()
        {
            return _users;
        }

        /// <summary>
        /// Removes a user from the list based on the provided email.
        /// </summary>
        /// <param name="email">The email of the user to remove.</param>
        /// <returns>True if the user was removed, false if the user was not found.</returns>
        /// 

        public bool RemoveUserByEmail(string email)
        {
            User userToRemove = _users.Find(u => u.Email == email);

            if (userToRemove != null)
            {
                _users.Remove(userToRemove);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Shows users in the list matching the provided first and last names.
        /// </summary>
        /// <param name="firstName">The first name to match.</param>
        /// <param name="lastName">The last name to match.</param>
        /// <returns>A list of users matching the specified first and last names.</returns>
        /// 

        public List<User> ShowUserMenu(string firstName, string lastName)
        {
            return _users.Where(u => u.FirstName.Equals(firstName, StringComparison.OrdinalIgnoreCase) &&
                                     u.LastName.Equals(lastName, StringComparison.OrdinalIgnoreCase))
                         .ToList();
        }
    }
}


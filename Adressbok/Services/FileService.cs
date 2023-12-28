using System.Text.Json;
using Adressbok.Models;

namespace Adressbok.Service
{
    /// <summary>
    /// Service for handling file operations related to user data.
    /// </summary>
    /// 

    public class FileService
    {
        private const string FilePath = @"C:\Education\Csharp\Solution\ConsoleApp\Adressbok\users.json";

        /// <summary>
        /// Loads user data from the specified file.
        /// </summary>
        /// <returns>A list of users loaded from the file.</returns>
        /// 

        public List<User> LoadUsersFromFile()
        {
            if (File.Exists(FilePath))
            {
                var json = File.ReadAllText(FilePath);
                return JsonSerializer.Deserialize<List<User>>(json);
            }
            return new List<User>();
        }

        /// <summary>
        /// Saves the provided list of users to the specified file.
        /// </summary>
        /// <param name="users">The list of users to be saved to the file.</param>
        /// 

        public void SaveUsersToFile(List<User> users)
        {
            var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }
    }
}


using Adressbok.Models;

namespace Adressbok.Service;

    /// <summary>
    /// Service for handling user interactions through console menu.
    /// </summary>
    /// 

public class MenuService
{
    private readonly UserService _userService = new UserService();

    /// <summary>
    /// Displays the main menu and shows user option based on input key.
    /// </summary>
    /// 

    public void ShowMainMenu()
    {
        while (true)
        { 
            Console.Clear();
            Console.WriteLine("# MENY #");
            Console.WriteLine();
            Console.WriteLine("1. Lägg till en användare");
            Console.WriteLine("2. Visa alla användare");
            Console.WriteLine("3. Ta bort en användare");
            Console.WriteLine("4. Sök på en användare");

            var option = Console.ReadLine();

            switch (option)
            { 
                case "1":
                    ShowAddMenu();
                    break;
                case "2":
                    ShowAllMenu();
                    break;
                case "3":
                    RemoveUserMenu();
                    break;
                case "4":
                    ShowUserMenu();
                    break;
            }
            Console.ReadKey();
        }
    }

    /// <summary>
    /// Displays a menu to add a new user and adds the user to the list.
    /// </summary>
    /// 

    private void ShowAddMenu()
    {
        var user = new User();

        Console.Clear();
        Console.Write("Ange förnamn: ");
        user.FirstName = Console.ReadLine()!;

        Console.Write("Ange efternamn: ");
        user.LastName = Console.ReadLine()!;

        Console.Write("Ange e-postadress: ");
        user.Email = Console.ReadLine()!;

        Console.Write("Ange telefonnummer: ");
        user.PhoneNumber = Console.ReadLine()!;

        Console.Write("Ange din Adress: ");
        user.HomeAdress = Console.ReadLine()!;

        Console.Write("Ange din Stad: ");
        user.City = Console.ReadLine()!;



        _userService.AddUserToList(user);
    }

    /// <summary>
    /// Displays a menu to show all registered users.
    /// </summary>

    private void ShowAllMenu()
    {
        var users = _userService.GetUsersFromList();
        
        if (_userService.GetUsersFromList().Count == 0)
        {
            Console.WriteLine("Det finns inga registrerade användare, bli den första!");
            return;
        }
        
        foreach (var user in users)
        {
            Console.WriteLine($"{user.FirstName} {user.LastName} <{user.City}>");
        }
    }

    /// <summary>
    /// A menu to remove a user by writing their email address.
    /// </summary>
    /// 

    private void RemoveUserMenu()
    {
        Console.Clear();
        
        if (_userService.GetUsersFromList().Count == 0)
        {
            Console.WriteLine("Det finns inga användare att ta bort.");
            return;
        }
        
        Console.Write("Enter the user's email address to remove: ");
        string emailToRemove = Console.ReadLine() ?? string.Empty;

        bool userRemoved = _userService.RemoveUserByEmail(emailToRemove);
        
        if (userRemoved)
        {
            Console.WriteLine("The user has been removed!");
        }
        
        else
        {
            Console.WriteLine("User not found or could not be removed.");
        }
    }

    /// <summary>
    /// A menu to show details of a specific user by writing their first and last names.
    /// </summary>
    /// 

    private void ShowUserMenu()
    {
        
        Console.Clear();
        Console.Write("Förnamn på användare: ");
        string firstName = Console.ReadLine() ?? string.Empty;

        Console.Write("Efternamn på användare: ");
        string lastName = Console.ReadLine() ?? string.Empty;

        var foundUsers = _userService.ShowUserMenu(firstName, lastName);

        
        if (foundUsers.Count > 0)
        {
            Console.WriteLine("Användare hittades: ");
            foreach (var user in foundUsers)
            {
                Console.WriteLine($"{user.FirstName} {user.LastName} <{user.Email}> <{user.PhoneNumber}> <{user.HomeAdress}> <{user.City}>");
            }
        }
        
        else
        {
            Console.WriteLine("Ingen användare med det namnet hittades. ");
        }
    }
}


using Adressbok.Models;
using Adressbok.Service;

public class UserServiceTests
{
    [Fact]
    public void AddUserToList_ShouldAddUser()
    {
        // Arrange
        var userService = new UserService();
        var user = new User { FirstName = "Douglas", LastName = "Hanell", Email = "D.Hanell@example.com" };

        // Act
        userService.AddUserToList(user);

        // Assert
        _ = userService.GetUsersFromList();
    }

    [Fact]
    public void GetUserFromList_ShouldReturnUserByEmail()
    {
        // Arrange
        var userService = new UserService();
        var user = new User { FirstName = "Moa", LastName = "Hansson", Email = "M.Hansson@example.com" };
        userService.AddUserToList(user);

        // Act
        User retrievedUser = userService.GetUserFromList("M.Hansson@example.com");

        // Assert
        Assert.Equal(user, retrievedUser, new UserEqualityComparer());
    }

    [Fact]
    public void RemoveUserByEmail_ShouldRemoveUser()
    {
        // Arrange
        var userService = new UserService();
        var user = new User { FirstName = "Dogge", LastName = "Nator", Email = "Dogge.Nator@example.com" };
        userService.AddUserToList(user);

        // Act
        bool removalResult = userService.RemoveUserByEmail("Dogge.Nator@example.com");

        // Assert
        Assert.True(removalResult);
        Assert.DoesNotContain(user, userService.GetUsersFromList());
    }

    [Fact]
    public void ShowUserMenu_ShouldReturnMatchingUsers()
    {
        // Arrange
        var userService = new UserService();
        var user1 = new User { FirstName = "Dogge", LastName = "Karlsson", Email = "Dogge.Karlsson@example.com" };
        var user2 = new User { FirstName = "Dogge", LastName = "Johansson", Email = "Dogge.Johansson@example.com" };
        userService.AddUserToList(user1);
        userService.AddUserToList(user2);

        // Act
        List<User> matchingUsers = userService.ShowUserMenu("Dogge", "Johansson");

        // Assert
        Assert.Single(matchingUsers);
        Assert.Equal(user2, matchingUsers[0], new UserEqualityComparer());
    }
}

// Custom equality comparer for User class
public class UserEqualityComparer : IEqualityComparer<User>
{
    public bool Equals(User x, User y)
    {
        if (ReferenceEquals(x, y)) return true;
        if (ReferenceEquals(x, null) || ReferenceEquals(y, null)) return false;

        return x.FirstName == y.FirstName &&
               x.LastName == y.LastName &&
               x.Email == y.Email;
    }

    public int GetHashCode(User obj)
    {
        return HashCode.Combine(obj.FirstName, obj.LastName, obj.Email);
    }
}
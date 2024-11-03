using BreadChat.Domain.Abstractions;

namespace BreadChat.Domain.Entities;

public class User : IEntity
{
    public Guid Id { get; private set; }

    public string Username { get; private set; }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    private User()
    {
    }
    
    public static User Create(string username, string firstName, string lastName)
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Username = username,
            FirstName = firstName,
            LastName = lastName
        };
    }
    
    public void Update(string username, string firstName, string lastName)
    {
        Username = username;
        FirstName = firstName;
        LastName = lastName;
    }
}
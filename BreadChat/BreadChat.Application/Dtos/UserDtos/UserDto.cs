using BreadChat.Domain.Entities;

namespace BreadChat.Application.Dtos;

public class UserDto
{
    public Guid Id { get; private set; }

    public string Username { get; private set; }

    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    
    public static UserDto FromDomain(User user)
    {
        return new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }
}
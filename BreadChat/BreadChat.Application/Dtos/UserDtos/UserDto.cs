using BreadChat.Domain.Entities;

namespace BreadChat.Application.Dtos.UserDtos;

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
    
    public static UserDto FromDbEntity(User user)
    {
        return new UserDto()
        {
            Id = user.Id,
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }
}
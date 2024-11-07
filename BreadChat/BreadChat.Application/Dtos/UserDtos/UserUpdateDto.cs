using BreadChat.Domain.Entities;

namespace BreadChat.Application.Dtos.UserDtos;

public class UserUpdateDto
{
    public string? Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    
    public UserUpdate ToDomain()
    {
        return new UserUpdate()
        {
            Username = Username,
            FirstName = FirstName,
            LastName = LastName
        };
    }
}
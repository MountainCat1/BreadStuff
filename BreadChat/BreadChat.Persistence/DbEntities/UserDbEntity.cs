using BreadChat.Domain.Entities;

namespace BreadChat.Persistence.DbEntities;

public class UserDbEntity
{
    public Guid Id { get; init; }

    public string Username { get; init; } = null!;
    public string FirstName { get; init; } = null!;
    public string LastName { get; init; } = null!;

    private UserDbEntity()
    {
    }

    public static UserDbEntity FromDomain(User user)
    {
        return new UserDbEntity
        {
            Id = user.Id,
            Username = user.Username,
            FirstName = user.FirstName,
            LastName = user.LastName
        };
    }
}
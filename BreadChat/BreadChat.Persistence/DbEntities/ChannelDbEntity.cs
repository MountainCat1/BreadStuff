using BreadChat.Domain.Entities;

namespace BreadChat.Persistence.DbEntities;

public class ChannelDbEntity
{
    public Guid Id { get; init; }
    public string Name { get; init; } = null!;
    public string Description { get; init; } = null!;
    
    public virtual List<UserDbEntity> Users { get; init; } = null!;

    private ChannelDbEntity()
    {
    }
    
    public static ChannelDbEntity FromDomain(Channel channel)
    {
        return new ChannelDbEntity()
        {
            Id = channel.Id,
            Name = channel.Name,
            Description = channel.Description,
            Users = channel.Users.Select(x => UserDbEntity.FromDomain(x)).ToList(),
        };
    }
}
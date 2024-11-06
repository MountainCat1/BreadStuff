using BreadChat.Domain.Abstractions;

namespace BreadChat.Domain.Entities;

public class Channel : IEntity
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public virtual List<ChannelMembership> Members { get; private set; } = null!; 

    public static Channel Create(string name, string description)
    {
        return new Channel()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            Members = new List<ChannelMembership>()
        };
    }
    
    public void Update(ChannelUpdate update)
    {
        Name = update.Name ?? Name;
        Description = update.Description ?? Description;
    }
}

public class ChannelUpdate
{
    public string? Name { get; init; }
    public string? Description { get; init; }
}
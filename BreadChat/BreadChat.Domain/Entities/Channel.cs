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

    public ChannelMembership AddMember(User user)
    {
        var membership = ChannelMembership.Create(this, user);
        
        Members.Add(membership);

        return membership;
    }
    
    public ChannelMembership RemoveMember(User user)
    {
        var membership = Members.FirstOrDefault(x => x.User == user);
        
        Members.Remove(membership);

        return membership;
    }

    public Message SendMessage(Message message)
    {
        if(message.ChannelId != Id)
            throw new InvalidOperationException("Message must be sent to the channel it was created in.");
        
        return message;
    }
}

public class ChannelUpdate
{
    public string? Name { get; init; }
    public string? Description { get; init; }
}
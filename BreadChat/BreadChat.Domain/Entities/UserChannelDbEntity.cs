namespace BreadChat.Domain.Entities;

public class ChannelMembership
{
    public Guid UserId { get; set; }
    public Guid ChannelId { get; set; }
    
    public virtual User User { get; set; } = null!;
    public virtual Channel Channel { get; set; } = null!;
}
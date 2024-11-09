namespace BreadChat.Domain.Entities;

public class ChannelMembership
{
    public Guid UserId { get; private set; }
    public Guid ChannelId { get; private set; }
    public virtual User User { get; private set; } = null!;
    public virtual Channel Channel { get; private set; } = null!;

    private ChannelMembership()
    {
    }

    internal static ChannelMembership Create(Channel channel, User user)
    {
        return new ChannelMembership()
        {
            User = user,
            Channel = channel,
            UserId = user.Id,
            ChannelId = channel.Id
        };
    }
}
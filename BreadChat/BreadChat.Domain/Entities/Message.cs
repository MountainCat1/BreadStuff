namespace BreadChat.Domain.Entities;

public class Message
{
    public Guid Id { get; private set; }
    public string Content { get; private set; }
    public Guid ChannelId { get; private set; }
    public Guid AuthorId { get; private set; }
    public virtual Channel Channel { get; private set; } = null!;
    public virtual User Author { get; private set; } = null!;

    private Message()
    {
    }

    internal static Message Create(Channel channel, User user, string content)
    {
        return new Message()
        {
            Id = Guid.NewGuid(),
            Content = content,
            ChannelId = channel.Id,
            AuthorId = user.Id
        };
    }
}
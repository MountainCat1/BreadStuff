namespace BreadChat.Domain.Entities;

public class Message
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public Guid ChannelId { get; set; }
    public Guid AuthorId { get; set; }
    public virtual Channel Channel { get; set; } = null!;
    public virtual User Author { get; set; } = null!;

    private Message()
    {
    }

    public static Message Create(Guid channelId, Guid authorId, string text)
    {
        return new Message()
        {
            Id = Guid.NewGuid(),
            Content = text,
            ChannelId = channelId,
            AuthorId = authorId
        };
    }
}
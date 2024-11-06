namespace BreadChat.Domain.Entities;

public class Message
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    
    
    public Guid ChannelId { get; set; }
    public virtual Channel Channel { get; set; } = null!;

    private Message()
    {
    }

    public static Message Create(Guid channelId, string text)
    {
        return new Message()
        {
            Id = Guid.NewGuid(),
            ChannelId = channelId,
            Content = text,
        };
    }
}
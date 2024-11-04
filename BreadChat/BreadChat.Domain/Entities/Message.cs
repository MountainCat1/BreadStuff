namespace BreadChat.Domain.Entities;

public class Message
{
    public Guid Id { get; set; }
    public string Text { get; set; }

    private Message()
    {
    }

    public static Message Create(string text)
    {
        return new Message()
        {
            Id = Guid.NewGuid(),
            Text = text,
        };
    }
}
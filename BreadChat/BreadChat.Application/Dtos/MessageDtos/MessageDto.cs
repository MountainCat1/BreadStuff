using BreadChat.Domain.Entities;

namespace BreadChat.Application.Dtos.MessageDtos;

public class MessageDto
{
    public Guid Id { get; private set; }
    public Guid AuthorId { get; private set; }
    public string Text { get; private set; }
    
    public static MessageDto FromDomain(Message message)
    {
        return new MessageDto()
        {
            Id = message.Id,
            AuthorId = message.AuthorId,
            Text = message.Content
        };
    }
}
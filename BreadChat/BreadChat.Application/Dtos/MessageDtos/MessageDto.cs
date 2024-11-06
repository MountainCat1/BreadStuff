using BreadChat.Domain.Entities;

namespace BreadChat.Application.Dtos.MessageDtos;

public class MessageDto
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    
    public static MessageDto FromDomain(Message message)
    {
        return new MessageDto()
        {
            Id = message.Id,
            Text = message.Content,
        };
    }
}
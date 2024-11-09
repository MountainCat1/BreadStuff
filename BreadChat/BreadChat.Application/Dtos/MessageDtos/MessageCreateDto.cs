namespace BreadChat.Application.Dtos.MessageDtos;

public class MessageCreateDto
{
    public string Text { get; set; }
    public Guid AuthorId { get; set; }
}
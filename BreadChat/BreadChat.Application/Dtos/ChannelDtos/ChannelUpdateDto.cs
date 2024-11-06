using BreadChat.Domain.Entities;

namespace BreadChat.Application.Dtos.ChannelDtos;

public class ChannelUpdateDto
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    
    public ChannelUpdate ToDomain()
    {
        return new ChannelUpdate()
        {
            Name = Name,
            Description = Description
        };
    }
}
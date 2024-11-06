using BreadChat.Domain.Entities;

namespace BreadChat.Application.Dtos.ChannelDtos;

public class ChannelDto
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }
    public string Description { get; private set; }
    public List<UserDto> Users { get; private set; }

    public static ChannelDto FromDomain(Channel channel)
    {
        return new ChannelDto()
        {
            Id = channel.Id,
            Name = channel.Name,
            Description = channel.Description,
            Users = channel.Members.Select(x => UserDto.FromDomain(x.User)).ToList(),
        };
    }
    
    public static ChannelDto FromDbEntity(Channel channel)
    {
        return new ChannelDto()
        {
            Id = channel.Id,
            Name = channel.Name,
            Description = channel.Description,
            Users = channel.Members.Select(x => UserDto.FromDomain(x.User)).ToList(),
        };
    }
}
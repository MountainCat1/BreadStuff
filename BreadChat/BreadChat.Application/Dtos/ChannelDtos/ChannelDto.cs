using BreadChat.Domain.Entities;
using BreadChat.Persistence.DbEntities;

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
            Users = channel.Users.Select(x => UserDto.FromDomain(x)).ToList(),
        };
    }
    
    public static ChannelDto FromDbEntity(ChannelDbEntity channel)
    {
        return new ChannelDto()
        {
            Id = channel.Id,
            Name = channel.Name,
            Description = channel.Description,
            Users = channel.Users.Select(x => UserDto.FromDbEntity(x)).ToList(),
        };
    }
}
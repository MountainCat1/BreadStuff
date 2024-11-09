using BreadChat.Domain.Entities;

namespace BreadChat.Application.Dtos.ChannelMembershipDtos;

public class ChannelMembershipDto
{
    public Guid UserId { get; private set; }
    public Guid ChannelId { get; private set; }
    
    public static ChannelMembershipDto FromDomain(ChannelMembership membership)
    {
        return new ChannelMembershipDto()
        {
            UserId = membership.UserId,
            ChannelId = membership.ChannelId
        };
    }
}
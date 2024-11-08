using BreadChat.Application.ApplicationErrors;
using BreadChat.Domain.Entities;
using BreadChat.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BreadChat.Application.Services;

public interface IMembershipService
{
    Task AddMemberAsync(Guid channelId, Guid userId);
}

public class MembershipService : IMembershipService
{
    private IBreadChatDbContext _dbContext;

    public MembershipService(IBreadChatDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // public async Task<List<ChannelMembership>> GetMembersAsync(Guid channelId)
    // {
    //     var channel = await _dbContext.Channels
    //         .Include(x => x.Members)
    //         .FirstOrDefaultAsync(x => x.Id == channelId);
    //     
    //     if (channel is null)
    //     {
    //         throw new NotFoundError($"Channel with id {channelId} not found ");
    //     }
    // }
    
    public async Task AddMemberAsync(Guid channelId, Guid userId)
    {
        var channel = await _dbContext.Channels
            .Include(x => x.Members)
            .FirstOrDefaultAsync(x => x.Id == channelId);
        
        if (channel is null)
        {
            throw new NotFoundError($"Channel with id {channelId} not found ");
        }
        
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == userId);

        if (user is null)
        {
            throw new NotFoundError($"User with id {userId} not found ");
        }
        
        var membership = channel.AddMember(user);
        
        _dbContext.ChannelMemberships.Add(membership);

        await _dbContext.SaveChangesAsync();
    }
}
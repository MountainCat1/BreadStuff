using BreadChat.Application.ApplicationErrors;
using BreadChat.Application.Dtos.ChannelDtos;
using BreadChat.Domain.Entities;
using BreadChat.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BreadChat.Application.Services;

public interface IChannelService
{
    public Task<ChannelDto> CreateChannelAsync(string name, string description);
    public Task<ChannelDto> GetChannelAsync(Guid id);
    public Task<ChannelDto> DeleteChannelAsync(Guid id);
}

public class ChannelService : IChannelService
{
    private IBreadChatDbContext _dbContext;

    public ChannelService(IBreadChatDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ChannelDto> CreateChannelAsync(string name, string description)
    {
        var channel = Channel.Create(name, description);

        _dbContext.Channels.Add(channel);

        await _dbContext.SaveChangesAsync();

        return ChannelDto.FromDbEntity(channel);
    }

    public async Task<ChannelDto> GetChannelAsync(Guid id)
    {
        var channelDbEntity = await _dbContext.Channels
            .Include(x => x.Users)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (channelDbEntity is null)
        {
            throw new NotFoundError($"Channel with id {id} not found");
        }
        
        return ChannelDto.FromDbEntity(channelDbEntity);
    }

    public async Task<ChannelDto> DeleteChannelAsync(Guid id)
    {
        var channelDbEntity = await _dbContext.Channels
            .Include(x => x.Users)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (channelDbEntity is null)
        {
            throw new NotFoundError($"Channel with id {id} not found");
        }

        _dbContext.Channels.Remove(channelDbEntity);
        await _dbContext.SaveChangesAsync();
        
        return ChannelDto.FromDbEntity(channelDbEntity);
    }
}
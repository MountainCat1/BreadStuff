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
        var channel = Channel.Create(name, description); // create domain entity

        _dbContext.Channels.Add(channel); // add to db context

        await _dbContext.SaveChangesAsync(); // save changes

        return ChannelDto.FromDbEntity(channel); // convert to dto
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
}
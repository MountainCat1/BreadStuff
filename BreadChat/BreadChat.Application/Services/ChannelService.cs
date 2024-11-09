using BreadChat.Application.ApplicationErrors;
using BreadChat.Application.Dtos;
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
    public Task<ChannelDto> UpdateChannelAsync(Guid id, ChannelUpdateDto updateDto);
    Task<PageDto<ChannelDto>> GetChannelsAsync(int pageNumber, int pageSize);
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
            .Include(x => x.Members)
            .FirstOrDefaultAsync(x => x.Id == id);

        if (channelDbEntity is null)
        {
            throw new NotFoundError($"Channel with id {id} not found");
        }

        _dbContext.Channels.Remove(channelDbEntity);
        await _dbContext.SaveChangesAsync();
        
        return ChannelDto.FromDbEntity(channelDbEntity);
    }

    public async Task<ChannelDto> UpdateChannelAsync(Guid id, ChannelUpdateDto updateDto)
    {
        var channelEntity = await _dbContext.Channels
            .Include(x => x.Members)
            .FirstOrDefaultAsync(x => x.Id == id);
        
        if (channelEntity is null)
            throw new NotFoundError($"Channel with id {id} not found");

        channelEntity.Update(updateDto.ToDomain());

        _dbContext.Channels.Update(channelEntity);
        
        await _dbContext.SaveChangesAsync();
        
        return ChannelDto.FromDbEntity(channelEntity);
    }

    public async Task<PageDto<ChannelDto>> GetChannelsAsync(int pageNumber, int pageSize)
    {
        var channels = await _dbContext.Channels.Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();
        var channelCount = await _dbContext.Channels.CountAsync();

        var userDtos = channels.Select(x => ChannelDto.FromDomain(x)).ToList();

        return new PageDto<ChannelDto>(userDtos, pageNumber, pageSize, channelCount);
    }
}
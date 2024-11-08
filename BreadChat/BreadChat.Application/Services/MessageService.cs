﻿using BreadChat.Application.ApplicationErrors;
using BreadChat.Application.Dtos.ChannelDtos;
using BreadChat.Application.Dtos.MessageDtos;
using BreadChat.Domain.Entities;
using BreadChat.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BreadChat.Application.Services;

public interface IMessageService
{
    public Task<MessageDto> CreateMessageAsync(Guid channelId, string text);
    public Task<MessageDto> GetMessageAsync(Guid channelId, Guid messageId);

    public Task<MessageDto> DeleteMessageAsync(Guid channelId, Guid messageId);
}
public class MessageService : IMessageService

{
    private IBreadChatDbContext _dbContext;

    public MessageService(IBreadChatDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MessageDto> CreateMessageAsync(Guid channelId, string text)
    {
        var message = Message.Create(channelId, text);

        _dbContext.Messages.Add(message);

        await _dbContext.SaveChangesAsync();

        return MessageDto.FromDomain(message);
    }

    public async Task<MessageDto> GetMessageAsync(Guid channelId, Guid id)
    {
        var message = await _dbContext.Messages.FirstOrDefaultAsync(x => x.ChannelId == channelId && x.Id == id);

        if (message is null)
        {
            throw new NotFoundError($"Message with id {id} not found in a channel with id {channelId}");
        }


        return MessageDto.FromDomain(message);
    }
    public async Task<MessageDto> DeleteMessageAsync(Guid channelId, Guid id)
    {
        var message = await _dbContext.Messages.FirstOrDefaultAsync(x => x.ChannelId == channelId && x.Id == id);

        if (message is null)
        {
            throw new NotFoundError($"Message with id {id} not found in a channel with id {channelId}");
        }
        
        _dbContext.Messages.Remove(message);
        await _dbContext.SaveChangesAsync();

        return MessageDto.FromDomain(message);
    }
}
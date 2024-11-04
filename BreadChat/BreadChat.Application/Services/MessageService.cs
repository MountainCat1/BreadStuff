using BreadChat.Application.ApplicationErrors;
using BreadChat.Application.Dtos.MessageDtos;
using BreadChat.Domain.Entities;
using BreadChat.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BreadChat.Application.Services;

public interface IMessageService
{
    public Task<MessageDto> CreateMessageAsync(string text);
    public Task<MessageDto> GetMessageAsync(Guid id);
}

public class MessageService : IMessageService
{
    private IBreadChatDbContext _dbContext;

    public MessageService(IBreadChatDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<MessageDto> CreateMessageAsync(string text)
    {
        var message = Message.Create(text);

        _dbContext.Messages.Add(message);

        await _dbContext.SaveChangesAsync();

        return MessageDto.FromDomain(message);
    }

    public async Task<MessageDto> GetMessageAsync(Guid id)
    {
        var message = await _dbContext.Messages.FirstOrDefaultAsync(x => x.Id == id);

        if (message is null)
        {
            throw new NotFoundError($"Message with id {id} not found");
        }

        return MessageDto.FromDomain(message);
    }
}
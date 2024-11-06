using BreadChat.Application.Dtos.MessageDtos;
using BreadChat.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BreadChat.Controllers;

[ApiController]
[Route("/api/{channelId:guid}/messages")]
public class ChannelMessagesController : Controller
{
    private IMessageService _messageService;

    public ChannelMessagesController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMessage([FromRoute] Guid channelId,  MessageCreateDto createDto)
    {
        var message = await _messageService.CreateMessageAsync(channelId, createDto.Text);

        return Ok(message);
    }
    
    [HttpGet("{messageId}")]
    public async Task<IActionResult> GetMessage([FromRoute] Guid channelId, [FromRoute] Guid messageId)
    {
        var message = await _messageService.GetMessageAsync(channelId, messageId);

        return Ok(message);
    }

    [HttpDelete("{messageId}")]
    public async Task<IActionResult> DeleteMessage([FromRoute] Guid channelId, [FromRoute] Guid messageId)
    {
        var message = await _messageService.DeleteMessageAsync(channelId, messageId);

        return Ok(message);
}
    }

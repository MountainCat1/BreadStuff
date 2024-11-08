using BreadChat.Application.Dtos;
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
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateMessage([FromRoute] Guid channelId, [FromBody] MessageCreateDto createDto)
    {
        await _messageService.CreateMessageAsync(channelId, createDto.AuthorId, createDto.Text);

        return Ok();
    }

    [HttpGet("{messageId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMessage([FromRoute] Guid channelId, [FromRoute] Guid messageId)
    {
        var message = await _messageService.GetMessageAsync(channelId, messageId);

        return Ok(message);
    }

    [HttpDelete("{messageId}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(MessageDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteMessage([FromRoute] Guid channelId, [FromRoute] Guid messageId)
    {
        var message = await _messageService.DeleteMessageAsync(channelId, messageId);

        return Ok(message);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(PageDto<MessageDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetChannelMessages([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var page = await _messageService.GetMessagesAsync(pageNumber, pageSize);

        return Ok(page);
    }
}
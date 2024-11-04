using BreadChat.Application.Dtos.MessageDtos;
using BreadChat.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BreadChat.Controllers;

[ApiController]
[Route("/api/messages")]
public class MessageController : Controller
{
    private IMessageService _messageService;

    public MessageController(IMessageService messageService)
    {
        _messageService = messageService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateMessage(MessageCreateDto createDto)
    {
        var message = await _messageService.CreateMessageAsync(createDto.Text);

        return Ok(message);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetMessage([FromRoute] Guid id)
    {
        var message = await _messageService.GetMessageAsync(id);

        return Ok(message);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMessage([FromRoute] Guid id)
    {
        var message = await _messageService.DeleteMessageAsync(id);

        return Ok(message);
    }
}
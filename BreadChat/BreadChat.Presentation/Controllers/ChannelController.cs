using BreadChat.Application.Dtos.ChannelDtos;
using BreadChat.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BreadChat.Controllers;

[ApiController]
[Route("api/channels")]
public class ChannelController : Controller
{
    private IChannelService _channelService;

    public ChannelController(IChannelService channelService)
    {
        _channelService = channelService;
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetChannel([FromRoute] Guid id)
    {
        var channel = await _channelService.GetChannelAsync(id);

        return Ok(channel);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateChannel([FromBody] ChannelCreateDto createDto)
    {
        var channel = await _channelService.CreateChannelAsync(
            createDto.Name,
            createDto.Description
        );
        
        return Ok(channel);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteChannel([FromRoute] Guid id)
    {
        var channel = await _channelService.DeleteChannelAsync(id);

        return Ok(channel);
    }
}
using BreadChat.Application.Dtos;
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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ChannelDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetChannel([FromRoute] Guid id)
    {
        var channel = await _channelService.GetChannelAsync(id);

        return Ok(channel);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(PageDto<ChannelDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsers([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var page = await _channelService.GetChannelsAsync(pageNumber, pageSize);

        return Ok(page);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ChannelDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateChannel([FromBody] ChannelCreateDto createDto)
    {
        var channel = await _channelService.CreateChannelAsync(
            createDto.Name,
            createDto.Description
        );

        return Ok(channel);
    }

    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ChannelDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateChannel([FromRoute] Guid id, [FromBody] ChannelUpdateDto updateDto)
    {
        var channelDto = await _channelService.UpdateChannelAsync(id, updateDto);

        return Ok(channelDto);
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ChannelDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteChannel([FromRoute] Guid id)
    {
        var channel = await _channelService.DeleteChannelAsync(id);

        return Ok(channel);
    }
}
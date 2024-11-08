using BreadChat.Application.Dtos.ChannelMembershipDtos;
using BreadChat.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BreadChat.Controllers;

[ApiController]
[Route("/api/channels/{channelId}/members")]
public class ChannelMembershipController : Controller
{
    private IMembershipService _membershipService;
    
    public ChannelMembershipController(IMembershipService membershipService)
    {
        _membershipService = membershipService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ChannelMembershipDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetMembers([FromRoute] Guid channelId)
    {
        var members = await _membershipService.GetMembersAsync(channelId);

        return Ok(members);
    }
    
    [HttpPost("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> AddMember([FromRoute] Guid channelId, [FromRoute] Guid userId)
    {
        await _membershipService.AddMemberAsync(channelId, userId);

        return Ok();
    }
    
    [HttpDelete("{userId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> RemoveMember([FromRoute] Guid channelId, [FromRoute] Guid userId)
    {
        await _membershipService.RemoveMemberAsync(channelId, userId);

        return Ok();
    }
}
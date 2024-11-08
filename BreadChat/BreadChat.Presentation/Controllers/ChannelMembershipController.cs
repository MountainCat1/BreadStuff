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
    
    [HttpPost]
    public async Task<IActionResult> AddMember([FromRoute] Guid channelId, [FromBody] Guid userId)
    {
        await _membershipService.AddMemberAsync(channelId, userId);

        return Ok();
    }
}
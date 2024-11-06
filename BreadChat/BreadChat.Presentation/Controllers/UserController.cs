using BreadChat.Application.Dtos;
using BreadChat.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace BreadChat.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : Controller
{
    private IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserCreateDto createDto)
    {
        var user = await _userService.CreateUserAsync(
            createDto.Username,
            createDto.FirstName, 
            createDto.LastName
        );
        
        return Ok(user);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser([FromRoute] Guid id)
    {
        var user = await _userService.GetUserAsync(id);

        return Ok(user);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
    {
        var user = await _userService.DeleteUserAsync(id);

        return Ok(user);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var page = await _userService.GetUsersAsync(pageNumber, pageSize);

        return Ok(page);
    }
}
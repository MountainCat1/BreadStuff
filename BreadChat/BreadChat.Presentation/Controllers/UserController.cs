using BreadChat.Application.Dtos;
using BreadChat.Application.Dtos.UserDtos;
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
    
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUser([FromRoute] Guid id)
    {
        var user = await _userService.GetUserAsync(id);

        return Ok(user);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(PageDto<UserDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUsers([FromQuery] int pageNumber, [FromQuery] int pageSize)
    {
        var page = await _userService.GetUsersAsync(pageNumber, pageSize);

        return Ok(page);
    }

    [HttpPost]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateUser([FromBody] UserCreateDto createDto)
    {
        var user = await _userService.CreateUserAsync(
            createDto.Username,
            createDto.FirstName, 
            createDto.LastName
        );
        
        return Ok(user);
    }
    
    [HttpPatch("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateChannel([FromRoute] Guid id, [FromBody] UserUpdateDto updateDto)
    {
        var channelDto = await _userService.UpdateUserAsync(id, updateDto);

        return Ok(channelDto);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteUser([FromRoute] Guid id)
    {
        var user = await _userService.DeleteUserAsync(id);

        return Ok(user);
    }
}
﻿using BreadChat.Application.Dtos;
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
    
    [HttpGet("")]
    public IActionResult GetUsers()
    {
        return Ok("XD");
    }
}
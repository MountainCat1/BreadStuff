using BreadChat.Application.ApplicationErrors;
using BreadChat.Application.Dtos;
using BreadChat.Domain.Entities;
using BreadChat.Persistence;
using Microsoft.EntityFrameworkCore;

namespace BreadChat.Application.Services;

public interface IUserService
{
    public Task<UserDto> CreateUserAsync(string username, string firstName, string lastName);
    public Task<UserDto> GetUserAsync(Guid id);
    public Task<UserDto> DeleteUserAsync(Guid id);
    Task<PageDto<UserDto>> GetUsersAsync(int pageNumber, int pageSize);
}

public class UserService : IUserService
{
    private IBreadChatDbContext _dbContext;

    public UserService(IBreadChatDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<UserDto> CreateUserAsync(string username, string firstName, string lastName)
    {
        var user = User.Create(username, firstName, lastName); // create domain entity

        _dbContext.Users.Add(user); // add to db context

        await _dbContext.SaveChangesAsync(); // save changes

        return UserDto.FromDomain(user); // convert to dto
    }

    public async Task<UserDto> GetUserAsync(Guid id)
    {
        var userDbEntity = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (userDbEntity is null)
        {
            throw new NotFoundError($"User with id {id} not found");
        }

        return UserDto.FromDomain(userDbEntity);
    }

    public async Task<PageDto<UserDto>> GetUsersAsync(int pageNumber, int pageSize)
    {
        var users = await _dbContext.Users.Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();
        var userCount = await _dbContext.Users.CountAsync();

        var userDtos = users.Select(x => UserDto.FromDomain(x)).ToList();

        return new PageDto<UserDto>(userDtos, pageNumber, pageSize, userCount);
    }

    public async Task<UserDto> DeleteUserAsync(Guid id)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);

        if (user is null)
        {
            throw new NotFoundError($"User with id {id} not found");
        }

        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();

        return UserDto.FromDomain(user);
    }
}
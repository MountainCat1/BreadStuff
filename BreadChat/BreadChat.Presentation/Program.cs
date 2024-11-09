using System.Security.Claims;
using System.Text.Json;
using BreadChat.Application.Configuration;
using BreadChat.Application.Services;
using BreadChat.Controllers;
using BreadChat.Middlewares;
using BreadChat.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;

services.Configure<JwtConfig>(builder.Configuration.GetSection("JwtConfig"));

typeof(UserController).Assembly.GetTypes()
    .Where(x => x.GetCustomAttributes(true).Any(x => x is ApiControllerAttribute));

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

services.AddDbContext<IBreadChatDbContext, BreadChatDbContext>(options =>
{
    options.UseSqlite("Data Source=breadchat.db",
        x => { x.MigrationsAssembly($"{typeof(BreadChatDbContext).Assembly.GetName().Name}"); });
});

services.AddScoped<IJwtService, JwtService>();
services.AddScoped<IUserService, UserService>();
services.AddScoped<IChannelService, ChannelService>();
services.AddScoped<IMessageService, MessageService>();
services.AddScoped<IMembershipService, MembershipService>();

services.AddSingleton<ErrorHandlingMiddleware>();

var serviceProvider = services.BuildServiceProvider();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlingMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<IBreadChatDbContext>();

    await dbContext.Database.MigrateAsync();
}

app.Run();
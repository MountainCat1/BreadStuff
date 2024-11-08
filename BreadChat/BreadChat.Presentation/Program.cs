using BreadChat.Application.Services;
using BreadChat.Middlewares;
using BreadChat.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddMvc();

services.AddDbContext<IBreadChatDbContext, BreadChatDbContext>(options =>
{
    options.UseSqlite("Data Source=breadchat.db", x =>
    {
        x.MigrationsAssembly($"{typeof(BreadChatDbContext).Assembly.GetName().Name}");
    });
});

services.AddScoped<IUserService, UserService>();
services.AddScoped<IChannelService, ChannelService>();
services.AddScoped<IMessageService, MessageService>();
services.AddScoped<IMembershipService, MembershipService>();

services.AddSingleton<ErrorHandlingMiddleware>();


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
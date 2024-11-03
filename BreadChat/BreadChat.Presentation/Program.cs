using BreadChat.Application.Services;
using BreadChat.Middlewares;
using BreadChat.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvc();

builder.Services.AddDbContext<IBreadChatDbContext, BreadChatDbContext>(options =>
{
    options.UseSqlite("Data Source=breadchat.db", x =>
    {
        x.MigrationsAssembly($"{typeof(BreadChatDbContext).Assembly.GetName().Name}");
    });
});
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddSingleton<ErrorHandlingMiddleware>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

app.UseMiddleware<ErrorHandlingMiddleware>();

app.Run();
using BreadChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BreadChat.Persistence;

public interface IBreadChatDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Channel> Channels { get; set; }
    DatabaseFacade Database { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public class BreadChatDbContext : DbContext, IBreadChatDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Channel> Channels { get; set; }
    
    public BreadChatDbContext(DbContextOptions<BreadChatDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        var user = mb.Entity<User>();

        user.ToTable("Users");
        user.HasKey(x => x.Id);
        user.Property(x => x.Username).IsRequired().HasMaxLength(32);
        user.Property(x => x.FirstName).IsRequired().HasMaxLength(32);
        user.Property(x => x.LastName).IsRequired().HasMaxLength(32);

        var channel = mb.Entity<Channel>();
        channel.ToTable("Channels");
        channel.HasKey(x => x.Id);
        channel.Property(x => x.Name).IsRequired().HasMaxLength(32);
        channel.HasMany<User>(x => x.Users).WithMany().UsingEntity<UserChannel>();

        var userChannel = mb.Entity<UserChannel>();
        userChannel.ToTable("UserChannels");
        userChannel.HasKey(x => new { x.UserId, x.ChannelId });
        
    }
}
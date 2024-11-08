using BreadChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BreadChat.Persistence;

public interface IBreadChatDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Channel> Channels { get; set; }
    DbSet<ChannelMembership> ChannelMemberships { get; set; }
    DbSet<Message> Messages { get; set; }
    DatabaseFacade Database { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public class BreadChatDbContext : DbContext, IBreadChatDbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Channel> Channels { get; set; }
    public DbSet<ChannelMembership> ChannelMemberships { get; set; }
    public DbSet<Message> Messages { get; set; }
    
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
        
        var channelMembership = mb.Entity<ChannelMembership>();
        channelMembership.ToTable("ChannelMemberships");
        channelMembership.HasKey(x => new { x.UserId, x.ChannelId });
        channelMembership.HasOne(x => x.User).WithMany().HasForeignKey(x => x.UserId);
        channelMembership.HasOne(x => x.Channel).WithMany(x => x.Members).HasForeignKey(x => x.ChannelId);
        
        var messages = mb.Entity<Message>();
        messages.ToTable("Messages");
        messages.HasKey(x => x.Id);
        messages.Property(x => x.Content).IsRequired().HasMaxLength(1024);
        messages.HasOne(x => x.Channel).WithMany(x => x.Messages).HasForeignKey(x => x.ChannelId).IsRequired();
        messages.HasOne(x => x.Author).WithMany().HasForeignKey(x => x.AuthorId).IsRequired();
    }
}
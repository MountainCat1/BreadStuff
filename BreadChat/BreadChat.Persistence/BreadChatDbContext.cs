using BreadChat.Domain.Entities;
using BreadChat.Persistence.DbEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BreadChat.Persistence;

public interface IBreadChatDbContext
{
    DbSet<UserDbEntity> Users { get; set; }
    DbSet<ChannelDbEntity> Channels { get; set; }
    DatabaseFacade Database { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public class BreadChatDbContext : DbContext, IBreadChatDbContext
{
    public DbSet<UserDbEntity> Users { get; set; }
    public DbSet<ChannelDbEntity> Channels { get; set; }
    
    public BreadChatDbContext(DbContextOptions<BreadChatDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        var user = mb.Entity<UserDbEntity>();

        user.ToTable("Users");
        user.HasKey(x => x.Id);
        user.Property(x => x.Username).IsRequired().HasMaxLength(32);
        user.Property(x => x.FirstName).IsRequired().HasMaxLength(32);
        user.Property(x => x.LastName).IsRequired().HasMaxLength(32);

        var channel = mb.Entity<ChannelDbEntity>();
        channel.ToTable("Channels");
        channel.HasKey(x => x.Id);
        channel.Property(x => x.Name).IsRequired().HasMaxLength(32);
        channel.HasMany<UserDbEntity>(x => x.Users).WithMany().UsingEntity<UserChannelDbEntity>();

        channel.ToTable("UserChannels");
        var userChannel = mb.Entity<UserChannelDbEntity>();
        userChannel.HasKey(x => new { x.UserId, x.ChannelId });
        
    }
}
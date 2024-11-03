using BreadChat.Persistence.DbEntities;
using Microsoft.EntityFrameworkCore;

namespace BreadChat.Persistence;

public interface IBreadChatDbContext
{
    DbSet<UserDbEntity> Users { get; set; }
    
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}

public class BreadChatDbContext : DbContext, IBreadChatDbContext
{
    public DbSet<UserDbEntity> Users { get; set; }
    
    public BreadChatDbContext(DbContextOptions<BreadChatDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder mb)
    {
        var user = mb.Entity<UserDbEntity>();

        user.HasKey(x => x.Id);
        user.Property(x => x.Username).IsRequired().HasMaxLength(32);
        user.Property(x => x.FirstName).IsRequired().HasMaxLength(32);
        user.Property(x => x.LastName).IsRequired().HasMaxLength(32);
    }
}
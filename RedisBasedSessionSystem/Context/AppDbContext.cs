using RedisBasedSessionSystem.Entities;
using Microsoft.EntityFrameworkCore;

namespace RedisBasedSessionSystem.Context;
public class AppDbContext : DbContext
{
    public DbSet<MenuRecord> MenuRecords { get; set; }
    public DbSet<SessionRecord> SessionRecords { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // MenuRecord tablosunun yapılandırması
        modelBuilder.Entity<MenuRecord>(entity =>
        {
            entity.ToTable("MenuRecords").HasKey(m => m.Id);
            entity.HasKey(m => m.Id);

            entity.Property(m => m.Role)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(m => m.MenuJson)
                .IsRequired();

            entity.Property(m => m.CreatedAt)
                .IsRequired();
        });

        // SessionRecord tablosunun yapılandırması
        modelBuilder.Entity<SessionRecord>(entity =>
        {
            entity.ToTable("SessionRecords");
            entity.HasKey(s => s.Id);

            entity.Property(s => s.SessionKey)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(s => s.SessionData)
                .IsRequired();

            entity.Property(s => s.CreatedAt)
                .IsRequired();
        });
    }
}

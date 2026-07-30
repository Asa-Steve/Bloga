using Bloga.Models;
using Microsoft.EntityFrameworkCore;

namespace Bloga.Data;

public class BlogaDbContext : DbContext
{
    public DbSet<Post> Posts { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public BlogaDbContext(DbContextOptions options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // tags
        modelBuilder.Entity<Tag>()
        .HasIndex(t => t.Name)
        .IsUnique();

        // category
        modelBuilder.Entity<Category>()
        .HasIndex(c => c.Name)
        .IsUnique();

        // deleting posts
        modelBuilder.Entity<Comment>()
        .HasOne(c => c.Post)
        .WithMany(p => p.Comments)
        .OnDelete(DeleteBehavior.Cascade);
    }
}
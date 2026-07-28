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
}
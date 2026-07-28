using Bloga.Data;
using Bloga.Models;

namespace Bloga.Common;

public class SeedData
{
    private List<Category> categories;

    private List<Tag> tags;


    private List<Post> posts;

    private List<Comment> comments;

    public SeedData()
    {
        tags =
[
    new () { Name = "C#" },
    new () { Name = ".NET" },
    new () { Name = "Entity Framework Core" },
    new () { Name = "ASP.NET Core" },
    new () { Name = "SQL" },
    new () { Name = "SQLite" },
    new () { Name = "Docker" },
    new () { Name = "REST API" },
    new () { Name = "Clean Code" },
    new () { Name = "Testing" }
];
        categories = [new() { Name = "Technology" },
    new() { Name = "Programming" },
    new() { Name = "Web Development" },
    new() { Name = "Mobile Development" },
    new() { Name = "Artificial Intelligence" },
    new() { Name = "Cybersecurity" },
    new() { Name = "Cloud Computing" },
    new() { Name = "DevOps" },
    new() { Name = "Career" },
    new() { Name = "Productivity" }];
        posts =
[
    new()
    {
        Title = "Getting Started with C#",
        Content = "An introduction to the C# programming language.",
        PublishDate = new DateTime(2026, 1, 5),
        CategoryId = 2,
        Tags = [tags[0], tags[1]]
    },
    new()
    {
        Title = "Understanding Entity Framework Core",
        Content = "Learn how EF Core simplifies database access.",
        PublishDate = new DateTime(2026, 1, 12),
        CategoryId = 2,
        Tags = [tags[1], tags[2], tags[4]]
    },
    new()
    {
        Title = "Building Your First Web API",
        Content = "Create a simple REST API using ASP.NET Core.",
        PublishDate = new DateTime(2026, 1, 20),
        CategoryId = 3,
        Tags = [tags[3], tags[7]]
    },
    new()
    {
        Title = "Introduction to Docker",
        Content = "Containerize your .NET applications with Docker.",
        PublishDate = new DateTime(2026, 2, 2),
        CategoryId = 7,
        Tags = [tags[6]]
    },
    new()
    {
        Title = "SQLite for Beginners",
        Content = "Store data locally using SQLite.",
        PublishDate = new DateTime(2026, 2, 10),
        CategoryId = 2,
        Tags = [tags[5], tags[4]]
    },
    new()
    {
        Title = "Deploying Applications to the Cloud",
        Content = "A beginner's guide to cloud deployment.",
        PublishDate = new DateTime(2026, 2, 18),
        CategoryId = 7,
        Tags = [tags[6]]
    },
    new()
    {
        Title = "Understanding Authentication",
        Content = "Learn the basics of authentication and authorization.",
        PublishDate = new DateTime(2026, 3, 1),
        CategoryId = 6,
        Tags = [tags[3]]
    },
    new()
    {
        Title = "Clean Code Principles",
        Content = "Write readable and maintainable code.",
        PublishDate = new DateTime(2026, 3, 10),
        CategoryId = 9,
        Tags = [tags[8]]
    },
    new()
    {
        Title = "Introduction to Unit Testing",
        Content = "Test your applications with xUnit.",
        PublishDate = new DateTime(2026, 3, 18),
        CategoryId = 9,
        Tags = [tags[9]]
    },
    new()
    {
        Title = "Boosting Developer Productivity",
        Content = "Tips and tools to work more efficiently.",
        PublishDate = new DateTime(2026, 3, 25),
        CategoryId = 10,
        Tags = [tags[8], tags[9]]
    }
];
        comments = [
            new()
            {
                Content = "This article was very helpful!",
                CreatedAt = new DateTime(2026, 1, 6),
                PostId = 1
            },
    new()
    {
        Content = "Thanks for the clear explanation.",
        CreatedAt = new DateTime(2026, 1, 13),
        PostId = 2
    },
    new()
    {
        Content = "Looking forward to more tutorials.",
        CreatedAt = new DateTime(2026, 1, 21),
        PostId = 3
    },
    new()
    {
        Content = "Docker finally makes sense to me.",
        CreatedAt = new DateTime(2026, 2, 3),
        PostId = 4
    },
    new()
    {
        Content = "SQLite is easier than I expected.",
        CreatedAt = new DateTime(2026, 2, 11),
        PostId = 5
    },
    new()
    {
        Content = "Great deployment tips!",
        CreatedAt = new DateTime(2026, 2, 19),
        PostId = 6
    },
    new()
    {
        Content = "Can you cover JWT in another post?",
        CreatedAt = new DateTime(2026, 3, 2),
        PostId = 7
    },
    new()
    {
        Content = "I learned a lot from this article.",
        CreatedAt = new DateTime(2026, 3, 11),
        PostId = 8
    },
    new()
    {
        Content = "Excellent explanation of unit testing.",
        CreatedAt = new DateTime(2026, 3, 19),
        PostId = 9
    },
    new()
    {
        Content = "Very practical productivity advice.",
        CreatedAt = new DateTime(2026, 3, 26),
        PostId = 10
    }

];
    }


    public void SeedDB(BlogaDbContext ctx)
    {
        // Create the database if it doesn't exist.
        ctx.Database.EnsureCreated();

        // Don't seed again if data already exists.
        if (ctx.Categories.Any())
            return;

        // Seed in dependency order.
        ctx.Categories.AddRange(categories);
        ctx.SaveChanges();

        ctx.Tags.AddRange(tags);
        ctx.SaveChanges();

        ctx.Posts.AddRange(posts);
        ctx.SaveChanges();

        ctx.Comments.AddRange(comments);
        ctx.SaveChanges();
    }

}

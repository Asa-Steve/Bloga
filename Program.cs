using System.Text.Json.Serialization;
using Bloga.Common;
using Bloga.Data;
using Bloga.DTOs;
using Bloga.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<BlogaDbContext>(options => options.UseSqlite("Data Source=bloga.db"));
builder.Services.ConfigureHttpJsonOptions(options =>
{
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    using var scope = app.Services.CreateScope();

    var context = scope.ServiceProvider.GetRequiredService<BlogaDbContext>();
    context.Database.EnsureCreated();
    var seeder = new SeedData();
    seeder.SeedDB(context);
}

app.UseHttpsRedirection();


app.MapGet("/posts", (BlogaDbContext ctx) =>
{
    var posts = ctx.Posts
    .Include(p => p.Tags)
    .Select(p => new PostDto(p)).ToList();
    return Results.Ok(posts);

})
.WithName("GetAllPosts");

app.MapGet("/post/{id}", async (int id, BlogaDbContext ctx) =>
{
    Console.WriteLine($"Post ID : {id}");
    var foundPost = await ctx.Posts.FindAsync(id);
    if (foundPost is null) return Results.NotFound();
    return Results.Ok(foundPost);
});

app.Run();

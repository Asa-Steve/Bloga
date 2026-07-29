using System.Text.Json.Serialization;
using Bloga.Common;
using Bloga.Data;
using Bloga.DTOs;
using Bloga.Models;
using Bloga.Endpoints;
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

var Post = app.MapGroup("api/posts");
Post.MapGet("/", PostEndpoints.HandleGetAllPostsAsync).WithName("GetAllPosts");
Post.MapGet("/{id}", PostEndpoints.HandleGetPostAsync).WithName("GetPost");
Post.MapPost("/", PostEndpoints.HandleCreatePostAsync).WithName("CreatePostAsync");
Post.MapPatch("/{id}", PostEndpoints.HandleUpdatePostAsync).WithName("UpdatePostAsync");
Post.MapDelete("/{id}", PostEndpoints.HandleDeletePostAsync).WithName("DeletePostAsync");
app.Run();
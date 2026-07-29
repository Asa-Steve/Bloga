using System.Text.Json;
using Bloga.Data;
using Bloga.DTOs;
using Bloga.Models;
using Microsoft.EntityFrameworkCore;

namespace Bloga.Endpoints;

public class PostEndpoints(BlogaDbContext ctx)
{
    private readonly BlogaDbContext _ctx = ctx;

    // get all posts
    public static async Task<IResult> HandleGetAllPostsAsync(BlogaDbContext ctx)
    {
        var posts = ctx.Posts
        .Select(p => new PostDto(p)).ToList();
        return Results.Ok(posts);
    }

    // get post
    public static async Task<IResult> HandleGetPostAsync(int id, BlogaDbContext ctx)
    {
        var foundPost = await ctx.Posts
        .Include(p => p.Category)
        .Include(p => p.Comments)
        .Include(p => p.Tags)
        .FirstOrDefaultAsync(p => p.Id == id);

        if (foundPost is null) return Results.NotFound();
        return Results.Ok(new PostDto(foundPost));
    }

    // create post
    public static async Task<IResult> HandleCreatePostAsync(PostCreateDto post, BlogaDbContext ctx)
    {
        try
        {
            Post p = new()
            {
                Title = post.Title,
                Content = post.Content,
                CategoryId = post.CategoryId,
                Tags = post.Tags is null ? null : [.. post.Tags.Select(tag => new Tag { Name = tag })]
            };

            await ctx.Posts.AddAsync(p);
            int res = await ctx.SaveChangesAsync();
            if (res < 1) throw new Exception();
            return Results.Created();
        }
        catch (Exception)
        {
            return Results.InternalServerError(new { Message = "Something went wrong" });
        }

    }

    // update post
    public static async Task<IResult> HandleUpdatePostAsync(int id, PostUpdateDto postUpdate, BlogaDbContext ctx)
    {
        try
        {
            // find the post
            var foundPost = await ctx.Posts.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == id) ?? throw new ArgumentException($"No post found for id : {id}");

            // found a post
            if (!string.IsNullOrWhiteSpace(postUpdate.Title)) foundPost.Title = postUpdate.Title;
            if (!string.IsNullOrWhiteSpace(postUpdate.Content)) foundPost.Content = postUpdate.Content;
            if (postUpdate.Tags is not null) foundPost.Tags = [.. postUpdate.Tags.Select(tag => new Tag { Name = tag })];

            if (postUpdate.CategoryId.ValueKind != JsonValueKind.Undefined)
            {
                if (postUpdate.CategoryId.ValueKind == JsonValueKind.Null) foundPost.CategoryId = null;
                else foundPost.CategoryId = postUpdate.CategoryId.GetInt32();
            }


            await ctx.SaveChangesAsync();
            return Results.Ok();
        }
        catch (ArgumentException ex)
        {
            return Results.InternalServerError(new { ex.Message });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
            return Results.InternalServerError(new { Message = "Something went wrong" });
        }

    }

    // delete post
    public static async Task<IResult> HandleDeletePostAsync(int id, BlogaDbContext ctx)
    {
        try
        {
            // find the post
            var foundPost = ctx.Posts.Find(id) ?? throw new ArgumentException($"No post found for id : {id}");
            // removing
            ctx.Posts.Remove(foundPost);
            await ctx.SaveChangesAsync();
            return Results.NoContent();
        }
        catch (ArgumentException ex)
        {
            return Results.InternalServerError(new { Message = ex.Message });
        }
        catch (Exception)
        {
            return Results.InternalServerError(new { Message = "Something went wrong" });
        }

    }

}
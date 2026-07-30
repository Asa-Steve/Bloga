using Bloga.Data;
using Bloga.DTOs;
using Bloga.Interface;
using Bloga.Models;
using Microsoft.EntityFrameworkCore;

namespace Bloga.Endpoints;

public class PostEndpoints
{
    // get all posts
    public static async Task<IResult> HandleGetAllPostsAsync(BlogaDbContext ctx, IPostService postService)
    {
        return Results.Ok(await postService.GetAllPostsAsync());
    }

    // get post
    public static async Task<IResult> HandleGetPostAsync(int id, BlogaDbContext ctx, IPostService postService)
    {
        var post = await postService.GetPostByIdAsync(id);
        return post is null ? Results.NotFound() : Results.Ok(post);
    }

    // create post
    public static async Task<IResult> HandleCreatePostAsync(PostCreateDto request, BlogaDbContext ctx, IPostService postService)
    {
        var post = await postService.CreatePostAsync(request);
        return post is null ? Results.InternalServerError() : Results.CreatedAtRoute("GetPost", new { id = post.Id },
        post);

    }

    // update post
    public static async Task<IResult> HandleUpdatePostAsync(int id, PostUpdateDto postUpdate, BlogaDbContext ctx, IPostService postService)
    {
        bool isUpdated = await postService.UpdatePostAsync(id, postUpdate);
        return isUpdated ? Results.NoContent() : Results.InternalServerError();
    }

    // delete post
    public static async Task<IResult> HandleDeletePostAsync(int id, BlogaDbContext ctx, IPostService postService)
    {
        return await postService.DeletePostAsync(id) ? Results.NoContent() : Results.InternalServerError();
    }
}
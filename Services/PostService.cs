using Bloga.Data;
using Bloga.DTOs;
using Bloga.Interface;
using Bloga.Models;
using Microsoft.EntityFrameworkCore;

namespace Bloga.Services;

class PostService(BlogaDbContext ctx) : IPostService
{
    private readonly BlogaDbContext _ctx = ctx;

    public async Task<List<PostDto>> GetAllPostsAsync()
    {
        var posts = await _ctx.Posts
        .Select(p => new PostDto(p)).ToListAsync();
        return posts;
    }

    public async Task<PostDto?> GetPostByIdAsync(int postId)
    {
        var foundPost = await _ctx.Posts
                .Include(p => p.Category)
                .Include(p => p.Comments)
                .Include(p => p.Tags)
                .FirstOrDefaultAsync(p => p.Id == postId);

        return foundPost is null ? null : new PostDto(foundPost);
    }

    public async Task<PostDto?> CreatePostAsync(PostCreateDto request)
    {
        try
        {
            var category = await _ctx.Categories.FindAsync(request.CategoryId);
            if (category is null) return null;

            List<Tag>? validTags = null;

            if (request.Tags is not null)
                validTags = await _ctx.Tags.Where(t => request.Tags.Contains(t.Name)).ToListAsync();

            Post post = new()
            {
                Title = request.Title,
                Content = request.Content,
                CategoryId = request.CategoryId,
                Tags = validTags
            };

            await _ctx.Posts.AddAsync(post);
            int res = await _ctx.SaveChangesAsync();
            if (res < 1) throw new Exception();
            return new PostDto(post);
        }
        catch (Exception)
        {
            return null;
        }

    }

    public async Task<bool> UpdatePostAsync(int postId, PostUpdateDto postUpdate)
    {
        try
        {
            // find the post
            var foundPost = await _ctx.Posts.Include(p => p.Tags).FirstOrDefaultAsync(p => p.Id == postId) ?? throw new Exception();
            var existingTags = await _ctx.Tags.Where(t => postUpdate.Tags.Contains(t.Name)).ToListAsync();

            // found a post
            if (!string.IsNullOrWhiteSpace(postUpdate.Title)) foundPost.Title = postUpdate.Title;
            if (!string.IsNullOrWhiteSpace(postUpdate.Content)) foundPost.Content = postUpdate.Content;
            if (postUpdate.Tags is not null) foundPost.Tags = [.. existingTags];
            if (postUpdate.CategoryId is not null) foundPost.CategoryId = (int)postUpdate.CategoryId;


            await _ctx.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"ERROR: {ex.Message}");
            return false;
        }

    }

    public async Task<bool> DeletePostAsync(int postId)
    {
        try
        {
            // find the post
            var foundPost = _ctx.Posts.Find(postId) ?? throw new ArgumentException($"No post found for id : {postId}");
            // removing
            _ctx.Posts.Remove(foundPost);
            await _ctx.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }
}
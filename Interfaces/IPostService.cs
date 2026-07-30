using Bloga.DTOs;

namespace Bloga.Interface;

public interface IPostService
{
    public Task<List<PostDto>> GetAllPostsAsync();
    public Task<PostDto?> GetPostByIdAsync(int postId);
    public Task<PostDto?> CreatePostAsync(PostCreateDto request);
    public Task<bool> UpdatePostAsync(int postId, PostUpdateDto postUpdate);
    public Task<bool> DeletePostAsync(int postId);
}
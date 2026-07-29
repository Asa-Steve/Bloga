using Bloga.Models;

namespace Bloga.DTOs;

public class PostDto(Post post)
{
    public int Id { get; set; } = post.Id;
    public string Title { get; set; } = post.Title;
    public string Content { get; set; } = post.Content;
    public DateTime PublishDate { get; set; } = post.PublishDate;

    // relationships
    public CategoryDto? Category { get; set; } = post.Category is null ? null : new CategoryDto(post.Category);
    public IEnumerable<string>? Tags { get; set; } = post.Tags is null ? null : [.. post.Tags.Select(t => t.Name)];
    public List<CommentDto>? Comments { get; set; } = post.Comments is null ? null : [.. post.Comments.Select(c => new CommentDto(c))];
}
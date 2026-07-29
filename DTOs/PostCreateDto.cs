using Bloga.Models;

namespace Bloga.DTOs;

public class PostCreateDto(string title, string content, int categoryId, string[]? tags = null)
{
    public string Title { get; set; } = title;
    public string Content { get; set; } = content;

    // relationships
    public int CategoryId { get; set; } = categoryId;
    public string[]? Tags { get; set; } = tags is null ? null : tags;
}
using Bloga.Models;

namespace Bloga.DTOs;

public class PostCreateDto
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;

    // relationships
    public int CategoryId { get; set; }
    public string[]? Tags { get; set; }
}
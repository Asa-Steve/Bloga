namespace Bloga.DTOs;

public class PostUpdateDto
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    // relationships
    public int? CategoryId { get; set; }
    public string[]? Tags { get; set; }
}
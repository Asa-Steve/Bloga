using System.Text.Json;

namespace Bloga.DTOs;


public class PostUpdateDto
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    // relationships
    public JsonElement CategoryId { get; set; }
    public string[]? Tags { get; set; }
}
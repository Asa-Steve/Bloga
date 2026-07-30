namespace Bloga.Models;

using System.ComponentModel.DataAnnotations;

public class Comment
{
    public int Id { get; set; }

    [Required]
    [StringLength(1000, MinimumLength = 2)]
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    // relationships
    [Required]
    public int PostId { get; set; }
    // Navigation properties
    public Post Post { get; set; } = null!;
}

/*
Id	int
Content	string
CreatedAt	DateTime
PostId	int
*/
//One Comment belongs to one Post.
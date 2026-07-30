namespace Bloga.Models;

using System.ComponentModel.DataAnnotations;
public class Post
{
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 30)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(5000, MinimumLength = 500)]
    public string Content { get; set; } = string.Empty;

    public DateTime PublishDate { get; set; } = DateTime.Now;

    // relationships
    [Required]
    public int CategoryId { get; set; }

    // Navigation properties
    public Category Category { get; set; } = null!;
    public List<Tag>? Tags { get; set; }
    public List<Comment> Comments { get; set; } = [];
}

/*
Id	int
Title	string
Content	string
PublishDate	DateTime
CategoryId	int
*/

/*
One Category
Many Comments
Many Tags
*/
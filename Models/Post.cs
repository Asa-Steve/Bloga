namespace Bloga.Models;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime PublishDate { get; set; } = DateTime.Now;

    // relationships
    // - Category
    public int? CategoryId { get; set; }

    // Navigation properties
    public Category? Category { get; set; }
    public List<Tag>? Tags { get; set; }
    public List<Comment>? Comments { get; set; }
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
namespace Bloga.Models;

public class Comment
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }

    // relationships
    public int PostId { get; set; }
    // Navigation properties
    public Post? Post { get; set; }
}

/*
Id	int
Content	string
CreatedAt	DateTime
PostId	int
*/
//One Comment belongs to one Post.
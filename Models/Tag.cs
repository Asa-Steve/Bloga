namespace Bloga.Models;

public class Tag
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    // Navigation properties
    public List<Post> Posts { get; set; } = [];
}

/*
Property	Type
Id	int
Name	string
*/
// Many Tags belong to many Posts.
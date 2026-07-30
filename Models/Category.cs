using System.ComponentModel.DataAnnotations;

namespace Bloga.Models;

public class Category
{
    public int Id { get; set; }

    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string Name { get; set; } = string.Empty;

    // Navigation properties
    public List<Post> Posts { get; set; } = null!;
}

/*
Property	Type
Id	int
Name	string
*/
// One Category has many Posts.
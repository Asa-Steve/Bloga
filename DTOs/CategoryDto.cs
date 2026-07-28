using Bloga.Models;

namespace Bloga.DTOs;

public class CategoryDto(Category cat)
{
    public int Id { get; set; } = cat.Id;
    public string Name { get; set; } = cat.Name;
}
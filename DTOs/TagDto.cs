using Bloga.Models;

namespace Bloga.DTOs;

public class TagDto(Tag tag)
{
    public int Id { get; set; } = tag.Id;
    public string Name { get; set; } = tag.Name;
}
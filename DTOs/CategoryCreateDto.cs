using System.ComponentModel.DataAnnotations;

namespace Bloga.DTOs;

class CategoryCreateDto
{
    [Required]
    public string Name { get; set; }
}
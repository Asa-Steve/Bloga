using Bloga.Models;

namespace Bloga.DTOs;

public class CommentDto(Comment comment)
{
    public int Id { get; set; } = comment.Id;
    public string Content { get; set; } = comment.Content;
}
using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Others
{
    public class CommentDto
    {
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        public string? Text { get; set; }
        public int? ParentCommentId { get; set; }
    }
}

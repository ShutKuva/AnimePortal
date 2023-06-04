namespace Core.DB
{
    public class Comment : BaseEntity
    {
        public string? Text { get; set; } = string.Empty;
        public int? ParentCommentId { get; set; }
        public Comment? ParentComment { get; set; }
    }
}

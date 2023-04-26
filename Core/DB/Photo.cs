namespace Core.DB
{
    public class Photo : BaseEntity
    {
        public string ImageUrl { get; set; } = string.Empty;
        public string PublicId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        
    }
}

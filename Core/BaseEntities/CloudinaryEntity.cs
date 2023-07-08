using Core.DB;

namespace Core.BaseEntities
{
    public class CloudinaryEntity : BaseEntity
    {
        public string Url { get; set; } = string.Empty;
        public string PublicId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
    }
}
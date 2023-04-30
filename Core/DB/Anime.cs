using Core.Enums;

namespace Core.DB
{
    public class Anime : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Placement { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string? VideoUrl { get; set; } = string.Empty;
        public float? Rating { get; set; } = 0.0f;
        public ICollection<Photo>? Photos { get; set; } = new List<Photo>();

        private DateTime _date;
        public DateTime Date
        {
            get => _date;
            set => _date = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }

        public VideoTags? Tags { get; set; }
        public int PostedBy { get; set; }
    }
}

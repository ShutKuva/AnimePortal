namespace Core.DB
{
    public class Anime : BaseEntity
    {
        public int PostedBy { get; set; }
        public DateTime Date { get; set; }
        public string Duration { get; set; } = string.Empty;
        public float? Rating { get; set; } = 0.0f;
        public string Studio { get; set; } = string.Empty;
        public string? VideoUrl { get; set; } = string.Empty;
        public ICollection<Tag>? Tags { get; set; } = new HashSet<Tag>();
        public ICollection<Photo>? Photos { get; set; } = new List<Photo>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public ICollection<AnimeDescription?> AnimeDescriptions { get; set; } = new List<AnimeDescription?>();
    }
}

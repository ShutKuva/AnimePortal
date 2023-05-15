namespace Core.DB
{
    public class Anime : BaseEntity
    {
        public string Duration { get; set; } = string.Empty;
        public string? VideoUrl { get; set; } = string.Empty;
        public float? Rating { get; set; } = 0.0f;
        public ICollection<string>? Tags { get; set; } = new HashSet<string>();
        public int PostedBy { get; set; }
        public DateTime Date { get; set; }
        public ICollection<Photo>? Photos { get; set; } = new List<Photo>();
        public ICollection<AnimeDescription?> AnimeDescriptions { get; set; } = new List<AnimeDescription?>();
    }
}

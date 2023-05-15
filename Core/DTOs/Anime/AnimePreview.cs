namespace Core.DTOs.Anime
{
    public class AnimePreview
    {
        public int Id { get; set; }
        public AnimeDescriptionDto? AnimeDescription { get; set; }
        public string Duration { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public ICollection<string>? Tags { get; set; } = new HashSet<string>();
    }
}

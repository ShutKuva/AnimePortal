using Core.DTOs.Others;

namespace Core.DTOs.Anime
{
    public class AnimePreview
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public float Rating { get; set; } = 0.0f;
        public string Studio { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string Spotlight { get; set; } = string.Empty;
        public AnimeDescriptionDto? AnimeDescription { get; set; }
        public ICollection<TagDto>? Tags { get; set; } = new HashSet<TagDto>();
    }
}

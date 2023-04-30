using Core.Enums;

namespace Core.DTOs.Anime
{
    public class AnimeDto
    {
        public string Title { get; set; } = string.Empty;
        public string Placement { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public VideoTags? Tags { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using Core.DTOs.Others;

namespace Core.DTOs.Anime
{
    public class AnimeDescriptionDto
    {
        [Required]
        public LanguageDto? Language { get; set; }
        public float Rating { get; set; } = 0.0f;
        public string Studio { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ICollection<GenreDto> Genres { get; set; } = new List<GenreDto>();

    }
}

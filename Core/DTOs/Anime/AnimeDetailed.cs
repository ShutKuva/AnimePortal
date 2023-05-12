using System.ComponentModel.DataAnnotations;
using Core.DB;

namespace Core.DTOs.Anime
{
    public class AnimeDetailed 
    {
        [Required]
        public ICollection<AnimeDescription?> AnimeDescription { get; set; } = new List<AnimeDescription?>();
        public string PosterUrl {get; set; } = string.Empty;
        public ICollection<Photo> Screenshots { get; set; } = new HashSet<Photo>();
    }
}

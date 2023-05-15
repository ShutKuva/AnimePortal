using System.ComponentModel.DataAnnotations;

namespace Core.DTOs.Anime
{
    public class AnimeDto
    {
        [Required]
        public ICollection<AnimeDescriptionDto?> AnimeDescription { get; set; } = new List<AnimeDescriptionDto?>();
        private DateTime _date;
        public string Duration { get; set; } = string.Empty;
        public DateTime Date
        {
            get => _date;
            set => _date = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
        public ICollection<string>? Tags { get; set; } = new HashSet<string>();
    }
}

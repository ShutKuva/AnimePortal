﻿
using Core.DTOs.Others;

namespace Core.DTOs.Anime
{
    public class AnimeDto
    {
        public ICollection<AnimeDescriptionDto?> AnimeDescription { get; set; } = new List<AnimeDescriptionDto?>();
        public ICollection<TagDto>? Tags { get; set; } = new HashSet<TagDto>();
        public string Duration { get; set; } = string.Empty;
        public string Studio { get; set; } = string.Empty;
        public float Rating { get; set; } = 0.0f;
        private DateTime _date;

        public DateTime Date
        {
            get => _date;
            set => _date = DateTime.SpecifyKind(value, DateTimeKind.Utc);
        }
    }
}

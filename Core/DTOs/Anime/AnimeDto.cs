using Core.DB;
using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs.Anime
{
    public class AnimeDto
    {
        public int? Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Placement { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public ICollection<Photo>? Photos { get; set; }
        public VideoTags? Tags { get; set; }
    }
}

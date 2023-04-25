using Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DB
{
    public class Anime : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Placement { get; set; } = string.Empty;
        public string Duration { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public float Rating { get; set; } = 0.0f;
        public ICollection<Photo>? Photos { get; set; }
        public DateTime Date { get; set; }
        public VideoTags? Tags { get; set; }
        public int PostedBy { get; set; }
        
    }
}

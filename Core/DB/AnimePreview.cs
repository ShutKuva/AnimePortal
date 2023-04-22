using Core.Enums;

namespace Core.DB
{
	public class AnimePreview : BaseEntity
	{
		public string Title { get; set; } = String.Empty;
		public string Placement { get; set; } = String.Empty;
		public string Duration { get; set; } = String.Empty;
		public string Description { get; set; } = String.Empty;
		public string ImageUrl { get; set; } = String.Empty;
        public DateTime Date { get; set; }
        public VideoTags Tags { get; set; }
    }
}


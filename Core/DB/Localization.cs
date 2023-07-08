using Core.DB.Videos;

namespace Core.DB
{
    public class Localization : BaseEntity
    {
        public ICollection<Video> Videos { get; set; } = new List<Video>();

        public int LocalizationProducerId { get; set; }
        public User LocalizationProducer { get; set; }
        public int LanguageId { get; set; }
        public Language Language { get; set; }
        public int AnimeId { get; set; }
        public Anime Anime { get; set; }
    }
}
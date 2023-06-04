
namespace Core.DB
{
    public class RelatedAnime : BaseEntity
    {
        public int AnimeId { get; set; }
        public int RelatedAnimeId { get; set; }
    }
}

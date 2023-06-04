
using Core.DB;
using Core.DTOs.Anime;

namespace Services.Abstraction.Interfaces
{
    public interface IRelatedAnimeService
    {
        Task<RelatedAnime> GetRelatedAnimeAsync(int animeId, int relatedId);
        Task<RelatedAnime> CreateRelatedAnimeAsync(RelatedAnimeDto relatedAnimeDto);
    }
}

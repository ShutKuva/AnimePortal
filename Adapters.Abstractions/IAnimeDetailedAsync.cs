

using Core.DTOs.Anime;

namespace Adapters.Abstractions
{
    public interface IAnimeDetailedAdapter
    {
        Task<AnimeDetailed> GetAnimeDetailedAsync(int animeId, string language);
        Task<ICollection<AnimeDetailed>> GetAnimesDetailedAsync(int quantity, string language);
    }
}

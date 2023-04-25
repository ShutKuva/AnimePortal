
using Core.DB;
using Core.DTOs.Anime;

namespace Services.Abstraction.Interfaces
{
    public interface IAnimeService
    {
        Task Create(Anime anime);
        Task<Anime> GetAnime(Anime anime);
        Task<AnimePreview> GetAnimePreview(Anime anime);
        Task<ICollection<AnimePreview>> GetAnimePreviews(int quantity);
        Task DeleteAnime(int animeId);

    }
}

using Core.DTOs.Anime;

namespace BLL.Abstractions.Interfaces.Adapters
{
    public interface IAnimePreviewAdapter
    {
        Task<AnimePreview>GetAnimePreviewAsync(int animeId);
        Task<ICollection<AnimePreview>> GetAnimePreviewsAsync(int quanitity);
    }
}

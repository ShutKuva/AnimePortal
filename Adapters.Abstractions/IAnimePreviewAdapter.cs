using Core.DTOs.Anime;

namespace BLL.Abstractions.Interfaces.Adapters
{
    public interface IAnimePreviewAdapter
    {
        Task<AnimePreview>GetAnimePreviewAsync(int animeId, string language);
        Task<ICollection<AnimePreview>> GetAnimePreviewsAsync(int quanitity, string language);
    }
}

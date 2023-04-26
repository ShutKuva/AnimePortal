
using Core.DB;
using Core.DTOs.Anime;
using Microsoft.AspNetCore.Http;

namespace Services.Abstraction.Interfaces
{
    public interface IAnimeService
    {
        Task CreateAsync(Anime? anime);
        Task<Anime> GetAnimeAsync(int animeId);
        Task<AnimePreview> GetAnimePreviewAsync(int animeId);
        Task<ICollection<AnimePreview>> GetAnimePreviewsAsync(int quantity);
        Task<Anime> UpdateAnimeAsync(Anime anime);
        Task DeleteAnimeAsync(int animeId);
        Task<Photo> AddAnimePhotoAsync(IFormFile file, int animeId);
        Task DeleteAnimePhotoAsync(int animeId, int photoId);

    }
}

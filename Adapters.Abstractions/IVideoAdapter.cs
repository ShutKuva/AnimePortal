using Core.DB;
using Core.DTOs.Anime.Videos;
using Core.Enums;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Adapters.Abstractions
{
    public interface IVideoAdapter
    {
        Task AddVideoAsProducerOfLocalizationAsync(ClaimsPrincipal user, int animeId, string language, VideoTypes videoType, IFormFile video);

        Task AddVideosAsProducerOfLocalizationAsync(ClaimsPrincipal user, int animeId, string language, VideoTypes videoType, IEnumerable<IFormFile> videos);

        Task<VideoDto> GetVideo(int videoId);

        Task<IEnumerable<VideoDto>> GetVideosOfAnime(int animeId, int producerId, string language);
    }
}
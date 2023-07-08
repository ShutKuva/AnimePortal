using Adapters.Abstractions;
using AutoMapper;
using BLL.Abstractions.Interfaces.Videos;
using Core.ClaimNames;
using Core.DB;
using Core.DB.Videos;
using Core.DTOs.Anime.Videos;
using Core.Enums;
using Microsoft.AspNetCore.Http;
using Services.Abstraction.Interfaces;
using System.Security.Claims;

namespace Adapters
{
    public class AnimeVideoAdapter : IVideoAdapter
    {
        private readonly IAnimeService _animeService;
        private readonly IVideoService _videoService;
        private readonly ILocalizationService _localizationService;
        private readonly ILanguageService _languageService;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IEnumerable<IVideoPlayerHandler> _handlers;

        public AnimeVideoAdapter(
            IAnimeService animeService,
            IVideoService videoService,
            ILocalizationService localizationService,
            ILanguageService languageService,
            IUserService userService,
            IMapper mapper,
            IEnumerable<IVideoPlayerHandler> handlers)
        {
            _animeService = animeService;
            _videoService = videoService;
            _localizationService = localizationService;
            _languageService = languageService;
            _userService = userService;
            _mapper = mapper;
            _handlers = handlers;
        }

        public async Task AddVideoAsProducerOfLocalizationAsync(ClaimsPrincipal user, int animeId, string language, VideoTypes videoType, IFormFile video)
        {
            Localization localization = await GetLocalization(animeId, language, user: user);
            await AddVideo(videoType, localization, video);
        }

        public async Task AddVideosAsProducerOfLocalizationAsync(ClaimsPrincipal user, int animeId, string language, VideoTypes videoType, IEnumerable<IFormFile> videos)
        {
            Localization localization = await GetLocalization(animeId, language, user: user);
            foreach (IFormFile video in videos)
            {
                await AddVideo(videoType, localization, video);
            }
        }

        public async Task<VideoDto> GetVideo(int videoId)
        {
            Video? video = await _videoService.GetVideoAsync(v => v.Id == videoId) ?? throw new ArgumentException("There is no video with this id.");

            return _mapper.Map<VideoDto>(video);
        }

        public async Task<IEnumerable<VideoDto>> GetVideosOfAnime(int animeId, int producerId, string language)
        {
            Localization localization = await GetLocalization(animeId, language, producerId: producerId);

            return _mapper.Map<IEnumerable<VideoDto>>(localization.Videos);
        }

        private async Task<Localization> GetLocalization(int animeId, string language, int producerId = -1, ClaimsPrincipal user = null!)
        {
            Language languageObject = await _languageService.GetLanguageByNameAsync(language);
            Anime anime = await _animeService.GetAnimeAsync(animeId);

            int id = -1;
            if (user != null)
            {
                id = int.Parse(user.FindFirst(UserClaimNames.Id)?.Value ?? throw new ArgumentException("There is no id claim in token."));
            }
            else
            {
                id = producerId;
            }
            User userObj = (await _userService.GetUserAsync(u => u.Id == id)) ?? throw new ArgumentException("There is no user with this id.");

            return await _localizationService.GetLocalizationAsync(anime, languageObject, userObj);
        }

        private async Task AddVideo(VideoTypes videoType, Localization localization, IFormFile video)
        {
            Stream videoStream = video.OpenReadStream();

            Video videoObject = await _videoService.AddVideoAsync(localization.Id, video.FileName, videoType, videoStream);

            foreach (IVideoPlayerHandler handler in _handlers)
            {
                await handler.CreatePlayerAsync(videoObject.Id, videoStream);
            }
        }
    }
}
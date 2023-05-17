using Adapters.Abstractions;
using AutoMapper;
using Core.DB;
using Core.DTOs.Anime;
using Core.Exceptions;
using Services.Abstraction.Interfaces;

namespace Adapters
{
    public class AnimeDetailedAdapter : IAnimeDetailedAdapter
    {
        private readonly IAnimeService _animeService;
        private readonly IMapper _mapper;

        public AnimeDetailedAdapter(IAnimeService animeService, IMapper mapper)
        {
            _animeService = animeService;
            _mapper = mapper;
        }

        public async Task<AnimeDetailed> GetAnimeDetailedAsync(int animeId, string language)
        {
            var anime = await _animeService.GetAnimeAsync(animeId);
            return MapAnimeDetailed(anime, language);
        }

        public async Task<ICollection<AnimeDetailed>> GetAnimesDetailedAsync(int quantity, string language)
        {
            var animes = await _animeService.GetAnimeByCountAsync(quantity, language);
            ICollection<AnimeDetailed> animePreviews = new List<AnimeDetailed>();

            foreach (Anime anime in animes)
            {
                animePreviews.Add(MapAnimeDetailed(anime, language));
            }
            return animePreviews;
        }

        private AnimeDetailed MapAnimeDetailed(Anime anime, string language)
        {
            var animePreview =
                _mapper.Map<AnimeDetailed>(anime, options =>
                {
                    options.Items.Add("DesiredLanguage", $"{language}");
                    options.AfterMap((_, _) =>
                    {
                        options.Items.Remove("DesiredLanguage");
                    });
                });

            if (animePreview.AnimeDescription?.Language == null)
            {
                throw new NotFoundException("Language not found");
            }

            return animePreview;
        }
    }
}

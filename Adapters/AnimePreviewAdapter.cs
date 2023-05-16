using Adapters.Abstractions;
using AutoMapper;
using Core.DB;
using Core.DTOs.Anime;
using Core.Exceptions;
using Services.Abstraction.Interfaces;

namespace Adapters
{
    public class AnimePreviewAdapter : IAnimePreviewAdapter
    {
        private readonly IAnimeService _animeService;
        private readonly IMapper _mapper;

        public AnimePreviewAdapter(IAnimeService animeService, IMapper mapper)
        {
            _animeService = animeService;
            _mapper = mapper;
        }

        public async Task<AnimePreview> GetAnimePreviewAsync(int animeId, string language)
        {
            var anime = await _animeService.GetAnimeAsync(animeId);
            return MapAnimePreview(anime, language);
        }

        public async Task<ICollection<AnimePreview>> GetAnimePreviewsAsync(int quantity, string language)
        {
            var animes = await _animeService.GetAnimeByCountAsync(quantity, language);
            ICollection<AnimePreview> animePreviews = new List<AnimePreview>(); 

            foreach (Anime anime in animes)
            {
                animePreviews.Add(MapAnimePreview(anime, language));
            }
            return animePreviews;
        }

        private AnimePreview MapAnimePreview(Anime anime, string language)
        {
            var animePreview =
                _mapper.Map<AnimePreview>(anime, options =>
                {
                    options.Items.Add("DesiredLanguage", $"{language}");
                    options.AfterMap((_, _) =>
                    {
                        options.Items.Remove("DesiredLanguage");
                    });
                });

            if (animePreview.AnimeDescription?.Language == null)
            {
                throw new NotFoundException("language not found");
            }
            return animePreview;
        }
    }

}

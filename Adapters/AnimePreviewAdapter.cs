using AutoMapper;
using BLL.Abstractions.Interfaces.Adapters;
using Core.DB;
using Core.DTOs.Anime;
using Services.Abstraction.Interfaces;

namespace BLL.Adapters
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
            var animePreview = _mapper.Map<AnimePreview>(anime);
            animePreview.AnimeDescription = _mapper.Map<AnimeDescriptionDto>(anime.AnimeDescriptions.FirstOrDefault(lang => lang?.Language?.Name == language));
            return animePreview;
        }

        public async Task<ICollection<AnimePreview>> GetAnimePreviewsAsync(int quantity, string language)
        {
            var animes = await _animeService.GetAnimeByCountAsync(quantity);
            ICollection<AnimePreview> animePreviews = new List<AnimePreview>(); 
            foreach (Anime anime in animes)
            {
                animePreviews.Add(await GetAnimePreviewAsync(anime.Id, language));
            }
            return animePreviews;
        }
    }
}

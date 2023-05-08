using AutoMapper;
using BLL.Abstractions.Interfaces.Adapters;
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

        public async Task<AnimePreview> GetAnimePreviewAsync(int animeId)
        {
            var anime = await _animeService.GetAnimeAsync(animeId);
            var animePreview = _mapper.Map<AnimePreview>(anime);
            return animePreview;
        }

        public async Task<ICollection<AnimePreview>> GetAnimePreviewsAsync(int quantity)
        {
            var animes = await _animeService.GetAnimeByCountAsync(quantity);
            var animePreviews = _mapper.Map<ICollection<AnimePreview>>(animes.ToList());
            return animePreviews;
        }
    }
}

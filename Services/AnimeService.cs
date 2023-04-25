using AutoMapper;
using Core.DB;
using Core.DTOs.Anime;
using DAL.Abstractions.Interfaces;
using Services.Abstraction.Interfaces;

namespace Services
{
    public class AnimeService:IAnimeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public AnimeService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task Create(Anime? anime)
        {
            if (anime != null)
            {
                await _uow.AnimeRepository.CreateAsync(anime);
            }
        }

        public async Task<Anime?> GetAnime(int animeId)
        {
            return await _uow.AnimeRepository.ReadAsync(animeId);
        }

        public async Task<AnimePreview?> GetAnimePreview(int animeId)
        {
            var anime = await _uow.AnimeRepository.ReadAsync(animeId);
            var animePreview = _mapper.Map<AnimePreview>(anime);
            return animePreview;
        }

        public async Task DeleteAnime(int animeId)
        {
            await _uow.AnimeRepository.DeleteAsync(animeId);
        }

        public Task<ICollection<AnimePreview>> GetAnimePreviews(int quantity)
        {
            var animes = _uow.AnimeRepository.GetAnimeByCount(quantity);
            var animePreviews = _mapper.Map<ICollection<AnimePreview>>(animes);
            return Task.FromResult(animePreviews);
        }
    }
}

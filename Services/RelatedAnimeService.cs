
using AutoMapper;
using Core.DB;
using Core.DTOs.Anime;
using DAL.Abstractions.Interfaces;
using Services.Abstraction.Interfaces;

namespace Services
{
    public class RelatedAnimeService : IRelatedAnimeService
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public RelatedAnimeService(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<RelatedAnime> GetRelatedAnimeAsync(int animeId, int relatedId)
        {
            var relatedAnime = await _uow.RelatedAnimeRepository.ReadByConditionAsync(r =>
                r.AnimeId == animeId && r.RelatedAnimeId == relatedId ||
                r.RelatedAnimeId == animeId && r.AnimeId == relatedId);

            return relatedAnime.FirstOrDefault()!;
        }

        public async Task<RelatedAnime> CreateRelatedAnimeAsync(RelatedAnimeDto relatedAnimeDto)
        {
            RelatedAnime relatedAnime = _mapper.Map<RelatedAnime>(relatedAnimeDto);
            await _uow.RelatedAnimeRepository.CreateAsync(relatedAnime);

            await _uow.SaveChangesAsync();
            return relatedAnime;
        }
    }
}

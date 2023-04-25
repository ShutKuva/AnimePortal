using Core.DB;
using Core.DTOs.Anime;
using Services.Abstraction.Interfaces;

namespace Services
{
    public class AnimeService:IAnimeService
    {
        public Task Create(Anime anime)
        {
            throw new NotImplementedException();
        }

        public Task<Anime> GetAnime(Anime anime)
        {
            throw new NotImplementedException();
        }

        public Task<AnimePreview> GetAnimePreview(Anime anime)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAnime(int animeId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<AnimePreview>> GetAnimePreviews(int quantity)
        {
            throw new NotImplementedException();
        }
    }
}

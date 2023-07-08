using System.Linq.Expressions;
using Core.DB;
using DAL.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL.Repositories
{
    public class AnimeRepository : GenericRepository<Anime>, IAnimeRepository
    {
        public AnimeRepository(AuthServerContext context) : base(context)
        {
        }

        public override async Task<Anime?> ReadAsync(int id)
        {
            var anime = await context.Animes.Include(p => p.Photos)
                .Include(a => a.AnimeDescriptions)
                .ThenInclude(l => l!.Language)
                .Include(a => a.AnimeDescriptions)
                .ThenInclude(a => a!.Genres)
                .Include(a=> a.Tags)
                .Include(a=>a.RelatedAnime)
                .Include(a=> a.Comments)
                .FirstOrDefaultAsync(user => user.Id == id);
            return anime;
        }

        public IQueryable<Anime> GetAnimeByCount(int count)
        {
            IQueryable<Anime> animes = context.Animes.Include(a => a.Photos)
                .Include(a => a.AnimeDescriptions)
                .ThenInclude(a => a!.Language)
                .Include(a => a.AnimeDescriptions)
                .ThenInclude(a => a!.Genres)
                .Include(a => a.Tags)
                .Include(a=>a.RelatedAnime)
                .Include(a => a.Comments)
                .Take(count);
            return animes;
        }

        public IQueryable<Anime> GetAnimeByCount(int count, string language)
        {
            IQueryable<Anime> animes = context.Animes
                .SelectMany(anime => anime.AnimeDescriptions, (anime, animeDescriptions) => new { anime, animeDescriptions })
                .Where(animeData => animeData.animeDescriptions!.Language!.Name == language)
                .Select(animeData => animeData.anime)
                .Include(a => a.Photos)
                .Include(a => a.AnimeDescriptions)
                .ThenInclude(a => a!.Language)
                .Include(a => a.AnimeDescriptions)
                .ThenInclude(a => a!.Genres)
                .Include(a => a.Tags)
                .Include(a => a.RelatedAnime)
                .Include(a=> a.Comments)
                .Take(count);

            return animes;
        }
    }
}


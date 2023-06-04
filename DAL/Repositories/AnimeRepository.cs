using System.Linq.Expressions;
using Core.DB;
using DAL.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL.Repositories
{
    public class AnimeRepository : IAnimeRepository
    {
        private readonly AuthServerContext _context;

        public AnimeRepository(AuthServerContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Anime entity)
        {
            await _context.Animes.AddAsync(entity);
        }

        public async Task<Anime?> ReadAsync(int id)
        {
            var anime = await _context.Animes.Include(p => p.Photos)
                .Include(a => a.AnimeDescriptions)
                .ThenInclude(l => l!.Language)
                .Include(a => a.AnimeDescriptions)
                .ThenInclude(a => a!.Genres)
                .Include(a=> a.Tags)
                .Include(a=>a.RelatedAnime)
                .FirstOrDefaultAsync(user => user.Id == id);
            return anime;
        }

        public async Task<IEnumerable<Anime>> ReadByConditionAsync(Expression<Func<Anime, bool>> predicate)
        {
            var animes = await _context.Animes.Where(predicate).ToListAsync();
            return animes;
        }

        public async Task UpdateAsync(Anime entity)
        {
            Anime? oldEntity = await ReadAsync(entity.Id);

            if (oldEntity == null)
            {
                await CreateAsync(entity);
            }
            else
            {
                EntityEntry<Anime> entityEntry = _context.Entry(oldEntity);
                entityEntry.CurrentValues.SetValues(entity);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var anime = await ReadAsync(id);
            if (anime != null)
            {
                _context.Animes.Remove(anime);
            }
        }

        public IQueryable<Anime> GetAnimeByCount(int count)
        {
            IQueryable<Anime> animes = _context.Animes.Include(a => a.Photos)
                .Include(a => a.AnimeDescriptions)
                .ThenInclude(a => a!.Language)
                .Include(a => a.AnimeDescriptions)
                .ThenInclude(a => a!.Genres)
                .Include(a => a.Tags)
                .Include(a=>a.RelatedAnime)
                .Take(count);
            return animes;
        }

        public IQueryable<Anime> GetAnimeByCount(int count, string language)
        {
            IQueryable<Anime> animes = _context.Animes
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
                .Take(count);

            return animes;
        }
    }
}


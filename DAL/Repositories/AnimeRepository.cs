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
            var user = await _context.Animes.Include(p=> p.Photos).FirstOrDefaultAsync(user => user.Id == id);
            return user;
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
            IQueryable<Anime> animes = _context.Animes.Include(p =>p.Photos).Take(count);
            return animes;
        }

    }
}


using System.Linq.Expressions;
using Core.DB;
using DAL.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL.Repositories
{
    public class RelatedAnimeRepository : IRelatedAnimeRepository
    {
        private readonly AuthServerContext _context;

        public RelatedAnimeRepository(AuthServerContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(RelatedAnime entity)
        {
            await _context.RelatedAnime.AddAsync(entity);
        }

        public async Task<RelatedAnime?> ReadAsync(int id)
        {
            RelatedAnime relatedAnime =
                (await _context.RelatedAnime.FirstOrDefaultAsync(p=> p.Id == id))!;
            return relatedAnime;
        }

        public async Task<IEnumerable<RelatedAnime>> ReadByConditionAsync(Expression<Func<RelatedAnime, bool>> predicate)
        {
            var relatedAnime = await _context.RelatedAnime.Where(predicate).ToListAsync();
            return relatedAnime;
        }

        public async Task UpdateAsync(RelatedAnime entity)
        {
            RelatedAnime? oldEntity = await ReadAsync(entity.Id);

            if (oldEntity == null)
            {
                await CreateAsync(entity);
            }
            else
            {
                EntityEntry<RelatedAnime> entityEntry = _context.Entry(oldEntity);
                entityEntry.CurrentValues.SetValues(entity);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var relatedAnime = await ReadAsync(id);
            if (relatedAnime != null)
            {
                _context.RelatedAnime.Remove(relatedAnime);
            }
        }
    }
}

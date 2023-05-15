using System.Linq.Expressions;
using Core.DB;
using DAL.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL.Repositories
{
    public class LanguageRepository : ILanguageRepository
    {
        private readonly AuthServerContext _context;

        public LanguageRepository(AuthServerContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Language entity)
        {
            await _context.Languages.AddAsync(entity);
        }

        public async Task<Language?> ReadAsync(int id)
        {
            var language = await _context.Languages.FirstOrDefaultAsync(l => l.Id == id);
            return language;
        }

        public async Task<IEnumerable<Language>> ReadByConditionAsync(Expression<Func<Language, bool>> predicate)
        {
            var languages = await _context.Languages.Where(predicate).ToListAsync();
            return languages;
        }

        public async Task UpdateAsync(Language entity)
        {
            Language? oldEntity = await ReadAsync(entity.Id);

            if (oldEntity == null)
            {
                await CreateAsync(entity);
            }
            else
            {
                EntityEntry<Language> entityEntry = _context.Entry(oldEntity);
                entityEntry.CurrentValues.SetValues(entity);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var language = await ReadAsync(id);
            if (language != null)
            {
                _context.Languages.Remove(language);
            }
        }
    }
}

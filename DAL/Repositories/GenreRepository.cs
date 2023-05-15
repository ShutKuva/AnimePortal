using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.DB;
using DAL.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL.Repositories
{
    public class GenreRepository : IGenreRepository
    {
        private readonly AuthServerContext _context;

        public GenreRepository(AuthServerContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Genre entity)
        {
            await _context.Genres.AddAsync(entity);
        }

        public async Task<Genre?> ReadAsync(int id)
        {
            var genre = await _context.Genres.FirstOrDefaultAsync(g=> g.Id == id);
            return genre;
        }

        public async Task<IEnumerable<Genre>> ReadByConditionAsync(Expression<Func<Genre, bool>> predicate)
        {
            var genres = await _context.Genres.Where(predicate).ToListAsync();
            return genres;
        }

        public async Task UpdateAsync(Genre entity)
        {
            Genre? oldEntity = await ReadAsync(entity.Id);

            if (oldEntity == null)
            {
                await CreateAsync(entity);
            }
            else
            {
                EntityEntry<Genre> entityEntry = _context.Entry(oldEntity);
                entityEntry.CurrentValues.SetValues(entity);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var genre = await ReadAsync(id);
            if (genre != null)
            {
                _context.Genres.Remove(genre);
            }
        }
    }
}

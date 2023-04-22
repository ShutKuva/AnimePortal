using System;
using Core.DB;
using System.Linq.Expressions;
using DAL.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL.Repositories
{
	public class AnimePreviewRepository : IAnimePreviewRepository
    {
        private readonly AuthServerContext _context;

        public AnimePreviewRepository(AuthServerContext context)
        {
            _context = context;
        }

        public void Create(AnimePreview entity)
        {
            _context.AnimePreviews.Add(entity);
        }

        public async Task CreateAsync(AnimePreview entity)
        {
            await _context.AnimePreviews.AddAsync(entity);
        }

        public AnimePreview? Read(int id)
        {
            return _context.AnimePreviews.FirstOrDefault(user => user.Id == id);
        }

        public async Task<AnimePreview?> ReadAsync(int id)
        {
            var preview = await _context.AnimePreviews.FirstOrDefaultAsync(user => user.Id == id);
            return preview;
        }

        public IEnumerable<AnimePreview?> ReadByCondition(Expression<Func<AnimePreview, bool>> predicate)
        {
            return _context.Set<AnimePreview>().Where(predicate).ToList();
        }

        public async Task<IEnumerable<AnimePreview>> ReadByConditionAsync(Expression<Func<AnimePreview, bool>> predicate)
        {
            var users = await _context.AnimePreviews.Where(predicate).ToListAsync();
            return users;
        }

        public async Task UpdateAsync(AnimePreview entity)
        {
            AnimePreview? oldEntity = await ReadAsync(entity.Id);

            if (oldEntity == null)
            {
                await CreateAsync(entity);
            }
            else
            {
                EntityEntry<AnimePreview> entityEntry = _context.Entry(oldEntity);

                entityEntry.CurrentValues.SetValues(entity);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var user = await ReadAsync(id);
            if (user != null)
            {
                _context.AnimePreviews.Remove(user);
            }
        }
    }
}


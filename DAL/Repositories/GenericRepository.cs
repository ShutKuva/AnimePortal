using Core.DB;
using DAL.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AuthServerContext context;

        public GenericRepository(AuthServerContext context)
        {
            this.context = context;
        }

        public virtual async Task CreateAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await ReadAsync(id);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
            }
        }

        public virtual async Task<T?> ReadAsync(int id)
        {
            var entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
            return entity;
        }

        public virtual async Task<IEnumerable<T>> ReadByConditionAsync(Expression<Func<T, bool>> predicate)
        {
            var entities = await context.Set<T>().Where(predicate).ToListAsync();
            return entities;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            T? oldEntity = await ReadAsync(entity.Id);

            if (oldEntity == null)
            {
                await CreateAsync(entity);
            }
            else
            {
                EntityEntry<T> entityEntry = context.Entry(oldEntity);
                entityEntry.CurrentValues.SetValues(entity);
            }
        }
    }
}
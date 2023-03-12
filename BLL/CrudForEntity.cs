using BLL.Abstractions;
using Core.DB;
using DAL;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.Emit;

namespace BLL
{
    public class CrudForEntity<T> : ICrudService<T> where T : BaseEntity
    {
        private readonly AuthServerContext _context;

        public CrudForEntity(AuthServerContext context)
        {
            _context = context;
        }

        public Task Create(T entity)
        {
            _context.Add(entity);

            _context.SaveChanges();

            return Task.CompletedTask;
        }

        public async Task Delete(int id)
        {
            T? entity = await Read(id);

            if (entity == null)
            {
                throw new ArgumentException("There is no user with this id");
            }

            _context.Remove(entity);

            _context.SaveChanges();
        }

        public Task<T?> Read(int id)
        {
            return Task.FromResult(_context.Set<T>().FirstOrDefault(entity => entity.Id == id));
        }

        public Task<IEnumerable<T>> ReadByCondition(Expression<Func<T, bool>> predicate)
        {
            return Task.FromResult(_context.Set<T>().Where(predicate).AsEnumerable());
        }

        public async Task Update(T entity)
        {
            T? oldEntity = await Read(entity.Id);

            if (oldEntity == null)
            {
                await Create(entity);
            } else
            {
                EntityEntry<T> entityEntry = _context.Entry(oldEntity);

                entityEntry.CurrentValues.SetValues(entity);
            }

            _context.SaveChanges();
        }
    }
}
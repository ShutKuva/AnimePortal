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
    public class UserRepository : IUserRepository
    {
        private readonly AuthServerContext _context;

        public UserRepository(AuthServerContext context)
        {
            _context = context;
        }

        public void Create(User entity)
        {
            _context.Users.Add(entity);
        }

        public async Task CreateAsync(User entity)
        {
            await _context.Users.AddAsync(entity);
        }

        public User? Read(int id)
        {
            return _context.Users.FirstOrDefault(user => user.Id == id);
        }

        public async Task<User?> ReadAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(user => user.Id == id);
            return user;
        }

        public IEnumerable<User?> ReadByCondition(Expression<Func<User, bool>> predicate)
        {
            return _context.Set<User>().Where(predicate).AsEnumerable();
        }

        public async Task<IEnumerable<User>> ReadByConditionAsync(Expression<Func<User, bool>> predicate)
        {
            var users = await _context.Users.Where(predicate).ToListAsync();
            return users;
        }

        public async Task UpdateAsync(User entity)
        {
            User? oldEntity = await ReadAsync(entity.Id);

            if (oldEntity == null)
            {
                await CreateAsync(entity);
            }
            else
            {
                EntityEntry<User> entityEntry = _context.Entry(oldEntity);

                entityEntry.CurrentValues.SetValues(entity);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var user = await ReadAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
        }

    }
}

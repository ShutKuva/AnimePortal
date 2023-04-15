﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.DB;
using DAL.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            return _context.Set<User>().Where(predicate).ToList();
        }

        public Task Update(User entity)
        {
            _context.Users.Update(entity);
            return Task.CompletedTask;
        }

        public void Delete(int id)
        {
            var user = Read(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
        }
    }
}
using System.Linq.Expressions;
using Core.DB;
using DAL.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly AuthServerContext _context;

        public CommentRepository(AuthServerContext context)
        {
            _context = context;
        }
        public async Task CreateAsync(Comment entity)
        {
            await _context.Comments.AddAsync(entity);
        }

        public async Task<Comment?> ReadAsync(int id)
        {
            var comment = await _context.Comments
                .Include(c => c.ParentComment)
                .FirstOrDefaultAsync(c => c.Id == id);
            return comment;
        }

        public async Task<IEnumerable<Comment>> ReadByConditionAsync(Expression<Func<Comment, bool>> predicate)
        {
            var commentaries = await _context.Comments.Where(predicate).ToListAsync();
            return commentaries;
        }

        public async Task UpdateAsync(Comment entity)
        {
            Comment? oldEntity = await ReadAsync(entity.Id);

            if (oldEntity == null)
            {
                await CreateAsync(entity);
            }
            else
            {
                EntityEntry<Comment> entityEntry = _context.Entry(oldEntity);
                entityEntry.CurrentValues.SetValues(entity);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var comment = await ReadAsync(id);
            if (comment != null)
            {
                _context.Comments.Remove(comment);
            }
        }
    }
}

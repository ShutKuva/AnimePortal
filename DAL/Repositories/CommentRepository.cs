using System.Linq.Expressions;
using Core.DB;
using DAL.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DAL.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        public CommentRepository(AuthServerContext context) : base(context)
        {
        }

        public override async Task<Comment?> ReadAsync(int id)
        {
            var comment = await context.Comments
                .Include(c => c.ParentComment)
                .FirstOrDefaultAsync(c => c.Id == id);
            return comment;
        }
    }
}

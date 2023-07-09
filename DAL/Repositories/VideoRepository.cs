using Core.DB.Videos;
using DAL.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class VideoRepository : GenericRepository<Video>, IVideoRepository
    {
        public VideoRepository(AuthServerContext context) : base(context)
        {
        }

        public override async Task<Video?> ReadAsync(int id)
        {
            var video = await context.Videos.Include(v => v.Players).FirstOrDefaultAsync(e => e.Id == id);
            return video;
        }

        public override async Task<IEnumerable<Video>> ReadByConditionAsync(Expression<Func<Video, bool>> predicate)
        {
            var videos = await context.Videos.Include(v => v.Players).Where(predicate).ToListAsync();
            return videos;
        }
    }
}
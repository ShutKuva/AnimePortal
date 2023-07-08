using Core.DB.Videos;
using DAL.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DAL.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        private readonly AuthServerContext _context;

        public VideoRepository(AuthServerContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Video entity)
        {
            await _context.Videos.AddAsync(entity);
        }

        public async Task<Video?> ReadAsync(int id)
        {
            var video = await _context.Videos.FirstOrDefaultAsync(photo => photo.Id == id);
            return video;
        }

        public async Task<IEnumerable<Video>> ReadByConditionAsync(Expression<Func<Video, bool>> predicate)
        {
            var videos = await _context.Videos.Where(predicate).ToListAsync();
            return videos;
        }

        public Task UpdateAsync(Video entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            var video = await ReadAsync(id);
            if (video != null)
            {
                _context.Videos.Remove(video);
            }
        }
    }
}
using System.Linq.Expressions;
using Core.DB;
using DAL.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class PhotoRepository : IPhotoRepository
    {
        private readonly AuthServerContext _context;

        public PhotoRepository(AuthServerContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Photo entity)
        {
            await _context.Photos.AddAsync(entity);
        }

        public async Task<Photo?> ReadAsync(int id)
        {
            var photo = await _context.Photos.FirstOrDefaultAsync(photo => photo.Id == id);
            return photo;
        }

        public async Task<IEnumerable<Photo>> ReadByConditionAsync(Expression<Func<Photo, bool>> predicate)
        {
            var photos = await _context.Photos.Where(predicate).ToListAsync();
            return photos;
        }

        public Task UpdateAsync(Photo entity)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            var photo = await ReadAsync(id);
            if (photo != null)
            {
                _context.Photos.Remove(photo);
            }
        }

        public IEnumerable<Photo> GetPhotos()
        {
            return _context.Photos.ToList();
        }
    }
}

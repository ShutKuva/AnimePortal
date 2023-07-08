using System.Linq.Expressions;
using Core.DB;
using DAL.Abstractions.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class PhotoRepository : GenericRepository<Photo>, IPhotoRepository
    {
        public PhotoRepository(AuthServerContext context) : base(context)
        {
        }

        public IEnumerable<Photo> GetPhotos()
        {
            return context.Photos.ToList();
        }
    }
}

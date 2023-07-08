using Core.DB.Videos;
using DAL.Abstractions.Interfaces;

namespace DAL.Repositories.VideoRepositories
{
    public class CloudinaryVideoRepository : GenericRepository<CloudinaryVideo>, ICloudinaryVideoRepository
    {
        public CloudinaryVideoRepository(AuthServerContext context) : base(context)
        {
        }
    }
}
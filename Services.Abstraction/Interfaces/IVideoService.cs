using Core.DB.Videos;
using Core.Enums;
using System.Linq.Expressions;

namespace Services.Abstraction.Interfaces
{
    public interface IVideoService
    {
        Task<Video> AddVideoAsync(int localizationId, string videoName, VideoTypes videoType, Stream videoByteStream);

        Task<Video?> GetVideoAsync(Expression<Func<Video, bool>> predicate);
        Task<IEnumerable<Video>> GetVideosAsync(Expression<Func<Video, bool>> predicate);

        Task DeleteVideoAsync(string videoName);

        Task DeleteVideoAsync(int id);
    }
}
using Core.DB;

namespace BLL.Abstractions.Interfaces.Videos
{
    public interface IVideoPlayerHandler
    {
        Task CreatePlayerAsync(int videoId, Stream videoByteStream);
        Task FreeVideoResourcesAsync(int videoId);
    }
}
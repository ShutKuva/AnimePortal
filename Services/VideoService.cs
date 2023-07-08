using Core.Enums;
using DAL.Abstractions.Interfaces;
using Services.Abstraction.Interfaces;
using System.Linq.Expressions;
using Video = Core.DB.Videos.Video;

namespace Services
{
    public class VideoService : IVideoService
    {
        private readonly IUnitOfWork _unitOfWork;

        public VideoService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Video?> GetVideoAsync(Expression<Func<Video, bool>> predicate)
        {
            IEnumerable<Video> videos = await _unitOfWork.VideoRepository.ReadByConditionAsync(predicate);
            return videos.FirstOrDefault();
        }

        public Task<IEnumerable<Video>> GetVideosAsync(Expression<Func<Video, bool>> predicate)
        {
            return _unitOfWork.VideoRepository.ReadByConditionAsync(predicate);
        }

        public async Task<Video> AddVideoAsync(int localizationId, string videoName, VideoTypes videoType, Stream videoByteStream)
        {
            IEnumerable<Video> videos = await _unitOfWork.VideoRepository.ReadByConditionAsync(v => v.Title == videoName);
            
            if (videos.Any())
            {
                throw new ArgumentException("There is video with this video name.");
            }

            Video video = new Video()
            {
                Title = videoName,
                VideoType = videoType,
                LocalizationId = localizationId,
            };

            await _unitOfWork.VideoRepository.CreateAsync(video);
            await _unitOfWork.SaveChangesAsync();

            return video;
        }

        public Task DeleteVideoAsync(string videoName)
        {
            return DeleteVideoAsync(_unitOfWork.VideoRepository.ReadByConditionAsync(v => v.Title == videoName));
        }

        public Task DeleteVideoAsync(int id)
        {
            return DeleteVideoAsync(_unitOfWork.VideoRepository.ReadByConditionAsync(v => v.Id == id));
        }

        private async Task DeleteVideoAsync(Task<IEnumerable<Video>> videosTask)
        {
            IEnumerable<Video> videos = await videosTask;

            if (!videos.Any())
            {
                throw new ArgumentException("There are no videos that fit predicate.");
            }

            foreach (Video video in videos)
            {
                await _unitOfWork.VideoRepository.DeleteAsync(video.Id);
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
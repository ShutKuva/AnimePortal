using BLL.Abstractions.Interfaces.Videos;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.DB;
using Core.DB.Videos;
using Core.DI;
using DAL.Abstractions.Interfaces;
using Microsoft.Extensions.Options;

namespace BLL.Videos
{
    public class CloudinaryVideoPlayerHandler : IVideoPlayerHandler
    {
        public const string PLAYER_NAME = "Cloudinary player";

        private readonly Cloudinary _cloudinary;
        private readonly CloudinaryPlayerConfigurations _cloudinaryPlayerConfigurations;
        private readonly IUnitOfWork _unitOfWork;

        public CloudinaryVideoPlayerHandler(
            IOptions<CloudinarySettings> cloudinaryConfigurationOptions,
            IOptions<CloudinaryPlayerConfigurations> cloudinaryPlayerConfigurations,
            IUnitOfWork unitOfWork)
        {
            CloudinarySettings configuration = cloudinaryConfigurationOptions.Value ?? throw new ArgumentNullException(nameof(cloudinaryConfigurationOptions));

            _cloudinary = new Cloudinary(
                new Account(
                    configuration.CloudName,
                    configuration.ApiKey,
                    configuration.ApiSecret
                    )
                );

            _cloudinaryPlayerConfigurations = cloudinaryPlayerConfigurations.Value ?? throw new ArgumentNullException(nameof(cloudinaryPlayerConfigurations));
            _unitOfWork = unitOfWork;
        }

        public async Task CreatePlayerAsync(int videoId, Stream videoByteStream)
        {
            if (videoByteStream.Length == 0)
            {
                throw new ArgumentException("Video is empty.");
            }

            VideoUploadParams videoUploadParams = new VideoUploadParams()
            {
                File = new FileDescription(videoId.ToString(), videoByteStream),
            };

            VideoUploadResult response = await _cloudinary.UploadAsync(videoUploadParams);

            if (response.Error != null)
            {
                throw new ArgumentException($"Cloudinary error: {response.Error}");
            }

            CloudinaryVideo video = new CloudinaryVideo()
            {
                PublicId = response.PublicId,
                Title = videoId.ToString(),
                Url = response.Url.AbsoluteUri,
            };

            await _unitOfWork.CloudinaryVideoRepository.CreateAsync(video);

            Player player = new Player()
            {
                Name = PLAYER_NAME,
                Url = Path.Combine(_cloudinaryPlayerConfigurations.Url, video.PublicId),
                VideoId = videoId,
            };

            await _unitOfWork.PlayerRepository.CreateAsync(player);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task FreeVideoResourcesAsync(int videoId)
        {
            IEnumerable<CloudinaryVideo> videos = await _unitOfWork.CloudinaryVideoRepository.ReadByConditionAsync(cv => cv.VideoId == videoId);

            if (!videos.Any())
            {
                throw new ArgumentException("There are no videos with this video name.");
            }

            CloudinaryVideo video = videos.First();

            DeletionParams deletionParams = new DeletionParams(video.PublicId);

            DeletionResult response = await _cloudinary.DestroyAsync(deletionParams);

            if (response.Error != null)
            {
                throw new ArgumentException($"Error occured in Cloudinary deletion: {response.Error.Message}");
            }

            await _unitOfWork.CloudinaryVideoRepository.DeleteAsync(video.Id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}

using BLL.Abstractions.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.DI;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace BLL.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;

        public PhotoService(IOptions<CloudinarySettings> cloudinaryConfig)
        {
            CloudinarySettings cloudinaryConfiguration = cloudinaryConfig.Value ?? throw new ArgumentNullException(nameof(cloudinaryConfig));

            Account account = new(
                cloudinaryConfiguration.CloudName,
                cloudinaryConfiguration.ApiKey,
                cloudinaryConfiguration.ApiSecret);
            _cloudinary = new Cloudinary(account);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<ImageUploadResult> UploadPhotoAsync(IFormFile photo)
        {
            var uploadResult = new ImageUploadResult();
            if (photo.Length > 0)
            {
                await using Stream stream = photo.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(photo.FileName, stream),
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }
            return uploadResult;
        }

        public Task<DeletionResult> DeletePhotoAsync(string cloudinaryId)
        {
            DeletionParams deleteParams = new(cloudinaryId);
            return _cloudinary.DestroyAsync(deleteParams);
        }
    }
}

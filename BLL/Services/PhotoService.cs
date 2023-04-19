using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL.Abstractions.Interfaces;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.DI;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace BLL.Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;

        public PhotoService(IOptions<CloudinarySettings> cloudinaryConfig)
        {
            Account account = new(
                cloudinaryConfig.Value.CloudName,
                cloudinaryConfig.Value.ApiKey,
                cloudinaryConfig.Value.ApiSecret);
            _cloudinary = new Cloudinary(account);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<ImageUploadResult> UploadPhotoAsync(IFormFile photo)
        {
            var uploadResult = new ImageUploadResult();
            if (photo.Length > 0)
            {
                await using var stream = photo.OpenReadStream();
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

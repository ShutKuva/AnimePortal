using System.Runtime.InteropServices;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Core.DB;
using Core.DI;
using Core.Enums;
using Core.Exceptions;
using DAL.Abstractions.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Services.Abstraction;
using static System.Net.Mime.MediaTypeNames;

namespace Services
{
    public class PhotoService : IPhotoService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IUnitOfWork _uow;

        public PhotoService(IOptions<CloudinarySettings> cloudinaryConfig, IUnitOfWork uow)
        {
            _uow = uow;
            CloudinarySettings cloudinaryConfiguration = cloudinaryConfig.Value ?? throw new ArgumentNullException(nameof(cloudinaryConfig));

            Account account = new(
                cloudinaryConfiguration.CloudName,
                cloudinaryConfiguration.ApiKey,
                cloudinaryConfiguration.ApiSecret);
            _cloudinary = new Cloudinary(account);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<Photo> UploadPhotoAsync(IFormFile photo, PhotoTypes photoTypes = PhotoTypes.Screenshots)
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

            if (uploadResult.Error != null)
            {
                throw new InvalidOperationException(uploadResult.Error.Message);
            }

            var image = await CreatePhotoAsync(uploadResult, photoTypes);

            return image;
        }

        public async Task<Photo> CreatePhotoAsync(ImageUploadResult result, PhotoTypes photoTypes = PhotoTypes.Screenshots)
        {
            var photo = new Photo()
            {
                ImageUrl = result.Url.AbsoluteUri,
                Title = result.OriginalFilename,
                PublicId = result.PublicId,
                PhotoType = photoTypes
            };

            await _uow.PhotoRepository.CreateAsync(photo);
            await _uow.SaveChangesAsync();

            return photo;
        } 

        public async Task<DeletionResult> DeletePhotoAsync(int photoId)
        {
            var photo = await GetPhotoAsync(photoId);
            await _uow.PhotoRepository.DeleteAsync(photoId);

            DeletionParams deleteParams = new(photo.PublicId);

            await _uow.SaveChangesAsync();
            return await _cloudinary.DestroyAsync(deleteParams);
        }

        public async Task<Photo> GetPhotoAsync(int id)
        {
            var photo = await _uow.PhotoRepository.ReadAsync(id) ??
                        throw new NotFoundException($"Resource with id {id} was not found.");
            return photo;
        }
    }
}

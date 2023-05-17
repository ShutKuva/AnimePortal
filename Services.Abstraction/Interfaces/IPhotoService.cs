using CloudinaryDotNet.Actions;
using Core.DB;
using Core.Enums;
using Microsoft.AspNetCore.Http;

namespace Services.Abstraction
{
    public interface IPhotoService
    {
        Task<Photo> UploadPhotoAsync(IFormFile photo, PhotoTypes photoTypes = PhotoTypes.Screenshots);
        Task<DeletionResult> DeletePhotoAsync(int photoId);
        Task<Photo> GetPhotoAsync(int id);
        public Task<Photo> CreatePhotoAsync(ImageUploadResult result, PhotoTypes photoTypes = PhotoTypes.Screenshots);
    }
}

using CloudinaryDotNet.Actions;
using Core.DB;
using Microsoft.AspNetCore.Http;

namespace Services.Abstraction
{
    public interface IPhotoService
    {
        Task<Photo> UploadPhotoAsync(IFormFile photo);
        Task<DeletionResult> DeletePhotoAsync(int photoId);
        Task<Photo> GetPhotoAsync(int id);
        public Task<Photo> CreatePhotoAsync(ImageUploadResult result);
    }
}

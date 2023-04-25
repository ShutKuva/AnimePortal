using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace Services.Abstraction
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> UploadPhotoAsync(IFormFile photo);
        Task<DeletionResult> DeletePhotoAsync(string cloudinaryId);
    }
}

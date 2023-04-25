using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;

namespace BLL.Abstractions.Interfaces
{
    public interface IPhotoService
    {
        Task<ImageUploadResult> UploadPhotoAsync(IFormFile photo);
        Task<DeletionResult> DeletePhotoAsync(string cloudinaryId);
    }
}

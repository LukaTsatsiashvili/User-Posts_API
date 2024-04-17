using EntityLayer.DTOs.API.Image;
using EntityLayer.Entities;

namespace ServiceLayer.Services.API.Abstract
{
    public interface IImageService
    {
        Task<ImageDTO> UploadImageAsync(Image model);
        Task<bool> RemoveImageAsync(Guid id);
    }
}

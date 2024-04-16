using EntityLayer.DTOs.Image;
using EntityLayer.Entities;

namespace ServiceLayer.Services.Abstract
{
    public interface IImageService
    {
        Task<ImageDTO> UploadImageAsync(Image model);
        Task<bool> RemoveImageAsync(Guid id);
    }
}

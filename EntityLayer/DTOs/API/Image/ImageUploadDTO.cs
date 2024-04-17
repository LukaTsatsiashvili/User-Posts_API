using Microsoft.AspNetCore.Http;

namespace EntityLayer.DTOs.API.Image
{
    public class ImageUploadDTO
    {
        public IFormFile File { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
    }
}

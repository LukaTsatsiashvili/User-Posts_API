using AutoMapper;
using EntityLayer.DTOs.API.Image;
using EntityLayer.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Services.API.Abstract;

namespace ServiceLayer.Services.API.Concrete
{
    public class ImageService : IImageService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Image> _repository;
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _contextAccessor;

        public ImageService(IMapper mapper, IUnitOfWork unitOfWork, IWebHostEnvironment environment, IHttpContextAccessor contextAccessor)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GetGenericRepository<Image>();
            _environment = environment;
            _contextAccessor = contextAccessor;
        }

        public async Task<ImageDTO> UploadImageAsync(Image model)
        {
            // Create file path where images will be saved
            var localFilePath = Path.Combine(_environment.ContentRootPath, "Images", $"{model.FileName}{model.FileExtension}");

            // Upload Image
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await model.File.CopyToAsync(stream);

            // Create URL that points to the uploaded image
            var urlFilePath = $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}{_contextAccessor.HttpContext.Request.PathBase}/Images/{model.FileName}{model.FileExtension}";
            model.FilePath = urlFilePath;

            // Add image to Images table
            await _repository.AddEntityAsync(model);
            await _unitOfWork.SaveAsync();

            var result = _mapper.Map<ImageDTO>(model);
            return result;
        }

        public async Task<bool> RemoveImageAsync(Guid id)
        {
            var image = await _repository.GetEntityByIdAsync(id);
            if (image == null || id == Guid.Empty)
            {
                return false;
            }

            // Constructing a file path where image is stored 
            var imagePath = Path.Combine(_environment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");
            if (File.Exists(imagePath))
            {
                File.Delete(imagePath);
            }

            _repository.DeleteEntity(image);
            await _unitOfWork.SaveAsync();

            return true;
        }
    }
}

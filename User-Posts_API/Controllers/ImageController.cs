using AutoMapper;
using EntityLayer.DTOs.Image;
using EntityLayer.Entities;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.FluentValidation.ImageValidator;
using ServiceLayer.Services.Abstract;

namespace User_Posts_API.Controllers
{
    [Route("api/ImageServices")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageService _service;
        private readonly ImageUploadValidator _imageValidator;
        private readonly IMapper _mapper;

        public ImageController(IImageService service, ImageUploadValidator imageValidator, IMapper mapper)
        {
            _service = service;
            _imageValidator = imageValidator;
            _mapper = mapper;
        }

        [HttpPost("UploadImage")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UploadImage([FromForm] ImageUploadDTO model)
        {
            ValidateFileUpload(model);

            var validation = await _imageValidator.ValidateAsync(model);
            if (!validation.IsValid || !ModelState.IsValid)
            {
                validation.AddToModelState(this.ModelState);
                return BadRequest(ModelState);
            }

            var domainModel = _mapper.Map<Image>(model);

            var result = await _service.UploadImageAsync(domainModel);

            return Ok(result);
        }


        [HttpDelete("RemoveImage/{id:Guid}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> RemoveImage([FromRoute] Guid id)
        {
            var result = await _service.RemoveImageAsync(id);
            if (!result)
            {
                return NotFound("Invalid Id!");
            }

            return NoContent();
        }



        private void ValidateFileUpload(ImageUploadDTO model)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg" };

            if (!allowedExtensions.Contains(Path.GetExtension(model.File.FileName)))
            {
                ModelState.AddModelError("file", "Only '.jpg' or '.jpeg' file extensions are supported!");
            }

            if (model.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "Only files with size 10MB or less are allowed!");
            }
        } 
    }
}

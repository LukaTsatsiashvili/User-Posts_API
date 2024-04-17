using AutoMapper;
using EntityLayer.DTOs.API.Image;
using EntityLayer.Entities;

namespace ServiceLayer.AutoMapper.API
{
    public class ImageMapper : Profile
    {
        public ImageMapper()
        {
            CreateMap<Image, ImageDTO>().ReverseMap();

            // Configuring to Map 'FileExtension' and 'FileSize' properly  
            CreateMap<ImageUploadDTO, Image>()
                .ForMember(x => x.FileExtension, opt => opt.MapFrom(src => Path.GetExtension(src.File.FileName)))
                .ForMember(x => x.FileSizeInBytes, opt => opt.MapFrom(src => src.File.Length))
                .ReverseMap();
        }
    }
}

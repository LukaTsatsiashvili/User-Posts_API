using AutoMapper;
using EntityLayer.DTOs.Post;
using EntityLayer.Entities;

namespace ServiceLayer.AutoMapper
{
    public class PostMapper : Profile
    {
        public PostMapper()
        {
            CreateMap<Post, PostListDTO>().ReverseMap();
            CreateMap<Post, PostCreateDTO>().ReverseMap();
            CreateMap<Post, PostUpdateDTO>().ReverseMap();
        }
    }
}

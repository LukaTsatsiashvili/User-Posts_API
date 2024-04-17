using AutoMapper;
using EntityLayer.DTOs.API.Post;
using EntityLayer.Entities;

namespace ServiceLayer.AutoMapper.API
{
    public class PostMapper : Profile
    {
        public PostMapper()
        {
            CreateMap<Post, PostDTO>().ReverseMap();
            CreateMap<Post, PostListDTO>().ReverseMap();
            CreateMap<Post, PostCreateDTO>().ReverseMap();
            CreateMap<Post, PostUpdateDTO>().ReverseMap();
        }
    }
}

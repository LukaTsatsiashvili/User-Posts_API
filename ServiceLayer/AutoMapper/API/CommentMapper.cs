using AutoMapper;
using EntityLayer.DTOs.API.Comment;
using EntityLayer.Entities;

namespace ServiceLayer.AutoMapper.API
{
    public class CommentMapper : Profile
    {
        public CommentMapper()
        {
            CreateMap<Comment, CommentDTO>().ReverseMap();
            CreateMap<Comment, CommentListDTO>().ReverseMap();
            CreateMap<Comment, CommentCreateDTO>().ReverseMap();
            CreateMap<Comment, CommentUpdateDTO>().ReverseMap();
        }
    }
}

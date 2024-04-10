using AutoMapper;
using EntityLayer.DTOs.Comment;
using EntityLayer.Entities;

namespace ServiceLayer.AutoMapper
{
    public class CommentMapper : Profile
    {
        public CommentMapper()
        {
            CreateMap<Comment, CommentListDTO>().ReverseMap();
            CreateMap<Comment, CommentCreateDTO>().ReverseMap();
            CreateMap<Comment, CommentUpdateDTO>().ReverseMap();
        }
    }
}

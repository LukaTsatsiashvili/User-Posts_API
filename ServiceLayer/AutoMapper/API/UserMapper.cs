using AutoMapper;
using EntityLayer.DTOs.API.User;
using EntityLayer.Entities;

namespace ServiceLayer.AutoMapper.API
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserListDTO>().ReverseMap();
            CreateMap<User, UserCreateDTO>().ReverseMap();
            CreateMap<User, UserUpdateDTO>().ReverseMap();
        }
    }
}

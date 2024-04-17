using AutoMapper;
using EntityLayer.DTOs.LogIn;
using EntityLayer.DTOs.Register;
using Microsoft.AspNetCore.Identity;

namespace ServiceLayer.AutoMapper
{
    public class AuthMapper : Profile
    {
        public AuthMapper()
        {
            CreateMap<RegisterRequestDTO, IdentityUser>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(src => src.Email))
                .ForMember(x => x.Email, opt => opt.MapFrom(src => src.Email))
                .ReverseMap();

            CreateMap<LogInRequestDTO, LogInResponseDTO>().ReverseMap();
        }
    }
}

using AutoMapper;
using EntityLayer.DTOs.Identity.LogIn;
using EntityLayer.DTOs.Identity.Register;
using Microsoft.AspNetCore.Identity;

namespace ServiceLayer.AutoMapper.Identity
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

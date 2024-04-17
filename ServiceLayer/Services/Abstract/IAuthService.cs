using EntityLayer.DTOs.LogIn;
using EntityLayer.DTOs.Register;
using Microsoft.AspNetCore.Identity;

namespace ServiceLayer.Services.Abstract
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(RegisterRequestDTO model);
        Task<LogInResponseDTO> LogInAsync(LogInRequestDTO model);
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}

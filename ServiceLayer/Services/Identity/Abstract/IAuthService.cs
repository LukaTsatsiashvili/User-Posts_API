using EntityLayer.DTOs.Identity.LogIn;
using EntityLayer.DTOs.Identity.Register;
using Microsoft.AspNetCore.Identity;

namespace ServiceLayer.Services.Identity.Abstract
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(RegisterRequestDTO model);
        Task<LogInResponseDTO> LogInAsync(LogInRequestDTO model);
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}

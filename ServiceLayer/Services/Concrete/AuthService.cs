using AutoMapper;
using EntityLayer.DTOs.LogIn;
using EntityLayer.DTOs.Register;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceLayer.Services.Abstract;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServiceLayer.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public AuthService(UserManager<IdentityUser> userManager, IMapper mapper, IConfiguration config)
        {
            _userManager = userManager;
            _mapper = mapper;
            _config = config;
        }



        public async Task<IdentityResult> RegisterAsync(RegisterRequestDTO model)
        {
            var identityUser = _mapper.Map<IdentityUser>(model);

            var identityResult = await _userManager.CreateAsync(identityUser, model.Password);

            if (!identityResult.Succeeded)
            {
                return null;
            }

            // Every new registered user gets 'Member' - role by default 
            var roles = new[] { "Member" };

            identityResult = await _userManager.AddToRolesAsync(identityUser, roles);
            if (!identityResult.Succeeded)
            {
                return null;
            }


            return identityResult;
        }

        public async Task<LogInResponseDTO> LogInAsync(LogInRequestDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return null;
            }

            var checkPasswordResult = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!checkPasswordResult)
            {
                return null;
            }

            var roles = await _userManager.GetRolesAsync(user);
            if (roles == null)
            {
                return null;
            }

            var jwtToken = CreateJWTToken(user, roles.ToList());

            var response = new LogInResponseDTO
            {
                JwtToken = jwtToken
            };

            return response;
        }

        public string CreateJWTToken(IdentityUser user, List<string> roles)
        {
            // Create claims
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Email, user.Email));

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Key"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

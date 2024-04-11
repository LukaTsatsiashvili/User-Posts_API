using EntityLayer.DTOs.User;

namespace ServiceLayer.Services.Abstract
{
    public interface IUserService
    {
        Task<List<UserListDTO>> GetAllUserAsync();
        Task<UserListDTO> GetUser(Guid id);
        Task<UserDTO> CreateUser(UserCreateDTO model);
        Task<UserDTO> UpdateUser(Guid id, UserUpdateDTO model);
    }
}

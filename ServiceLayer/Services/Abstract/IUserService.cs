using EntityLayer.DTOs.User;

namespace ServiceLayer.Services.Abstract
{
    public interface IUserService
    {
        Task<List<UserListDTO>> GetAllUserAsync();
        Task<UserListDTO> GetSingleUserAsync(Guid id);
        Task<UserDTO> CreateUserAsync(UserCreateDTO model);
        Task<UserDTO> UpdateUserAsync(Guid id, UserUpdateDTO model);
        Task<bool> RemoveUserAsync(Guid id);
    }
}

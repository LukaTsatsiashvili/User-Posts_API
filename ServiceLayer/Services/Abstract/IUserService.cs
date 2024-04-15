using EntityLayer.DTOs.User;

namespace ServiceLayer.Services.Abstract
{
    public interface IUserService
    {
        Task<List<UserListDTO>> GetAllUserAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 100);
        Task<UserListDTO> GetSingleUserAsync(Guid id);
        Task<UserDTO> CreateUserAsync(UserCreateDTO model);
        Task<UserDTO> UpdateUserAsync(Guid id, UserUpdateDTO model);
        Task<bool> RemoveUserAsync(Guid id);
    }
}

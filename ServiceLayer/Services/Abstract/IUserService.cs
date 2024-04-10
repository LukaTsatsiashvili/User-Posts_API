using EntityLayer.DTOs.User;

namespace ServiceLayer.Services.Abstract
{
    public interface IUserService
    {
        Task<List<UserListDTO>> GetAllUserAsync();
    }
}

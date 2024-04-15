using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.DTOs.User;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Services.Abstract;

namespace ServiceLayer.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<User> _repository;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = _unitOfWork.GetGenericRepository<User>();
        }


        public async Task<List<UserListDTO>> GetAllUserAsync()
        {
            var userListDto = await _repository.GetAllEntity().ProjectTo<UserListDTO>
                (_mapper.ConfigurationProvider).ToListAsync();

            return userListDto;
        }

        public async Task<UserListDTO> GetSingleUserAsync(Guid id)
        {

            var user = await _repository
                .Where(x => x.Id == id)
                .Include(x => x.Posts)
                .ProjectTo<UserListDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

            if (user == null || id == Guid.Empty)
            {
                return null;
            }

            return user;
        }

        public async Task<UserDTO> CreateUserAsync(UserCreateDTO model)
        {
            var user = _mapper.Map<User>(model);

            await _repository.AddEntityAsync(user);
            await _unitOfWork.SaveAsync();

            var userDto = _mapper.Map<UserDTO>(user);
            return userDto;
        }

        public async Task<UserDTO> UpdateUserAsync(Guid id, UserUpdateDTO model)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            // Finding user with given id
            var existingUser = await _repository
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (existingUser == null)
            {
                return null;
            }

            // Mapping UserUpdateDTO(model) to User(existingUser) which we already found
            _mapper.Map(model, existingUser);

            // Updateing mapped user
            _repository.UpdateEntity(existingUser);
            await _unitOfWork.SaveAsync();

            // Mapping User to UserDTO 
            var result = _mapper.Map<UserDTO>(existingUser);

            return result;
        }

        public async Task<bool> RemoveUserAsync(Guid id)
        {
            var user = await _repository.GetEntityByIdAsync(id);

            if (user == null || id == Guid.Empty)
            {
                return false;
            }

            _repository.DeleteEntity(user);
            await _unitOfWork.SaveAsync();

            return true;
        }
    }
}

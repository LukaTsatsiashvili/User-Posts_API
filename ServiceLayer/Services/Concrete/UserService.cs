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


        public async Task<List<UserListDTO>> GetAllUserAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAscending = true, int pageNumber = 1, int pageSize = 100)
        {
            var userListDto = await _repository.GetAllEntity().ProjectTo<UserListDTO>
                (_mapper.ConfigurationProvider).ToListAsync();

            // Filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false && string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    userListDto = userListDto.Where(x => x.Name.ToLower().Contains(filterQuery.ToLower())).ToList();
                }

                if (filterOn.Equals("Email", StringComparison.OrdinalIgnoreCase))
                {
                    userListDto = userListDto.Where(x => x.Email.ToLower().Contains(filterQuery.ToLower())).ToList();
                }
            }

            // Sorting
            if (string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    userListDto = isAscending ? userListDto.OrderBy(x => x.Name).ToList() : userListDto.OrderByDescending(x => x.Name).ToList();
                }
            }

            // Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            return userListDto.Skip(skipResults).Take(pageSize).ToList();
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

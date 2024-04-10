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
            var aboutListDto = await _repository.GetAllEntity().ProjectTo<UserListDTO>
                (_mapper.ConfigurationProvider).ToListAsync();

            return aboutListDto;
        }
    }
}

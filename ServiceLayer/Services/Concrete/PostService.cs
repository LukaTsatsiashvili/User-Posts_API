using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.DTOs.Post;
using EntityLayer.DTOs.User;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Services.Abstract;

namespace ServiceLayer.Services.Concrete
{
    public class PostService : IPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Post> _repository;

        public PostService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = _unitOfWork.GetGenericRepository<Post>();
        }


        public async Task<List<PostListDTO>> GetAllPostAsync()
        {
            var postListDto = await _repository.GetAllEntity().ProjectTo<PostListDTO>
                (_mapper.ConfigurationProvider).ToListAsync();

            return postListDto;
        }

        public async Task<PostListDTO> GetSinglePostAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            var post = await _repository
                .Where(x => x.Id == id)
                .Include(x => x.Comments)
                .ProjectTo<PostListDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

            if (post == null)
            {
                return null;
            }

            return post;
        }

        public async Task<PostDTO> CreatePostAsync(PostCreateDTO model)
        {
            var post = _mapper.Map<Post>(model);

            await _repository.AddEntityAsync(post);
            await _unitOfWork.SaveAsync();

            var postDto = _mapper.Map<PostDTO>(post);
            return postDto;
        }

    }
}

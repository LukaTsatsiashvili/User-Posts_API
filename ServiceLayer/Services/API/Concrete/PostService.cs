using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.DTOs.API.Post;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Services.API.Abstract;

namespace ServiceLayer.Services.API.Concrete
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

        public async Task<PostDTO> UpdatePostAsync(Guid id, PostUpdateDTO model)
        {
            if (id == Guid.Empty)
            {
                return null;
            }

            var existingPost = await _repository
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if (existingPost == null)
            {
                return null;
            }

            _mapper.Map(model, existingPost);

            _repository.UpdateEntity(existingPost);
            await _unitOfWork.SaveAsync();

            var result = _mapper.Map<PostDTO>(existingPost);

            return result;
        }

        public async Task<bool> RemovePostAsync(Guid id)
        {
            var post = await _repository.GetEntityByIdAsync(id);
            if (post == null || id == Guid.Empty)
            {
                return false;
            }

            _repository.DeleteEntity(post);
            await _unitOfWork.SaveAsync();

            return true;
        }

    }
}

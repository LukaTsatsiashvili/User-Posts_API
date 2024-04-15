using AutoMapper;
using AutoMapper.QueryableExtensions;
using EntityLayer.DTOs.Comment;
using EntityLayer.Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Repositories.Abstract;
using RepositoryLayer.UnitOfWorks.Abstract;
using ServiceLayer.Services.Abstract;

namespace ServiceLayer.Services.Concrete
{
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Comment> _repository;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _repository = _unitOfWork.GetGenericRepository<Comment>();
        }



        public async Task<List<CommentListDTO>> GetAllCommentAsync()
        {
            var result = await _repository
                .GetAllEntity()
                .ProjectTo<CommentListDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return result;
        }

        public async Task<CommentListDTO> GetSingleCommentAsync(Guid id)
        {
            var comment = await _repository
                .Where(x => x.Id == id)
                .ProjectTo<CommentListDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();

            if (comment == null || id == Guid.Empty)
            {
                return null;
            }

            return comment;
        }

        public async Task<CommentDTO> CreateCommentAsync(CommentCreateDTO model)
        {
            var comment = _mapper.Map<Comment>(model);

            await _repository.AddEntityAsync(comment);
            await _unitOfWork.SaveAsync();

            var commentDto = _mapper.Map<CommentDTO>(comment);

            return commentDto;
        }

        public async Task<bool> RemoveCommentAsync(Guid id)
        {
            var commment = await _repository.GetEntityByIdAsync(id);
            if (commment == null || id == Guid.Empty)
            {
                return false;
            }

            _repository.DeleteEntity(commment);
            await _unitOfWork.SaveAsync();

            return true;
        }
    }
}

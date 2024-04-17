using EntityLayer.DTOs.API.Comment;

namespace ServiceLayer.Services.API.Abstract
{
    public interface ICommentService
    {
        Task<List<CommentListDTO>> GetAllCommentAsync();
        Task<CommentListDTO> GetSingleCommentAsync(Guid id);
        Task<CommentDTO> CreateCommentAsync(CommentCreateDTO model);
        Task<bool> RemoveCommentAsync(Guid id);
    }
}

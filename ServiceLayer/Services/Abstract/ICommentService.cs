using EntityLayer.DTOs.Comment;

namespace ServiceLayer.Services.Abstract
{
    public interface ICommentService
    {
        Task<List<CommentListDTO>> GetAllCommentAsync();
        Task<CommentListDTO> GetSingleCommentAsync(Guid id);
        Task<CommentDTO> CreateCommentAsync(CommentCreateDTO model);
        Task<bool> RemoveCommentAsync(Guid id);
    }
}

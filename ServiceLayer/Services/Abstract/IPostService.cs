using EntityLayer.DTOs.Post;

namespace ServiceLayer.Services.Abstract
{
    public interface IPostService
    {
        Task<List<PostListDTO>> GetAllPostAsync();
        Task<PostListDTO> GetSinglePostAsync(Guid id);
        Task<PostDTO> CreatePostAsync(PostCreateDTO model);
    }
}

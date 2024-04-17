using EntityLayer.DTOs.API.Post;

namespace ServiceLayer.Services.API.Abstract
{
    public interface IPostService
    {
        Task<List<PostListDTO>> GetAllPostAsync();
        Task<PostListDTO> GetSinglePostAsync(Guid id);
        Task<PostDTO> CreatePostAsync(PostCreateDTO model);
        Task<PostDTO> UpdatePostAsync(Guid id, PostUpdateDTO model);
        Task<bool> RemovePostAsync(Guid id);
    }
}

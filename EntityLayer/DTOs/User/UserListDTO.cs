using EntityLayer.DTOs.Comment;
using EntityLayer.DTOs.Post;

namespace EntityLayer.DTOs.User
{
    public class UserListDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public List<PostListDTO> Posts { get; set; }
    }
}

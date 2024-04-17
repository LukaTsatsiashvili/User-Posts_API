using EntityLayer.DTOs.API.Post;

namespace EntityLayer.DTOs.API.User
{
    public class UserListDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public List<PostListDTO> Posts { get; set; }
    }
}

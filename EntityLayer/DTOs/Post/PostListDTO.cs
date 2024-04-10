using EntityLayer.DTOs.Comment;
using EntityLayer.Entities;

namespace EntityLayer.DTOs.Post
{
    public class PostListDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string PublishedAt { get; set; }

        public Guid AuthorId { get; set; }

        public List<CommentListDTO> Comments { get; set; }
    }
}

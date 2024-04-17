namespace EntityLayer.DTOs.API.Comment
{
    public class CommentCreateDTO
    {
        public string Content { get; set; }

        public Guid UserId { get; set; }

        public Guid PostId { get; set; }
    }
}

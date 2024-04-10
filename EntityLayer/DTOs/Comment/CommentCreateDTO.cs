namespace EntityLayer.DTOs.Comment
{
    public class CommentCreateDTO
    {
        public string Content { get; set; }
        public string CreatedTime { get; set; }

        public Guid UserId { get; set; }

        public Guid PostId { get; set; }
    }
}

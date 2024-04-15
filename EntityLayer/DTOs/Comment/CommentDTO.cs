namespace EntityLayer.DTOs.Comment
{
    public class CommentDTO
    {
        public Guid Id { get; set; }
        public string Content { get; set; }
        public string CreatedTime { get; set; }

        public Guid UserId { get; set; }

        public Guid PostId { get; set; }
    }
}

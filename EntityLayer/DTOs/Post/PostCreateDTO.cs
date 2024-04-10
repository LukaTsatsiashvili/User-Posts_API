namespace EntityLayer.DTOs.Post
{
    public class PostCreateDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string PublishedAt { get; set; }

        public Guid AuthorId { get; set; }
    }
}

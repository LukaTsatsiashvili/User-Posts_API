namespace EntityLayer.DTOs.API.Post
{
    public class PostCreateDTO
    {
        public string Title { get; set; }
        public string Content { get; set; }

        public Guid AuthorId { get; set; }
    }
}

namespace EntityLayer.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string PublishedAt { get; set; }

        public Guid AuthorId { get; set; }
        public User Author { get; set; }

        public List<Comment>? Comments { get; set; }
        
    }
}

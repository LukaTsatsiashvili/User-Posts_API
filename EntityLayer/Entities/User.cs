namespace EntityLayer.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public List<Post>? Posts { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}

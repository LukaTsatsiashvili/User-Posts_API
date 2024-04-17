namespace EntityLayer.DTOs.API.Image
{
    public class ImageDTO
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string? FileDescription { get; set; }
    }
}

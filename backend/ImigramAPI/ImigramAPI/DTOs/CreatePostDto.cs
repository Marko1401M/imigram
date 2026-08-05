namespace ImigramAPI.DTOs
{
    public class CreatePostDto
    {
        public string? Content { get; set; }
        public string? Location { get; set; }
        public List<IFormFile> Media { get; set; } = new List<IFormFile>();
    }
}

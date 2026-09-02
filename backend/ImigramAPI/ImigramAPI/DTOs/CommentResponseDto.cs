namespace ImigramAPI.DTOs
{
    public class CommentResponseDto
    {
        public string Id { get; set; }
        public string PostId { get; set; }
        public string UserId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public string FullName { get; set; }
        public string ProfileImage { get; set; }
    }
}

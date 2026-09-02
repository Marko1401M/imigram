namespace ImigramAPI.DTOs
{
    public class AddCommentDto
    {
        public string UserId { get; set; }
        public string PostId { get; set; }
        public string Content { get; set; }
    }
}

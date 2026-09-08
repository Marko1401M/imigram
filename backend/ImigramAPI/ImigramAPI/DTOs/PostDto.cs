using ImigramAPI.Models;

namespace ImigramAPI.DTOs
{
    public class PostDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string ProfileImage { get; set; }
        public string? Content { get; set; }
        public List<PostMedia> Media { get; set; }
        public DateTime CreatedAt { get; set; }
        public int LikesCount { get; set; }
        public bool IsLiked { get; set; }
        public int CommentsCount { get; set; }
        public PostLocation? Location { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}

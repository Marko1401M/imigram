using MongoDB.Bson.Serialization.Attributes;

namespace ImigramAPI.Models
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string? Content { get; set; }
        public List<PostMedia> Media { get; set; } = new List<PostMedia>();
        public DateTime CreatedAt { get; set; }
        public List<string> LikedBy { get; set; } = new List<string>();
        public int CommentsCount { get; set; }
        public PostLocation? Location { get; set; }
        public bool IsDeleted { get; set; }
    }
}

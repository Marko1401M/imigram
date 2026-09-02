using MongoDB.Bson.Serialization.Attributes;

namespace ImigramAPI.Models
{
    public class Notification
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; } // Primalac
        public string SenderId { get; set; }
        public string Type { get; set; } // LIKE / COMMENT / FOLLOW / MESSAGE
        public string Message { get; set; }
        public bool IsRead { get; set; }
        public string? PostId { get; set; }
        public string? CommentId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

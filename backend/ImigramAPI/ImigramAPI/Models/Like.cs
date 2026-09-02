using MongoDB.Bson.Serialization.Attributes;

namespace ImigramAPI.Models
{
    public class Like
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public string PostId { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

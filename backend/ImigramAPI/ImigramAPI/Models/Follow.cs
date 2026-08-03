using MongoDB.Bson.Serialization.Attributes;

namespace ImigramAPI.Models
{
    public class Follow
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string FollowerId { get; set; }
        public string FollowingId { get; set; }
        public DateTime FollowedAt { get; set; }
    }
}

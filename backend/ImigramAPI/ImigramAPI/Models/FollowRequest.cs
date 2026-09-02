using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
namespace ImigramAPI.Models
{
    public class FollowRequest
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string SenderId { get; set; }
        public string RecieverId { get; set; }
        public string Status { get; set; } // Pending / Accepted / Rejected
        public DateTime CreatedAt { get; set; }
        
    }
}

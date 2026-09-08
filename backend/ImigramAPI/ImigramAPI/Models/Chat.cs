using MongoDB.Bson.Serialization.Attributes;

namespace ImigramAPI.Models
{
    public class Chat
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string User1Id { get; set; }
        public string User2Id { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

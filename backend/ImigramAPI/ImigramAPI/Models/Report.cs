using MongoDB.Bson.Serialization.Attributes;

namespace ImigramAPI.Models
{
    public class Report
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string ReporterId { get; set; }
        public string? PostId { get; set; }
        public string? ReportedUserId { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } // Pending, ActionTaken, Ignored
        public DateTime? ResolvedAt { get; set; }
        public string? ResolvedBy { get; set; }

    }
}

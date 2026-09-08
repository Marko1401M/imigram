using MongoDB.Bson.Serialization.Attributes;

namespace ImigramAPI.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public string Role { get; set; } // Admin | User
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
        public string Bio { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsBanned { get; set; } = false;
        public string Country { get; set; }
        
    }
}

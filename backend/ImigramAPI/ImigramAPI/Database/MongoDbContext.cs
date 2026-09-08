using ImigramAPI.Models;
using MongoDB.Driver;

namespace ImigramAPI.Database
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        
        public MongoDbContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoDb:ConnectionString"]);

            _database = client.GetDatabase(configuration["MongoDb:Database"]);
        }

        public IMongoDatabase Database => _database;
        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
        public IMongoCollection<Follow> Follows => _database.GetCollection<Follow>("Follows");
        public IMongoCollection<Post> Posts => _database.GetCollection<Post>("Posts");
        public IMongoCollection<Comment> Comments => _database.GetCollection<Comment>("Comments");
        public IMongoCollection<Like> Likes => _database.GetCollection<Like>("Likes");
        public IMongoCollection<Notification> Notifications => _database.GetCollection<Notification>("Notifications");
        public IMongoCollection<FollowRequest> FollowRequests => _database.GetCollection<FollowRequest>("FollowRequests");
        public IMongoCollection<Chat> Chats => _database.GetCollection<Chat>("Chats");
        public IMongoCollection<Message> Messages => _database.GetCollection<Message>("Messages");
        public IMongoCollection<Report> Reports => _database.GetCollection<Report>("Reports");
    }
}

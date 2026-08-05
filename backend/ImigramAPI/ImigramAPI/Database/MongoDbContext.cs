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
        public IMongoCollection<Follow> Followings => _database.GetCollection<Follow>("Followings");
        public IMongoCollection<Post> Posts => _database.GetCollection<Post>("Posts");
        public IMongoCollection<Comment> Comments => _database.GetCollection<Comment>("Comments");
    }
}

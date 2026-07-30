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
    }
}

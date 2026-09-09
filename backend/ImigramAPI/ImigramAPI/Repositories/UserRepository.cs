using ImigramAPI.Database;
using ImigramAPI.Models;
using ImigramAPI.Repositories.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
namespace ImigramAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly MongoDbContext _context;
        public UserRepository(MongoDbContext context)
        {
            _context = context;
        }
        public async Task Create(User user)
        {
            await _context.Users.InsertOneAsync(user);
        }

        public async Task Delete(string id)
        {
            await _context.Users.DeleteOneAsync(u => u.Id == id);
        }
        public async Task Update(User user)
        {
            await _context.Users.ReplaceOneAsync(u => u.Id == user.Id, user);
        }
        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<User?> GetById(string id)
        {
            return await _context.Users.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User?> GetByUsername(string username)
        {
            return await _context.Users.Find(u => u.Username == username).FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetByBanStatus(bool banned)
        {
            return await _context.Users.Find(u => u.IsBanned == banned).ToListAsync();
        }
        public async Task<List<User>> Search(string query)
        {
            var parts = query
            .Trim()
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);

            var filters = new List<FilterDefinition<User>>();

            foreach (var part in parts)
            {
                filters.Add(
                    Builders<User>.Filter.Or(
                        Builders<User>.Filter.Regex(
                            x => x.Username,
                            new BsonRegularExpression(part, "i")
                        ),
                        Builders<User>.Filter.Regex(
                            x => x.FirstName,
                            new BsonRegularExpression(part, "i")
                        ),
                        Builders<User>.Filter.Regex(
                            x => x.LastName,
                            new BsonRegularExpression(part, "i")
                        )
                    )
                );
            }

            var filter = Builders<User>.Filter.And(filters);

            return await _context.Users
                .Find(filter)
                .Limit(20)
                .ToListAsync();
        }
    }
}

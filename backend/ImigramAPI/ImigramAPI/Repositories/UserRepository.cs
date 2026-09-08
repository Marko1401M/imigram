using ImigramAPI.Database;
using ImigramAPI.Models;
using ImigramAPI.Repositories.Interfaces;
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
    }
}

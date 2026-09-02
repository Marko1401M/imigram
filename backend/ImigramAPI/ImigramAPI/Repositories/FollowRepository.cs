using ImigramAPI.Database;
using ImigramAPI.Models;
using ImigramAPI.Repositories.Interfaces;
using MongoDB.Driver;
namespace ImigramAPI.Repositories
{
    public class FollowRepository : IFollowRepository

    {
        private readonly MongoDbContext _context;
        public FollowRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task Create(Follow follow)
        {
            await _context.Follows.InsertOneAsync(follow);
        }

        public async Task<List<Follow>> GetFollolwers(string userId)
        {
            return await _context.Follows.Find(f => f.FollowingId == userId).ToListAsync();
        }

        public async Task<List<Follow>> GetFollowings(string userId)
        {
            return await _context.Follows.Find(f => f.FollowerId == userId).ToListAsync();
        }

        public async Task Delete(Follow follow)
        {
            //await _context.Follows.ReplaceOneAsync(f => f.Id == follow.Id, follow);
            await _context.Follows.DeleteOneAsync(f => f.Id == follow.Id);
        }

        public async Task<Follow> GetFollow(string followerId, string followingId)
        {
            return await _context.Follows.Find(f => f.FollowerId == followerId && f.FollowingId == followingId).FirstOrDefaultAsync();
        }
    }
}

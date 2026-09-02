using ImigramAPI.Database;
using ImigramAPI.Models;
using ImigramAPI.Repositories.Interfaces;
using MongoDB.Driver;
namespace ImigramAPI.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly MongoDbContext _context;
        public LikeRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task Create(Like like)
        {
            await _context.Likes.InsertOneAsync(like);
        }

        public async Task Delete(string postId, string userId)
        {
            await _context.Likes.DeleteOneAsync(l => l.PostId == postId && l.UserId == userId);
        }

        public async Task<Like> GetLike(string postId, string userId)
        {
            return await _context.Likes.FindAsync(l => l.PostId == postId && l.UserId == userId).Result.FirstOrDefaultAsync();
        }

        public async Task<List<Like>> GetLikes(string postId)
        {
            return await _context.Likes.Find(l => l.PostId == postId).ToListAsync();
        }
    }
}

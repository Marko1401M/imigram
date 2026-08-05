using ImigramAPI.Database;
using ImigramAPI.Models;
using ImigramAPI.Repositories.Interfaces;
using MongoDB.Driver;

namespace ImigramAPI.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly MongoDbContext _context;
        public PostRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task Create(Post post)
        {
            await _context.Posts.InsertOneAsync(post);
        }

        public async Task Delete(string id)
        {
            await _context.Posts.DeleteOneAsync(p => p.Id == id);
        }

        public async Task<List<Post>> GetAll()
        {
            return await _context.Posts.Find(_ => true).ToListAsync();
        }

        public async Task<Post?> GetById(string id)
        {
            return await _context.Posts.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Post>> GetByUserId(string userId)
        {
            return await _context.Posts.Find(p => p.UserId == userId).ToListAsync();
        }

        public async Task Update(Post post)
        {
            await _context.Posts.ReplaceOneAsync(p => p.Id == post.Id, post);
        }
    }
}

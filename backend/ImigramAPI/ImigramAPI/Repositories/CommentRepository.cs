using ImigramAPI.Database;
using ImigramAPI.Models;
using ImigramAPI.Repositories.Interfaces;
using MongoDB.Driver;
namespace ImigramAPI.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly MongoDbContext _context;
        public CommentRepository(MongoDbContext context)
        {
            _context = context;
        }
        public async Task Create(Comment comment)
        {
            await _context.Comments.InsertOneAsync(comment);
        }
        public async Task Delete(string id)
        {
            await _context.Comments.DeleteOneAsync(c => c.Id == id);
        }
        public async Task<Comment?> GetById(string id)
        {
            return await _context.Comments.Find(c => c.Id == id).FirstOrDefaultAsync();
        }
        public async Task<List<Comment>> GetByPostId(string postId)
        {
            return await _context.Comments.Find(c => c.PostId == postId).ToListAsync();
        }
    }
}

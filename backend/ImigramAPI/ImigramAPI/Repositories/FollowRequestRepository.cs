using ImigramAPI.Database;
using ImigramAPI.Models;
using ImigramAPI.Repositories.Interfaces;
using MongoDB.Driver;
namespace ImigramAPI.Repositories
{
    public class FollowRequestRepository : IFollowRequestRepository
    {
        private readonly MongoDbContext _context;
        public FollowRequestRepository(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<FollowRequest> Check(string senderId, string receiverId)
        {
            var request = await _context.FollowRequests.Find(r => r.SenderId == senderId && r.RecieverId == receiverId).FirstOrDefaultAsync();

            return request;
        }

        public async Task<FollowRequest> Create(FollowRequest followRequest)
        {
            await _context.FollowRequests.InsertOneAsync(followRequest);

            return followRequest;
        }

        public async Task<FollowRequest?> GetById(string id)
        {
            var result = await _context.FollowRequests.Find(f => f.Id == id).FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<FollowRequest>> GetByUserId(string userId)
        {
            var result = await _context.FollowRequests.Find(f => f.RecieverId == userId).ToListAsync();

            return result;
        }

        public async Task Update(FollowRequest followRequest)
        {
            await _context.FollowRequests.ReplaceOneAsync(f => f.Id == followRequest.Id, followRequest);
        }
    }
}

using ImigramAPI.Database;
using ImigramAPI.Models;
using ImigramAPI.Repositories.Interfaces;
using MongoDB.Driver;
namespace ImigramAPI.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly MongoDbContext _context;
        public ChatRepository(MongoDbContext context)
        {
            _context = context;
        }
        public async Task<Chat> Create(Chat chat)
        {
            await _context.Chats.InsertOneAsync(chat);

            return chat;
        }

        public async Task<List<Chat>> GetAllChatsForUser(string userId)
        {
            var result = await _context.Chats.Find(c => c.User1Id == userId || c.User2Id == userId).ToListAsync();

            return result;
        }

        public async Task<Chat> GetChat(string chatId)
        {
            var result = await _context.Chats.Find(c => c.Id == chatId).FirstOrDefaultAsync();

            return result;
        }

        public async Task<Chat> GetChatForUsers(string user1Id, string user2Id)
        {
            var result = await _context.Chats.Find(c => (c.User2Id == user2Id && c.User1Id == user1Id) || (c.User1Id == user2Id && c.User2Id == user1Id)).FirstOrDefaultAsync();

            return result;
        }
    }
}

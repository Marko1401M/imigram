using ImigramAPI.Database;
using ImigramAPI.Models;
using ImigramAPI.Repositories.Interfaces;
using MongoDB.Driver;
namespace ImigramAPI.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly MongoDbContext _context;
        public MessageRepository(MongoDbContext context)
        {
            _context = context;
        }
        public async Task<Message> Create(Message message)
        {
            await _context.Messages.InsertOneAsync(message);

            return message;
        }

        public async Task<Message> GetMessage(string id)
        {
            var result = await _context.Messages.Find(m => m.Id == id).FirstOrDefaultAsync();

            return result;
        }

        public async Task<List<Message>> GetMessagesForChat(string chatId)
        {
            var result = await _context.Messages.Find(m => m.ChatId == chatId).ToListAsync();

            return result;
        }

        public async Task<Message> Update(Message message)
        {
            await _context.Messages.ReplaceOneAsync(m => m.Id == message.Id, message);

            return message;
        }
    }
}

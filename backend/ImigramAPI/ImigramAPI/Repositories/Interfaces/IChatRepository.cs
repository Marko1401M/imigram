using ImigramAPI.Models;

namespace ImigramAPI.Repositories.Interfaces
{
    public interface IChatRepository
    {
        Task<Chat> Create(Chat chat);
        Task<List<Chat>> GetAllChatsForUser(string userId);
        Task<Chat> GetChat(string chatId);
        Task<Chat> GetChatForUsers(string user1Id, string user2Id);
    }
}

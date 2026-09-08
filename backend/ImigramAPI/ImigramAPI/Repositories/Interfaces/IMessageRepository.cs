using ImigramAPI.Models;

namespace ImigramAPI.Repositories.Interfaces
{
    public interface IMessageRepository 
    {
        Task<Message> Create(Message message);
        Task<Message> Update(Message message);
        Task<List<Message>> GetMessagesForChat(string chatId);
        Task<Message> GetMessage(string id);
    }
}

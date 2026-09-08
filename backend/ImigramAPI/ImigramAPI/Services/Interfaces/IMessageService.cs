using ImigramAPI.Models;

namespace ImigramAPI.Services.Interfaces
{
    public interface IMessageService
    {
        Task<Message> SendMessage(string chatId, string senderId, string content);
        Task<List<Message>> GetMessages(string chatId);
        Task<Message> GetMessage(string messageId);
        Task<Message> MarkAsRead(string messageId);
    }
}

using ImigramAPI.Models;
using ImigramAPI.Repositories.Interfaces;
using ImigramAPI.Services.Interfaces;

namespace ImigramAPI.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _messageRepository;
        private readonly IChatRepository _chatRepository;
        public MessageService(IMessageRepository messageRepository, IChatRepository chatRepository)
        {
            _messageRepository = messageRepository;
            _chatRepository = chatRepository;
        }

        public async Task<Message> GetMessage(string messageId)
        {
            return await _messageRepository.GetMessage(messageId);
        }

        public async Task<List<Message>> GetMessages(string chatId)
        {
            return await _messageRepository.GetMessagesForChat(chatId);
        }

        public async Task<Message> MarkAsRead(string messageId)
        {
            var message = await _messageRepository.GetMessage(messageId);

            message.IsRead = true;

            await _messageRepository.Update(message);

            return message;
        }

        public async Task<Message> SendMessage(string chatId, string senderId, string content)
        {
            var chat = await _chatRepository.GetChat(chatId);
            if (chat == null) return null;

            var message = new Message
            {
                ChatId = chatId,
                SenderId = senderId,
                Content = content,
                SentAt = DateTime.UtcNow,
                IsRead = false
            };

            return await _messageRepository.Create(message);
        }
    }
}

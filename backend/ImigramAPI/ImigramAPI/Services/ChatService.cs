using ImigramAPI.DTOs;
using ImigramAPI.Models;
using ImigramAPI.Repositories.Interfaces;
using ImigramAPI.Services.Interfaces;

namespace ImigramAPI.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMessageRepository _messageRepository;
        public ChatService(IChatRepository chatRepository, IUserRepository userRepository, IMessageRepository messageRepository) { 
            _chatRepository = chatRepository;
            _userRepository = userRepository;
            _messageRepository = messageRepository;
        }

        public async Task<Chat> CreateChat(string user1Id, string user2Id)
        {
            var existingChat = await _chatRepository.GetChatForUsers(user1Id, user2Id);

            if (existingChat != null) return existingChat;

            var chat = new Chat
            {
                User1Id = user1Id,
                User2Id = user2Id,
                CreatedAt = DateTime.UtcNow,
            };

            return await _chatRepository.Create(chat);
        }

        public async Task<List<ChatResponseDto>> GetAllChatsForUser(string userId)
        {
            var chats = await _chatRepository.GetAllChatsForUser(userId);
            List<ChatResponseDto> result = new List<ChatResponseDto>();
            foreach(var chat in chats)
            {
                var uId = chat.User1Id != userId ? chat.User1Id : chat.User2Id;
                var user = await _userRepository.GetById(uId);
                var messages = await _messageRepository.GetMessagesForChat(chat.Id);
                var lastMessage = messages.Count != 0 ? messages.Last() : null;

                var temp = new ChatResponseDto
                {
                    Id = chat.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserId = uId,
                    Username = user.Username,
                    ProfileImage = user.ProfileImage,
                    LastMessage = lastMessage != null ? lastMessage.Content : " ",
                    LastMessageDate = lastMessage != null ? lastMessage.SentAt : DateTime.UtcNow,
                    UnreadCount = 0, 
                };

                result.Add(temp);
            }
            return result;
        }

        public async Task<ChatResponseDto> GetChat(string chatId, string currentUserId)
        {
            var chat = await _chatRepository.GetChat(chatId);
            var uId = chat.User1Id != currentUserId ? chat.User1Id : chat.User2Id;
            var user = await _userRepository.GetById(uId);
            var messages = await _messageRepository.GetMessagesForChat(chat.Id);
            var lastMessage = messages != null ? messages.Last() : null;

            var result = new ChatResponseDto
            {
                Id = chat.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserId = uId,
                Username = user.Username,
                ProfileImage = user.ProfileImage,
                LastMessage = lastMessage != null ? lastMessage.Content : " ",
                LastMessageDate = lastMessage != null ? lastMessage.SentAt : DateTime.UtcNow,
                UnreadCount = 0,
            };

            return result;
        }

        public async Task<ChatResponseDto> GetChatForUsers(string user1Id, string user2Id)
        {//user2Id je current user
            var chat =  await _chatRepository.GetChatForUsers(user1Id, user2Id);
            if(chat != null)
            {
                var uId = chat.User1Id != user2Id ? chat.User1Id : chat.User2Id;
                var user = await _userRepository.GetById(uId);
                var messages = await _messageRepository.GetMessagesForChat(chat.Id);
                var lastMessage = messages != null ? messages.Last() : null;

                var result = new ChatResponseDto
                {
                    Id = chat.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserId = uId,
                    Username = user.Username,
                    ProfileImage = user.ProfileImage,
                    LastMessage = lastMessage != null ? lastMessage.Content : " ",
                    LastMessageDate = lastMessage != null ? lastMessage.SentAt : DateTime.UtcNow,
                    UnreadCount = 0,
                };
                return result;
            }

            else
            {
                var new_ = await CreateChat(user1Id, user2Id);

                var uId = new_.User1Id != user2Id ? new_.User1Id : new_.User2Id;
                var user = await _userRepository.GetById(uId);
                var messages = await _messageRepository.GetMessagesForChat(new_.Id);
                var lastMessage = messages != null ? messages.Last() : null;

                var result = new ChatResponseDto
                {
                    Id = new_.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    UserId = uId,
                    Username = user.Username,
                    ProfileImage = user.ProfileImage,
                    LastMessage = lastMessage != null ? lastMessage.Content : " ",
                    LastMessageDate = lastMessage != null ? lastMessage.SentAt : DateTime.UtcNow,
                    UnreadCount = 0,
                };

                return result;
            };
        }
    }
}

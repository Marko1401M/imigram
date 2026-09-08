using ImigramAPI.DTOs;
using ImigramAPI.Models;

namespace ImigramAPI.Services.Interfaces
{
    public interface IChatService
    {
        Task<Chat> CreateChat(string user1Id, string user2Id);
        Task<List<ChatResponseDto>> GetAllChatsForUser(string userId);
        Task<ChatResponseDto> GetChat(string chatId, string currentUserId);
        Task<ChatResponseDto> GetChatForUsers(string user1Id, string user2Id);
        
    }
}

using ImigramAPI.Models;

namespace ImigramAPI.Services.Interfaces
{
    public interface INotificationService 
    {
        Task<List<Notification>> GetByUserId(string userId);
        Task Create(string userId, string senderId, string type, string message, string? postId = null, string? commentId = null);
        Task MarkAsRead(string notificationId);
    }
}

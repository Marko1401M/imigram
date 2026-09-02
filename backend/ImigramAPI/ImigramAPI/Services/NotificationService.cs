using ImigramAPI.Hubs;
using ImigramAPI.Models;
using ImigramAPI.Repositories.Interfaces;
using ImigramAPI.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace ImigramAPI.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IHubContext<NotificationHub> _hubContext;
        public NotificationService(INotificationRepository notificationRepository, IHubContext<NotificationHub> hubContext)
        {
            _notificationRepository = notificationRepository;
            _hubContext = hubContext;
        }

        public async Task Create(string userId, string senderId, string type, string message, string? postId = null, string? commentId = null)
        {
            var notification = new Notification
            {
                UserId = userId,
                SenderId = senderId,
                Type = type,
                Message = message,
                PostId = postId,
                CommentId = commentId,
                IsRead = false,
                CreatedAt = DateTime.UtcNow,
            };

            await _notificationRepository.Create(notification);

            await _hubContext.Clients
                .User(userId)
                .SendAsync("ReceiveNotification", notification);

            Console.WriteLine("Notification sent!");
        }

        public async Task<List<Notification>> GetByUserId(string userId)
        {
            return await _notificationRepository.GetByUserId(userId);
        }

        public async Task MarkAsRead(string notificationId)
        {
            
            var notification = await _notificationRepository.GetNotification(notificationId);
            notification.IsRead = true;
            await _notificationRepository.Update(notification);
        }
    }
}

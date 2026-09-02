using ImigramAPI.Models;

namespace ImigramAPI.Repositories.Interfaces
{
    public interface INotificationRepository
    {
        Task<List<Notification?>> GetByUserId(string userId);
        Task Create(Notification notification);
        Task Update(Notification notification);
        Task<Notification> GetNotification(string notificationId);
    }
}

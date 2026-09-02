using ImigramAPI.Database;
using ImigramAPI.Models;
using ImigramAPI.Repositories.Interfaces;
using MongoDB.Driver;

namespace ImigramAPI.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly MongoDbContext _context;
        public NotificationRepository(MongoDbContext context)
        {
            _context = context;
        }
        public async Task Create(Notification notification)
        {
            await _context.Notifications.InsertOneAsync(notification);
        }

        public async Task<List<Notification?>> GetByUserId(string userId)
        {
            var result = await _context.Notifications.Find(n => n.UserId == userId).ToListAsync();

            return result;
        }

        public async Task<Notification> GetNotification(string notificationId)
        {
            var result = await _context.Notifications.Find(n => n.Id == notificationId).FirstOrDefaultAsync();

            return result;
        }

        public async Task Update(Notification notification)
        {
            await _context.Notifications.ReplaceOneAsync(n => n.Id == notification.Id, notification);
        }
    }
}

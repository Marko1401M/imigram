using ImigramAPI.Services;
using ImigramAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ImigramAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotificationController : ControllerBase
    {
        private INotificationService _notificationService;
        public NotificationController(INotificationService notificationService) { 
            _notificationService = notificationService;
        }
        [HttpGet()]
        public async Task<IActionResult> GetNotificationsForUser()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Ok();
            var result = await _notificationService.GetByUserId(userId);

            return Ok(result);
        }
        [HttpPost("read/{notificationId}")]
        public async Task<IActionResult> MarkAsRead(string notificationId)
        {
            await _notificationService.MarkAsRead(notificationId);

            return Ok();
        }
    }
}

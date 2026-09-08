using ImigramAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImigramAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;
        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }
        [HttpGet("{chatId}")]
        public async Task<IActionResult> GetMessages(string chatId)
        {
            var result = await _messageService.GetMessages(chatId);

            return Ok(result);
        }
        [HttpPut("{messageId}/read")]
        public async Task<IActionResult> MarkAsRead(string messageId)
        {
            var message = await _messageService.MarkAsRead(messageId);

            return Ok(message);
        }
    }
}

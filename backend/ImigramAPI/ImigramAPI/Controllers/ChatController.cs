using ImigramAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ImigramAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllChats()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _chatService.GetAllChatsForUser(userId);

            return Ok(result);
        }
        [HttpGet("{chatId}")]
        public async Task<IActionResult> GetChat(string chatId)
        {
            var user1Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _chatService.GetChat(chatId, user1Id);

            return Ok(result);
        }
        [HttpGet("with/{userId}")]
        public async Task<IActionResult> GetChatWithUser(string userId) 
        {
            var user1Id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _chatService.GetChatForUsers(userId, user1Id);

            return Ok(result);

        }
    }
}

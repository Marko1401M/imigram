using ImigramAPI.DTOs;
using ImigramAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ImigramAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService) {
            _userService = userService;
        }
        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(string userId)
        {
            var result = await _userService.GetUserById(userId);

            return Ok(result);
        }
        [HttpPut("update/profile-image")]
        public async Task<IActionResult> UpdateProfileImage([FromForm] UpdateProfileImageDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _userService.ChangeProfileImage(userId, dto.Image);
            return Ok(result);
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchUsers([FromQuery] string query)
        {
            var users = await _userService.SearchUsers(query);

            return Ok(users);
        }
    }
}

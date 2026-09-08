using ImigramAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImigramAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "admin")]
    public class AdminController : ControllerBase
    {
        private IAdminService _adminService;
        private IUserService _userService;
        public AdminController(IAdminService adminService, IUserService userService) { 
            _adminService = adminService;
            _userService = userService;
        }
        [HttpPost("ban/{userId}")]
        public async Task<IActionResult> BanUser(string userId)
        {
            await _adminService.BanUser(userId);

            return Ok();
        }
        [HttpPost("unban/{userId}")]
        public async Task<IActionResult> UnbanUser(string userId)
        {
            await _adminService.UnbanUser(userId);

            return Ok();
        }
        [HttpDelete("{postId}")]
        public async Task<IActionResult> DeletePost(string postId)
        {
            await _adminService.DeletePost(postId);

            return Ok();
        }
        [HttpGet("")]
        public async Task<IActionResult> GetBannedUsers()
        {
            var res = await _userService.GetBannedUsers();

            return Ok(res);
        }
    }
}

using ImigramAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    }
}

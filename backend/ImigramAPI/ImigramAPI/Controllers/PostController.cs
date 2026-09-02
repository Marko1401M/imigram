using ImigramAPI.DTOs;
using ImigramAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ImigramAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _postService.CreatePost(dto, userId);

            return Ok(result);
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(string id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await  _postService.GetPost(id, userId);
            return Ok(result);
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _postService.GetPosts();

            return Ok(result);
        }
        [Authorize]
        [HttpGet("all/{userId}")]
        public async Task<IActionResult> GetAllPostForUser(string userId) {
            var result = await _postService.GetAllPostsForUser(userId);

            return Ok(result);
        }
        
    }
}

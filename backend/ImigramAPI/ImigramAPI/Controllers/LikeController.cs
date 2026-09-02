using ImigramAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using ImigramAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
namespace ImigramAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;
        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }
        [HttpPost]
        public async Task<IActionResult> Like([FromForm] AddLikeDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var res = await _likeService.LikePost(dto.PostId, userId);
            return Ok(res);
        }
        [HttpDelete("{postId}")]
        public async Task<IActionResult> RemoveLike(string postId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = await _likeService.UnlikePost(postId, userId);

            return Ok(result);
        }
        [HttpGet("{postId}")]
        public async Task<IActionResult> GetLikesForPost(string postId)
        {
            var result = _likeService.GetLikes(postId);

            return Ok(result);
        }
        [HttpGet("check-like/{postId}")]
        public async Task<IActionResult> CheckLike(string postId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = _likeService.IsLiked(postId, userId);

            return Ok(result);
        }
    }
}

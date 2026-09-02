using ImigramAPI.DTOs;
using ImigramAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ImigramAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddComment([FromForm] AddCommentDto dto)
        {
            dto.UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var comment = await _commentService.AddComment(dto);

            return Ok(comment);
        }
        [Authorize]
        [HttpGet("{commentId}")]
        public async Task<IActionResult> GetComment(string commentId)
        {
            var comment = await _commentService.GetComment(commentId);

            return Ok(comment);
        }
        [Authorize]
        [HttpGet("all/{postId}")]
        public async Task<IActionResult> GetAllComments(string postId)
        {
            var comments = await _commentService.GetComments(postId);

            return Ok(comments);
        }

    }
}

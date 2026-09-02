using ImigramAPI.DTOs;
using ImigramAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ImigramAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FollowController : ControllerBase
    {
        private readonly IFollowRequestService _followRequestService;
        private readonly IFollowService _followService;
        public FollowController(IFollowRequestService followRequestService, IFollowService followService) { 
            _followRequestService = followRequestService;
            _followService = followService;
        }
        [Authorize]
        [HttpPost("send_request")]
        public async Task<IActionResult> SendFollowRequest([FromBody] FollowDto dto)
        {
            var senderId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _followRequestService.Create(senderId, dto.RecieverId);
            return Ok(result);
        }
        [Authorize]
        [HttpPost("accept_request/{requestId}")]
        public async Task<IActionResult> AcceptFollowRequest(string requestId)
        {
            await _followRequestService.AcceptRequest(requestId);

            return Ok();
        }
        [Authorize]
        [HttpPost("decline_request/{requestId}")]
        public async Task<IActionResult> DeclineFollowRequest(string requestId) { 
            await _followRequestService.DeclineRequest(requestId);

            return Ok();
        }
        [Authorize]
        [HttpDelete("remove_following")]
        public async Task<IActionResult> RemoveFollowing([FromBody] RemoveFollowDto dto) {

            var senderId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await _followService.Unfollow(senderId, dto.FollowingId);

            return Ok();
        }
        [Authorize]
        [HttpGet("followings/{userId}")]
        public async Task<IActionResult> GetFollowings(string userId) { 
            var result = await _followService.GetFollowings(userId);

            return Ok(result);
        }
        [Authorize]
        [HttpGet("followers/{userId}")]
        public async Task<IActionResult> GetFollowers(string userId)
        {
            var result = await _followService.GetFollowers(userId);

            return Ok(result);
        }
        [Authorize]
        [HttpGet("follow_request/{userId}")]
        public async Task<IActionResult> GetFollowRequests(string userId) { 
            var result = await _followRequestService.GetRequestsForUser(userId);
           
            return Ok(result);
        }
        [Authorize]
        [HttpGet("follow_status/{userId}")]
        public async Task<IActionResult> GetFollowStatus(string userId)
        {
            var receiverId = userId;
            var senderId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            var result = await _followRequestService.Check(senderId, receiverId);

            return Ok(result);
        }
        
    }
}

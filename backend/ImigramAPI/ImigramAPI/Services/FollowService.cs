using ImigramAPI.DTOs;
using ImigramAPI.Models;
using ImigramAPI.Repositories.Interfaces;
using ImigramAPI.Services.Interfaces;

namespace ImigramAPI.Services
{
    public class FollowService : IFollowService
    {
        private readonly IFollowRepository _followRepository;
        private readonly IUserService _userService;
        public FollowService(IFollowRepository followRepository, IUserService userService)
        {
            _followRepository = followRepository;
            _userService = userService;
        }
        public async Task Follow(string followerId, string followingId)
        {
            var follow = new Follow
            {
                FollowerId = followerId,
                FollowingId = followingId,
                CreatedAt = DateTime.UtcNow
            };
            await _followRepository.Create(follow);
        }

        public async Task<List<UserDto>> GetFollowers(string userId)
        {
            var follows = await _followRepository.GetFollolwers(userId);
            List<UserDto> result = new List<UserDto>();
            foreach (var follow in follows) {
                var user = await _userService.GetUserById(follow.FollowerId);
                var dto = new UserDto
                {
                    Bio = user.Bio,
                    Id = user.Id,
                    CreatedAt = user.CreatedAt,
                    FirstName = user.FirstName,
                    Email = user.Email,
                    LastName = user.LastName,
                    ProfileImage = user.ProfileImage,
                    Username = user.Username,
                };
                result.Add(dto);
            }
            return result;
            
        }

        public async Task<List<UserDto>> GetFollowings(string userId)
        {
            var follows = await _followRepository.GetFollowings(userId);
            List<UserDto> result = new List<UserDto>();
            foreach (var follow in follows)
            {
                var user = await _userService.GetUserById(follow.FollowingId);
                var dto = new UserDto
                {
                    Bio = user.Bio,
                    Id = user.Id,
                    CreatedAt = user.CreatedAt,
                    FirstName = user.FirstName,
                    Email = user.Email,
                    LastName = user.LastName,
                    ProfileImage = user.ProfileImage,
                    Username = user.Username,
                };
                result.Add(dto);
            }
            return result;
        }

        public async Task<bool> IsFollowing(string followerId, string followingId)
        {
            var result = await _followRepository.GetFollow(followerId, followingId);
            return result != null;
        }

        public async Task Unfollow(string followerId, string followingId)
        {
            var follow = await _followRepository.GetFollow(followerId, followingId);
            await _followRepository.Delete(follow);
        }
    }
}

using ImigramAPI.DTOs;
using ImigramAPI.Models;

namespace ImigramAPI.Services.Interfaces
{
    public interface IFollowService
    {
        Task Follow(string followerId, string followingId);
        Task Unfollow(string followerId, string followingId);
        Task<List<UserDto>> GetFollowers(string userId);
        Task<List<UserDto>> GetFollowings(string userId);
        Task<bool> IsFollowing(string followerId, string followingId);
    }
}

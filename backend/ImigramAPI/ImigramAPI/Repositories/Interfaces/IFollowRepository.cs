using ImigramAPI.DTOs;
using ImigramAPI.Models;

namespace ImigramAPI.Repositories.Interfaces
{
    public interface IFollowRepository
    {
        Task Create(Follow follow);
        Task Delete(Follow follow);
        Task<Follow> GetFollow(string followerId, string followingId);
        Task<List<Follow>> GetFollolwers(string userId);
        Task<List<Follow>> GetFollowings(string userId);
    }
}

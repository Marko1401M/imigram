using ImigramAPI.Models;

namespace ImigramAPI.Repositories.Interfaces
{
    public interface IFollowRequestRepository
    {
        Task<List<FollowRequest>> GetByUserId(string userId);
        Task<FollowRequest?> GetById(string id);
        Task<FollowRequest> Create(FollowRequest followRequest);
        Task<FollowRequest> Check(string senderId, string receiverId);
        Task Update(FollowRequest followRequest);
    }
}

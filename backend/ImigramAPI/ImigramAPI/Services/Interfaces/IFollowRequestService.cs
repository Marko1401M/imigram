using ImigramAPI.DTOs;
using ImigramAPI.Models;

namespace ImigramAPI.Services.Interfaces
{
    public interface IFollowRequestService
    {
        Task<FollowRequest> Create(string senderId, string recieverId);
        Task<List<FollowRequestDto>> GetRequestsForUser(string userId);
        Task<string> Check(string senderId, string receiverId);
        Task AcceptRequest(string requestId);
        Task DeclineRequest(string requestId);
    }
}

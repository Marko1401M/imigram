using ImigramAPI.DTOs;
using ImigramAPI.Models;
using ImigramAPI.Repositories.Interfaces;
using ImigramAPI.Services.Interfaces;

namespace ImigramAPI.Services
{
    public class FollowRequestService : IFollowRequestService
    {
        private readonly INotificationService _notificationService;
        private readonly IFollowRequestRepository _followRequestRepository;
        private readonly IFollowService _followService;
        private readonly IUserService _userService;
        public FollowRequestService(INotificationService notificationService, IFollowRequestRepository followRequestRepository, IUserService userService, IFollowService followService)
        {
            _notificationService = notificationService;
            _followRequestRepository = followRequestRepository;
            _userService = userService;
            _followService = followService;
        }

        public async Task AcceptRequest(string requestId)
        {
            var request = await _followRequestRepository.GetById(requestId);

            if (request == null) return;

            request.Status = "Accepted";

            await _followService.Follow(request.SenderId, request.RecieverId);

            await _followRequestRepository.Update(request);

            var user = await _userService.GetUserById(request.RecieverId);

            await _notificationService.Create(request.SenderId, request.RecieverId, "Follow", $"Korisnik {user.Username} je prihvatio Vaš zahtev za praćenje!");
        }

        public async Task<string> Check(string senderId, string receiverId)
        {
            var result = await _followRequestRepository.Check(senderId, receiverId);

            if (result == null || result.Status == "Declined") return "None";

            else return result.Status;
        }

        public async Task<FollowRequest> Create(string senderId, string recieverId)
        {
            var request = new FollowRequest
            {
                SenderId = senderId,
                RecieverId = recieverId,
                Status = "Pending",
                CreatedAt = DateTime.UtcNow,
            };
            await _followRequestRepository.Create(request);

            var user = await _userService.GetUserById(request.SenderId);

            await _notificationService.Create(request.RecieverId, request.SenderId, "FollowReq", $"Korisnik {user.Username} Vam je poslao zahtev za praćenje!", request.RecieverId);

            return request;
        }

        public async Task DeclineRequest(string requestId)
        {
            var request = await _followRequestRepository.GetById(requestId);

            if(request == null) return;

            request.Status = "Declined";

            await _followRequestRepository.Update(request);

            
        }

        public async Task<List<FollowRequestDto>> GetRequestsForUser(string userId)
        {
            var requests = await _followRequestRepository.GetByUserId(userId);
            List<FollowRequestDto> result = new List<FollowRequestDto>();
            foreach(var req in requests)
            {
                var user = await _userService.GetUserById(req.SenderId);
                var temp = new FollowRequestDto
                {
                    Id = req.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Username = user.LastName,
                    CreatedAt = req.CreatedAt,
                    ProfileImage = user.ProfileImage,
                    RecieverId = req.RecieverId,
                    SenderId = req.SenderId,
                    Status = req.Status,
                };
                if(temp.Status == "Pending") result.Add(temp);

            }
            return result;
        }
    }
}

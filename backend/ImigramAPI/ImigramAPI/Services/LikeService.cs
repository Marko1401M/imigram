using ImigramAPI.Models;
using ImigramAPI.Repositories.Interfaces;
using ImigramAPI.Services.Interfaces;
using MongoDB.Driver;

namespace ImigramAPI.Services
{
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;
        private readonly IPostRepository _postRepository;
        private readonly INotificationService _notificationService;
        private readonly IUserRepository _userRepository;
        public LikeService(ILikeRepository likeRepository, IPostRepository postRepository, INotificationService notificationService, IUserRepository userRepository)
        {
            _likeRepository = likeRepository; 
            _postRepository = postRepository;
            _notificationService = notificationService;
            _userRepository = userRepository;
        }
        public async Task<List<Like>> GetLikes(string postId)
        {
            return await _likeRepository.GetLikes(postId);
        }

        public async Task<bool> IsLiked(string postId, string userId)
        {
            var result = await _likeRepository.GetLike(postId, userId);

            return result != null;
        }

        public async Task<Like> LikePost(string postId, string userId)
        {

            var newLike = new Like
            {
                PostId = postId,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
            };

            await _likeRepository.Create(newLike);

            var post = await _postRepository.GetById(postId);

            if (!post.LikedBy.Contains(userId))
            {
                post.LikedBy.Add(userId);
            }

            await _postRepository.Update(post);
            var user = await _userRepository.GetById(userId);
            await _notificationService.Create(post.UserId, userId, "LIKE", $"{user.FirstName} je lajkovao Vašu objavu!", postId);

            return newLike;
        }

        public async Task<Like> UnlikePost(string postId, string userId)
        {
            await _likeRepository.Delete(postId, userId);

            var post = await _postRepository.GetById(postId);

            post.LikedBy.Remove(userId);

            await _postRepository.Update(post);

            return null;
        }
    }
}

using ImigramAPI.Models;

namespace ImigramAPI.Services.Interfaces
{
    public interface ILikeService
    {
        Task<Like> LikePost(string postId, string userId);
        Task<Like> UnlikePost(string postId, string userId);
        Task<bool> IsLiked(string postId, string userId);
        Task<List<Like>> GetLikes(string postId);
    }
}

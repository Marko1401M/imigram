using ImigramAPI.Models;

namespace ImigramAPI.Repositories.Interfaces
{
    public interface ILikeRepository
    {
        Task Create(Like like);
        Task Delete(string postId, string userId);
        Task<Like> GetLike(string postId, string userId);
        Task<List<Like>> GetLikes(string postId);
    }
}

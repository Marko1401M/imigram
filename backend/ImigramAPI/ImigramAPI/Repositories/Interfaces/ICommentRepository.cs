using ImigramAPI.Models;

namespace ImigramAPI.Repositories.Interfaces
{
    public interface ICommentRepository
    {
        Task<List<Comment>> GetByPostId(string postId);
        Task<Comment?> GetById(string id);
        Task Create(Comment comment);
        Task Delete(string id);
    }
}

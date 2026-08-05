using ImigramAPI.Models;

namespace ImigramAPI.Repositories.Interfaces
{
    public interface IPostRepository
    {
        Task<Post?> GetById(string id);
        Task<List<Post>> GetAll();
        Task Create(Post post);
        Task Update(Post post);
        Task Delete(string id);
        Task<List<Post>> GetByUserId(string userId);
    }
}

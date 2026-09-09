using ImigramAPI.Models;

namespace ImigramAPI.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmail(string email);
        Task<User?> GetByUsername(string username);
        Task<User?> GetById(string id);
        Task Create(User user);
        Task Update(User user);
        Task Delete(string id);
        Task<List<User>> GetByBanStatus(bool banned);
        Task<List<User>> Search(string query);
    }
}

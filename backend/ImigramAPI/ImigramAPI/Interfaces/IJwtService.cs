using ImigramAPI.Models;

namespace ImigramAPI.Interfaces
{
    public interface IJwtService
    {
        public string GenerateToken(User user);
    }
}

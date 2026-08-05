using ImigramAPI.Models;

namespace ImigramAPI.Services.Interfaces
{
    public interface IJwtService
    {
        public string GenerateToken(User user);
    }
}

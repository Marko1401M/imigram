using ImigramAPI.DTOs;

namespace ImigramAPI.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserDto> GetUserById(string userId);
    }
}

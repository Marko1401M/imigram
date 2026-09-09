using ImigramAPI.DTOs;

namespace ImigramAPI.Services.Interfaces
{
    public interface IUserService
    {
        public Task<UserDto> GetUserById(string userId);
        public Task<List<UserDto>> GetBannedUsers();
        public Task<UserDto> ChangeProfileImage(string userId, IFormFile image);
        public Task<List<UserSearchDto>> SearchUsers(string query);
    }
}

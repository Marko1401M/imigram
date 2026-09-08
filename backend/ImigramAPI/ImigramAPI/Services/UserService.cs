using ImigramAPI.DTOs;
using ImigramAPI.Repositories.Interfaces;
using ImigramAPI.Services.Interfaces;

namespace ImigramAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<UserDto> GetUserById(string userId)
        {
            var user = await _userRepository.GetById(userId);
            var userDto = new UserDto
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Bio = user.Bio,
                Username = user.Username,
                CreatedAt = user.CreatedAt,
                Id = user.Id,
                Email = user.Email,
                ProfileImage = user.ProfileImage,
                IsBanned = user.IsBanned,
            };
            return userDto;
        }
        public async Task<List<UserDto>> GetBannedUsers()
        {
            var users = await _userRepository.GetByBanStatus(true);
            List<UserDto> list = new List<UserDto>();
            foreach(var user in users)
            {
                var userDto = new UserDto
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Bio = user.Bio,
                    Username = user.Username,
                    CreatedAt = user.CreatedAt,
                    Id = user.Id,
                    Email = user.Email,
                    ProfileImage = user.ProfileImage,
                    IsBanned = user.IsBanned
                };
                list.Add(userDto);
            }
            return list;
        }
    }
}

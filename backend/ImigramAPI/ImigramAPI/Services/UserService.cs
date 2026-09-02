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
                ProfileImage = user.ProfileImage
            };
            return userDto;
        }
    }
}

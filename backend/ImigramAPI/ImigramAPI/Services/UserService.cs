using ImigramAPI.DTOs;
using ImigramAPI.Models;
using ImigramAPI.Repositories.Interfaces;
using ImigramAPI.Services.Interfaces;
using MongoDB.Bson;

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

        public async Task<UserDto> ChangeProfileImage(string userId, IFormFile image)
        {
            var user = await _userRepository.GetById(userId);

            if (user == null) return null;

            if (image == null || image.Length == 0) return null;

            if (!image.ContentType.StartsWith("image/")) return null;

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/users");

            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

            var filePath = Path.Combine(folderPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            user.ProfileImage = "/images/users/" + fileName;

            await _userRepository.Update(user);

            return await GetUserById(userId);
        }

        public async Task<List<UserSearchDto>> SearchUsers(string query)
        {
            var users = await _userRepository.Search(query);

            return users.Select(u => new UserSearchDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                ProfileImage = u.ProfileImage,
                Username = u.Username
            }).ToList();
        }
    }
}

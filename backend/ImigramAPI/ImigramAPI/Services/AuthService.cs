using ImigramAPI.DTOs;
using ImigramAPI.Interfaces;
using ImigramAPI.Models;
using ImigramAPI.Repositories;
using MongoDB.Bson;

namespace ImigramAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtService _jwtService;
        public AuthService(IUserRepository userRepository, IJwtService jwtService)
        {
            _userRepository = userRepository;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseDto> Login(LoginDto loginDto)
        {
            var user = await _userRepository.GetByUsername(loginDto.Username);

            if (user == null) return null;

            bool validPassword = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);

            if (!validPassword) return null;

            var token = _jwtService.GenerateToken(user);
           
            return new LoginResponseDto
            {
                Token = token,
                Username = user.Username
            };
        }

        public async Task<bool> Register(RegisterDto registerDto)
        {
            var userExists = await _userRepository.GetByEmail(registerDto.Email);

            if (userExists != null)
                return false;

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/users");

            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(registerDto.ProfileImage.FileName);

            var filePath = Path.Combine(folderPath, fileName);

            using(var stream = new FileStream(filePath, FileMode.Create))
            {
                await registerDto.ProfileImage.CopyToAsync(stream);
            }

            var user = new User
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Username = registerDto.Username,
                Email = registerDto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                CreatedAt = DateTime.UtcNow,
                ProfileImage = "/images/users/" + fileName,
                Role="User",
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName
            };
            _userRepository.Create(user);
            return true;
        }
    }
}

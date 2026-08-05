using ImigramAPI.DTOs;

namespace ImigramAPI.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<bool> Register(RegisterDto registerDto);
        public Task<LoginResponseDto> Login(LoginDto loginDto);
    }
}

using ImigramAPI.DTOs;

namespace ImigramAPI.Services.Interfaces
{
    public interface IAuthService
    {
        public Task<string> Register(RegisterDto registerDto);
        public Task<LoginResponseDto> Login(LoginDto loginDto);
    }
}

namespace ImigramAPI.DTOs
{
    public class RegisterDto
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public IFormFile? ProfileImage { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

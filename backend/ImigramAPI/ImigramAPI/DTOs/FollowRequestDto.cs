namespace ImigramAPI.DTOs
{
    public class FollowRequestDto
    {
        public string Id { get; set; }
        public string SenderId { get; set; }
        public string RecieverId { get; set; }
        public string Status { get; set; } // Pending / Accepted / Rejected
        public DateTime CreatedAt { get; set; }
        public string Username { get; set; }
        public string ProfileImage { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}

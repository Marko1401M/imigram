namespace ImigramAPI.DTOs
{
    public class ReportResponseDto
    {
        public string Id { get; set; }
        public UserDto Reporter { get; set; }
        public PostDto Post { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } // Pending, ActionTaken, Ignored
        public DateTime? ResolvedAt { get; set; }
        public UserDto ResolvedBy { get; set; }
    }
}

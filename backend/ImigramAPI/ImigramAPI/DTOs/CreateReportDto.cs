namespace ImigramAPI.DTOs
{
    public class CreateReportDto
    {
        public string ReporterId { get; set; }
        public string? PostId { get; set; }
        public string? ReportedUserId { get; set; }
        public string Reason { get; set; }
        public string? Description { get; set; }
    }
}

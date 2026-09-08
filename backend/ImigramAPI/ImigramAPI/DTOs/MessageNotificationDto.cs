namespace ImigramAPI.DTOs
{
    public class MessageNotificationDto
    {
        public string Id { get; set; }
        public string ChatId { get; set; }
        public string SenderId { get; set; }
        public string SenderUsername { get; set; }
        public string SenderFullName { get; set; }
        public string SenderProfileImage { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
    }
}

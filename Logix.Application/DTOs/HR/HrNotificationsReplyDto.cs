namespace Logix.Application.DTOs.HR
{
    public class HrNotificationsReplyDto
    {
        public long Id { get; set; }
        public string? Reply { get; set; }
        public long? NotificationId { get; set; }
  
        public string? Source { get; set; }
    }
    public class HrNotificationsReplyEditDto
    {
        public long Id { get; set; }
        public string? Reply { get; set; }
        public long? NotificationId { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

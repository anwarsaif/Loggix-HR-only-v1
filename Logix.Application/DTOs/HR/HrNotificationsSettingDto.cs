namespace Logix.Application.DTOs.HR
{
    public class HrNotificationsSettingDto
    {

        public long? Id { get; set; }
        public string? NotificationDate { get; set; }
        public int? NationalityId { get; set; }
        public string? Subject { get; set; }
        public string? Detailes { get; set; }
        public string? AttachFile { get; set; }
        public bool? IsActive { get; set; }
        public int? IsActiveInt { get; set; }
        public bool? IsDeleted { get; set; }
        public long? FacilityId { get; set; }
    }

    public class HrNotificationsSettingEditDto
    {
        public long Id { get; set; }
        public string? NotificationDate { get; set; }
        public int? NationalityId { get; set; }
        public string? Subject { get; set; }
        public string? Detailes { get; set; }
        public string? AttachFile { get; set; }
        public bool? IsActive { get; set; }
        public int? IsActiveInt { get; set; }
        public long? FacilityId { get; set; }
    }

    public class HrNotificationsSettingFilterDto
    {
        public long? Id { get; set; }
        public int? IsActive { get; set; }
        public string? Subject { get; set; }
    }
}

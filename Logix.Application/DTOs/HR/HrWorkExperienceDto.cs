namespace Logix.Application.DTOs.HR
{
    public partial class HrWorkExperienceDto
    {
        public long? Id { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public string? Company { get; set; }
        public string? JobTitle { get; set; }
        public string? FromWork { get; set; }
        public string? ToWork { get; set; }
        public string? Comment { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public partial class HrWorkExperienceEditDto
    {
        public long Id { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public string? Company { get; set; }
        public string? JobTitle { get; set; }
        public string? FromWork { get; set; }
        public string? ToWork { get; set; }
        public string? Comment { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

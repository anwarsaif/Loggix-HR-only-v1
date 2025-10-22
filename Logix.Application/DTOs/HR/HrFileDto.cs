namespace Logix.Application.DTOs.HR
{
    public partial class HrFileDto
    {
        public long? Id { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public string? FileUrl { get; set; }
        public string? Note { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public partial class HrFileEditDto
    {
        public long Id { get; set; }
        public long? EmpId { get; set; }
        public string? FileUrl { get; set; }
        public string? Note { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }

}

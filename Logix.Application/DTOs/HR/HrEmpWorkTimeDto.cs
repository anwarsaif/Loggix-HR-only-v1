namespace Logix.Application.DTOs.HR
{
    public class HrEmpWorkTimeDto
    {
        public long EmpWorkId { get; set; }
        public long? EmpId { get; set; }
        public string? InAm { get; set; }
        public string? OutAm { get; set; }
        public string? InPm { get; set; }
        public string? OutPm { get; set; }
        public string? InPmYest { get; set; }
        public string? OutPmYest { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

    }
    public class HrEmpWorkTimeEditDto
    {
        public long EmpWorkId { get; set; }
        public long? EmpId { get; set; }
        public string? InAm { get; set; }
        public string? OutAm { get; set; }
        public string? InPm { get; set; }
        public string? OutPm { get; set; }
        public string? InPmYest { get; set; }
        public string? OutPmYest { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }

    }
}

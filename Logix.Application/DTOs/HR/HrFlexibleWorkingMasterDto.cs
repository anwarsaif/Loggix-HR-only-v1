namespace Logix.Application.DTOs.HR
{
    public class HrFlexibleWorkingMasterDto
    {
        public long? Id { get; set; }
        public string? Code { get; set; }
        public string? DateFrom { get; set; }
        public string? DateTo { get; set; }
        public string? Note { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class HrFlexibleWorkingMasterEditDto
    {
        public long? Id { get; set; }
        public string? Code { get; set; }
        public string? DateFrom { get; set; }
        public string? DateTo { get; set; }
        public string? Note { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class HrFlexibleWorkingMasterFilterDto
    {
        public string? Code { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? DateFrom { get; set; }
        public string? DateTo { get; set; }
        public int? Branch { get; set; }
        public int? Location { get; set; }
        public int? Dept { get; set; }
        public string? BranchIds { get; set; }

    }

    public class HrFlexibleWorkingMasterAddDto
    {
        public string? DateFrom { get; set; }
        public string? DateTo { get; set; }
        public List<HrFlexibleDetailsAddDto> Details { get; set; }
    }
    public class HrFlexibleDetailsAddDto
    {
        public long EmpId { get; set; }
        public string DayDateGregorian { get; set; } = null!;
        public string TimeOutString { get; set; } = null!;
        public string TimeInString { get; set; } = null!;
        public long Minute { get; set; }
        public long ActualMinute { get; set; }
        public decimal HourCost { get; set; }

    }
}

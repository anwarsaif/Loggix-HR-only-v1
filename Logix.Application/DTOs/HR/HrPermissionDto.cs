namespace Logix.Application.DTOs.HR
{
    public class HrPermissionDto
    {
        public long? Id { get; set; }
        public long? EmpId { get; set; }
        public int? ReasonLeave { get; set; }

        public string? PermissionDate { get; set; }

        public string? DetailsReason { get; set; }

        public string? LeaveingTime { get; set; }

        public string? EstimatedTimeReturn { get; set; }

        public string? ContactNumber { get; set; }


        public int? Type { get; set; }
        public string? Note { get; set; }

        public bool? IsDeleted { get; set; }
        public string? EmpCode { get; set; }
    }

    public class HrPermissionEditDto
    {
        public long Id { get; set; }
        public int? Type { get; set; }
        public int? ReasonLeave { get; set; }
        public string? PermissionDate { get; set; }
        public string? DetailsReason { get; set; }
        public string? LeaveingTime { get; set; }
        public string? EstimatedTimeReturn { get; set; }
        public string? ContactNumber { get; set; }
        public string? Note { get; set; }
        public string? Empcode { get; set; }
    }
    public class HrPermissionFilterDto
    {

        public long Id { get; set; }
        public long? EmpId { get; set; }
        public string? EmpName { get; set; }
        public string? Empcode { get; set; }
        public int? BranchId { get; set; }
        public int? LocationId { get; set; }
        public int? DeptId { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? FromTime { get; set; }
        public string? ToTime { get; set; }
        public string? PermissionDate { get; set; }
        public string? reasonName { get; set; }
        /////////////
        public string? BraName { get; set; }
        public string? DepName { get; set; }
        public string? LocName { get; set; }
        public string? TypeName { get; set; }
        public int? TypeId { get; set; }
        public int? ReasonLeave { get; set; }
        public string? DetailsReason { get; set; }
        public string? LeaveingTime { get; set; }
        public string? EstimatedTimeReturn { get; set; }
        public string? ContactNumber { get; set; }
        public string? TimeDifference { get; set; }
    }
}

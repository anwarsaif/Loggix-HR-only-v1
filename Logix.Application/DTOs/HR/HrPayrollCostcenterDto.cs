namespace Logix.Application.DTOs.HR
{
    public  class HrPayrollCostcenterDto
    {
        public long Id { get; set; }
        public long? MsId { get; set; }
        public long? MsdId { get; set; }
        public long? EmpId { get; set; }
        public long? FacilityId { get; set; }
        public long? BranchId { get; set; }
        public long? DepId { get; set; }
        public long? LocationId { get; set; }
        public int? CountDayWork { get; set; }
        public long? CcId { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public string? Note { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
    }
    public class HrPayrollCostcenterEditDto
    {
        public long Id { get; set; }
        public long? MsId { get; set; }
        public long? MsdId { get; set; }
        public long? EmpId { get; set; }
        public long? FacilityId { get; set; }
        public long? BranchId { get; set; }
        public long? DepId { get; set; }
        public long? LocationId { get; set; }
        public int? CountDayWork { get; set; }
        public long? CcId { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public string? Note { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
    }
}

namespace Logix.Application.DTOs.HR
{
    public  class HrProvisionsEmployeeDto
    {
        public long? Id { get; set; }
        public long? PId { get; set; }
        public long? EmpId { get; set; }
        public decimal? BasicSalary { get; set; }
        public decimal? TotalAllowances { get; set; }
        public decimal? TotalDeductions { get; set; }
        public decimal? NetSalary { get; set; }
        public decimal? TotalSalary { get; set; }
        public decimal? Amount { get; set; }
        public long? DeptId { get; set; }
        public long? LocationId { get; set; }
        public long? FacilityId { get; set; }
        public long? BranchId { get; set; }
        public long? SalaryGroupId { get; set; }
        public long? CcId { get; set; }
        public bool IsDeleted { get; set; }
    }
  
    public  class HrProvisionsEmployeeEditDto
    {
        public long Id { get; set; }
        public long? PId { get; set; }
        public long? EmpId { get; set; }
        public decimal? BasicSalary { get; set; }
        public decimal? TotalAllowances { get; set; }
        public decimal? TotalDeductions { get; set; }
        public decimal? NetSalary { get; set; }
        public decimal? TotalSalary { get; set; }
        public decimal? Amount { get; set; }
        public long? DeptId { get; set; }
        public long? LocationId { get; set; }
        public long? FacilityId { get; set; }
        public long? BranchId { get; set; }
        public long? SalaryGroupId { get; set; }
        public long? CcId { get; set; }
    }

}

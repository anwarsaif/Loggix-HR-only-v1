namespace Logix.Application.DTOs.HR
{
    public partial class HrGosiEmployeeDto
    {
        public long? Id { get; set; }
        public long? GosiId { get; set; }
        public long? EmpId { get; set; }
        public decimal? BasicSalary { get; set; }
        public decimal? HousingAllowance { get; set; }
        public decimal? OtherAllowance { get; set; }
        public decimal? TotalSalary { get; set; }
        public decimal? GosiEmp { get; set; }
        public decimal? GosiCompany { get; set; }
        public decimal? GosiRate { get; set; }
        public decimal? GosiEmpRate { get; set; }
        public decimal? GosiCompanyRate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public long? CcId { get; set; }
    }

    public partial class HrGosiEmployeeEditDto
    {
        public long Id { get; set; }
        public long? GosiId { get; set; }
        public string? EmpCode { get; set; }
        public decimal? HousingAllowance { get; set; }
        public decimal? OtherAllowance { get; set; }
        public decimal? GosiEmp { get; set; }
        public decimal? GosiCompany { get; set; }
        public decimal? GosiRate { get; set; }
        public decimal? GosiEmpRate { get; set; }
        public decimal? GosiCompanyRate { get; set; }
        public long? CcId { get; set; }
        public bool IsDeleted { get; set; }
    }
}

using Logix.Application.DTOs.Main;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logix.Application.DTOs.HR
{
    public partial class HrGosiDto
    {
        public long Id { get; set; }
        public string? TDate { get; set; }
        public string? TMonth { get; set; }
        public int? FinancelYear { get; set; }
        public long? FacilityId { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public bool IsDeleted { get; set; }
        public long? BranchId { get; set; }
    }
    public partial class HrGosiAddDto
    {
        public string? TDate { get; set; }
        public int? TMonth { get; set; }
        public int? FinancelYear { get; set; }
        public long? BranchId { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }
        public List<HrGosiEmployeeForAddDto> GosiEmp { get; set; }

    }
    public partial class HrGosiEmployeeForAddDto
    {
        public long? Id { get; set; }
        public string? emp_ID { get; set; }
        public decimal? gosi_Bisc_Salary { get; set; }
        public decimal? gosi_House_Allowance { get; set; }
        public decimal? gosi_Salary { get; set; }
        public decimal? gosi_Rate_Facility { get; set; }
        public decimal? GosiCompanyRate { get; set; }
        public decimal? gosi_Salary_Facility { get; set; }
        public decimal? gosi_Salary_Emp { get; set; }
        public decimal? gosi_Allowance_Commission { get; set; }
        public long? cC_ID { get; set; }
        public bool IsDeleted { get; set; }
    }
    public partial class HrGosiEditDto
    {
        public long Id { get; set; }
        public string? TDate { get; set; }
        public string? TMonth { get; set; }
        public int? FinancelYear { get; set; }
        public long? FacilityId { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public long? BranchId { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }
        public List<HrGosiEmployeeForEditDto> GosiEmp { get; set; }
    }
    public partial class HrGosiEmployeeForEditDto
    {
        public long? Id { get; set; }
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
        public long? CcId { get; set; }
        public string? CostCenterCode { get; set; }
        public bool IsDeleted { get; set; }
    }
    public partial class HrGosiFilterDto
    {

        public int? Month { get; set; }
        public int? FinancelYear { get; set; }
        public long? BranchId { get; set; }
        /////////////////////////////////
        public decimal? GosiEmp { get; set; }
        public decimal? GosiCompany { get; set; }
        public decimal? Total { get; set; }
        public string? Facility_Name { get; set; }
        public long? ID { get; set; }
        public string? TMonth { get; set; }
        public string? TDate { get; set; }

    }
    public class HrCreateDueDto
    {
        public long Id { get; set; }
        public int tMonth { get; set; }
        public int? FinancelYear { get; set; }
        public long? FacilityId { get; set; }
        public long? BranchId { get; set; }
        [Required]
        public string tDate { get; set; } = null!;
        public List<SaveFileDto>? fileDtos { get; set; }
        public List<HrGosiEmployeeForEditDto> GosiEmp { get; set; }
    }
    public class HRGOSIAccEntryDto
    {
        public long? AccountID { get; set; }
        public decimal? Debit { get; set; }
        public decimal? Credit { get; set; }
        public string? Description { get; set; }
        public long? CC_ID { get; set; }
        public long? Cc2Id { get; set; }
        public long? Cc3Id { get; set; }
        public long? Cc4Id { get; set; }
        public long? Cc5Id { get; set; }
        public long? EmpId { get; set; }
        public long? ReferenceNo { get; set; }
        public int ReferenceType_ID { get; set; }
    }
    public class EmployeeGosiSearchtDto
    {
        public long? FacilityId { get; set; }
        public int? CmdType { get; set; }
        public int? BranchId { get; set; }
        public string? BranchsId { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? Location { get; set; }
        public int? DeptId { get; set; }
        public int? SalaryGroupId { get; set; }
        public string? MonthCode { get; set; }
        public int TMonth { get; set; }
        public int FinancelYear { get; set; }
    }
    //  تقرير استحقاق التأمينات
    public partial class HrRPGosiDueFilterDto
    {

        public int? FacilityId { get; set; }
        public int? BranchId { get; set; }
        public int? DeptId { get; set; }
        public int? Location { get; set; }
        public int? StatusId { get; set; }
        public string? CostCenterName { get; set; }
        public string? EmpName { get; set; }
        public string? EmpCode { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        /////////////////////////////////
        public string? LocationName2 { get; set; }
        public string? LocationName { get; set; }
        public string? DepName2 { get; set; }
        public string? DepName { get; set; }
        public string? EmpName2 { get; set; }
        public string? CostCenterCode { get; set; }
        public string? StatusName { get; set; }
        public string? GosiTypeName { get; set; }
        public string? IdNo { get; set; }
        public decimal? BasicSalary { get; set; }
        public decimal? HousingAllowance { get; set; }
        public decimal? OtherAllowance { get; set; }
        public decimal? TotalSalary { get; set; }
        public decimal? GosiEmp { get; set; }
        public decimal? GosiCompany { get; set; }
        public decimal? Total { get; set; }
        public long? EmpId { get; set; }

    }
    public class HrEmployeeGosiReportDto
    {
        public string? IdNo { get; set; }
        public decimal? GosiSalary { get; set; }
        public string? GosiDate { get; set; }
        public string? GosiNo { get; set; }
        public string? GoisSubscriptionExpiryDate { get; set; }
        public decimal? GosiRateFacility { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? LocationName { get; set; }
        public string? CostCenterCode { get; set; }
        public string? CostCenterName { get; set; }
        public long? CcId { get; set; }
        public decimal? GosiBiscSalary { get; set; }
        public decimal? GosiHouseAllowance { get; set; }
        public decimal? GosiAllowanceCommission { get; set; }
        public decimal? GosiOtherAllowances { get; set; }
        public string? GosiName { get; set; }
        public decimal? GosiSalaryFacility { get; set; }
        public decimal? GosiSalaryEmp { get; set; }
        public decimal? GosiTotalSalary { get; set; }
        //public DateTime? StopDateSalary { get; set; }
    }
    public class HrEmployeeGosiReportFilterDto
    {
        public string? MonthCode { get; set; }
        public long FacilityId { get; set; } = 0;
        public string? EmpName { get; set; }
        public string? EmpCode { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? Location { get; set; }
        public string? BranchIds { get; set; }
        public int? BranchId { get; set; }
        public int? SalaryGroupId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Application.DTOs.Main;
using Logix.Domain.HR;

namespace Logix.Application.DTOs.HR
{
    public class HrContracteDto
    {
        public int? contractDurationType { get; set; }
        public int? ContractDurationNo { get; set; }
        public string? Note { get; set; }
        public string? TDate { get; set; }
        public List<string> EmpCodes { get; set; }

        //
        public long? EmpId { get; set; }
        public string? StartDate { get; set; }
        public int? ApplyType { get; set; }
        public bool? IsDeleted { get; set; }
    }

    public class HrContracteAdd2Dto
    {
        public long? Id { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public int? BranchId { get; set; }
        public int? deptId { get; set; }
        public int? LocationId { get; set; }
        public string? TDate { get; set; }
        public int? FacilityId { get; set; }
        public string? StartContractDate { get; set; }
        public string? ContractExpiryDate { get; set; }
        public string? NewStartContractDate { get; set; }
        public string? NewContractExpiryDate { get; set; }
        public int? ContractDurationType { get; set; }
        public int? ContractDurationNo { get; set; }
        public string? Note { get; set; }

        public bool? IsDeleted { get; set; }
        public List<SaveFileDto?> fileDtos { get; set; }

        [Column("With_Salary_Inc")]
        public bool? WithSalaryInc { get; set; }

        [Column("Old_Salary", TypeName = "decimal(18, 2)")]
        public decimal? OldSalary { get; set; }

        [Column("New_Salary", TypeName = "decimal(18, 2)")]
        public decimal? NewSalary { get; set; }

        [Column("Inc_Rate", TypeName = "decimal(18, 2)")]
        public decimal? IncRate { get; set; }

        [Column("Inc_Amount", TypeName = "decimal(18, 2)")]
        public decimal? IncAmount { get; set; }

        [Column("Start_Date")]
        [StringLength(10)]
        public string? StartDate { get; set; }

        [Column("Apply_Type")]
        public int? ApplyType { get; set; }

        //[Column("Decision_No")]
        //[StringLength(250)]
        public int? DecisionNo { get; set; }

        [Column("Decision_Date")]
        [StringLength(10)]
        public string? DecisionDate { get; set; }

        public decimal? DifferenceAmount { get; set; }
        public string? DeductionDate { get; set; }
        public int? DeductionType { get; set; }

        public List<HrContractsAllowanceDeductionDto>? allowances { get; set; }
        public List<HrContractsAllowanceDeductionDto>? deductions { get; set; }
    }
    public class HrContracteAdd3Dto
    {
        public long Id { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public int? BranchId { get; set; }
        public int? DepartmentId { get; set; }
        public int? LocationId { get; set; }
        public string? TDate { get; set; }
        public int? FacilityId { get; set; }
        public string? StartContractDate { get; set; }
        public string? ContractExpiryDate { get; set; }
        public string? NewStartContractDate { get; set; }
        public string? NewContractExpiryDate { get; set; }
        public int? ContractDurationType { get; set; }
        public int? ContractDurationNo { get; set; }
        public int? TypeId { get; set; }
        public string? Note { get; set; }
        public bool? WithSalaryInc { get; set; }
        public decimal? OldSalary { get; set; }
        public decimal? NewSalary { get; set; }
        public decimal? IncRate { get; set; }
        public decimal? IncAmount { get; set; }
        public string? StartDate { get; set; }
        public int? ApplyType { get; set; }
        public string? DecisionNo { get; set; }
        public string? DecisionDate { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }
    }
    public class HrContracteEditDto
    {


        public long Id { get; set; }
        public long? EmpId { get; set; }
        public string? empCode { get; set; }

        public int? BranchId { get; set; }
        public int? DepartmentId { get; set; }
        public int? LocationId { get; set; }
        public string? TDate { get; set; }
        public int? FacilityId { get; set; }
        public string? StartContractDate { get; set; }
        public string? ContractExpiryDate { get; set; }
        public string? NewStartContractDate { get; set; }
        public string? NewContractExpiryDate { get; set; }
        [Required]
        public int? ContractDurationType { get; set; }
        [Required]

        public int? ContractDurationNo { get; set; }
        public int? TypeId { get; set; }
        public string? Note { get; set; }

        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        // table id=106

        [Column("With_Salary_Inc")]
        public bool? WithSalaryInc { get; set; }

        [Column("Old_Salary", TypeName = "decimal(18, 2)")]
        public decimal? OldSalary { get; set; }

        [Column("New_Salary", TypeName = "decimal(18, 2)")]
        public decimal? NewSalary { get; set; }

        [Column("Inc_Rate", TypeName = "decimal(18, 2)")]
        public decimal? IncRate { get; set; }

        [Column("Inc_Amount", TypeName = "decimal(18, 2)")]
        public decimal? IncAmount { get; set; }

        [Column("Start_Date")]
        [StringLength(10)]
        public string? StartDate { get; set; }

        [Column("Apply_Type")]
        public int? ApplyType { get; set; }

        [Column("Decision_No")]
        [StringLength(250)]
        public string? DecisionNo { get; set; }

        [Column("Decision_Date")]
        [StringLength(10)]
        public string? DecisionDate { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }
    }
    public class HrContractFilterDto
    {

        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? BranchId { get; set; }
        public int? DepartmentId { get; set; }
        public int? LocationId { get; set; }


    }
    public class HrContractAddFilterDto
    {

        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public int? BranchId { get; set; }
        public int? DepartmentId { get; set; }
        public int? LocationId { get; set; }
        public int? ContractTypeID { get; set; }
        public int? JobCategory { get; set; }
        public int? NationalityId { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        /////////////////////////////////////////////
        public decimal? Salary { get; set; }
        public decimal? NetSalary { get; set; }
        public string? BranchName { get; set; }
        public string? DepartmentName { get; set; }
        public string? LocationName { get; set; }
        public string? NationalityName { get; set; }
        public string? IdNo { get; set; }
        public int? RemainingDays { get; set; }
        public string? ContractExpiryDate { get; set; }
        public decimal? Allowance { get; set; }
        public decimal? Deduction { get; set; }

    }
    public class HrContractAddResponseDto : HrEmployeeVw
    {
        public int? RemainingDays { get; set; }
        public decimal? NetSalary { get; set; }
        public decimal? Allowance { get; set; }
        public decimal? Deduction { get; set; }

    }
}

using Castle.MicroKernel.SubSystems.Conversion;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Logix.Application.DTOs.HR
{
    public class HrIncrementDto
    {
        public long Id { get; set; }
        public long? EmpId { get; set; }
        [StringLength(10)]
        public string? IncreaseDate { get; set; }
        public decimal? IncreaseAmount { get; set; }
        [StringLength(10)]
        public string? StartDate { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Allowances { get; set; }
        public decimal? Deductions { get; set; }
        public bool? StatusId { get; set; }
        public decimal? NewSalary { get; set; }
        public int? ApplyType { get; set; }
        public string? Note { get; set; }
        public long? CurCatJobId { get; set; }
        public long? CurJobId { get; set; }
        public long? CurLevelId { get; set; }
        public long? CurGradId { get; set; }
        public long? NewLevelId { get; set; }
        public long? NewGradId { get; set; }
        public long? NewCatJobId { get; set; }
        public long? NewJobId { get; set; }
        [StringLength(250)]
        public string? DecisionNo { get; set; }
        [StringLength(10)]
        public string? DecisionDate { get; set; }
        public int? TransTypeId { get; set; }
        public int? AppId { get; set; }
        public long? PackageNumber { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }

    }

    public class HrIncrementEditDto
    {
        public long Id { get; set; }
        public long? EmpId { get; set; }
        [StringLength(10)]
        public string? IncreaseDate { get; set; }
        public decimal? IncreaseAmount { get; set; }
        [StringLength(10)]
        public string? StartDate { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Allowances { get; set; }
        public decimal? Deductions { get; set; }
        public bool? StatusId { get; set; }
        public decimal? NewSalary { get; set; }
        public int? ApplyType { get; set; }
        public string? Note { get; set; }
        public long? CurCatJobId { get; set; }
        public long? CurJobId { get; set; }
        public long? CurLevelId { get; set; }
        public long? CurGradId { get; set; }
        public long? NewLevelId { get; set; }
        public long? NewGradId { get; set; }
        public long? NewCatJobId { get; set; }
        public long? NewJobId { get; set; }
        [StringLength(250)]
        public string? DecisionNo { get; set; }
        [StringLength(10)]
        public string? DecisionDate { get; set; }
        public int? TransTypeId { get; set; }
        public int? AppId { get; set; }
        public long? PackageNumber { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }
    public class HrIncrementFilterDto
    {
        public int? BranchId { get; set; }
        public int? DeptId { get; set; }
        public int? TransactionType { get; set; }
        public int? Location { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }

        ////////////////////////////////////
    }
    public class HrIncrementsAddDto
    {
        public long Id { get; set; }
        public string? EmpCode { get; set; }
        public string? IncreaseDate { get; set; }
        public decimal? IncreaseAmount { get; set; }
        public string? StartDate { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Allowances { get; set; }
        public decimal? Deductions { get; set; }
        public bool? StatusId { get; set; }
        public decimal? NewSalary { get; set; }
        public int? ApplyType { get; set; }
        public string? Note { get; set; }
        public long? CurCatJobId { get; set; }
        public long? CurJobId { get; set; }
        public long? CurLevelId { get; set; }
        public long? CurGradId { get; set; }
        public long? NewLevelId { get; set; }
        public long? NewGradId { get; set; }
        public long? NewCatJobId { get; set; }
        public long? NewJobId { get; set; }
        public string? DecisionNo { get; set; }
        public string? DecisionDate { get; set; }
        public int? TransTypeId { get; set; }
        public int? AppTypeId { get; set; } = 0;
        public long? PackageNumber { get; set; }
        public int? DeductionType { get; set; }
        public decimal? DifferenceAmount { get; set; }
        public string? DeductionDate { get; set; }
        public decimal? IncrementRate { get; set; }
        public List<IncrementAllowanceDeductionDto>? allowancesList { get; set; }
        public List<IncrementAllowanceDeductionDto>? deductionsList { get; set; }
        public bool? ChkRetroactiveAmount { get; set; }
        public bool? ChkUpdateInsuranceInEmployeeFile { get; set; }
        public string? DateRetroactiveAmount { get; set; }
        public decimal? TxtRetroactiveAmount { get; set; }
        public int? DDLallowanceRetroactiveAmount { get; set; }
    }
    public class IncrementAllowanceDeductionDto
    {
        public long Id { get; set; }
        public int? TypeId { get; set; }
        public int? AdId { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public decimal? NewAmount { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AllDedId { get; set; }

        public bool IsNew { get; set; }

        public bool? IsUpdated { get; set; }
    }

    public class IncrementsBothFilterDto
    {
        public int? BranchId { get; set; }
        public int? DeptId { get; set; }
        public int? Nationality { get; set; }
        public int? Location { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? ToDate { get; set; }
        public string? Branches { get; set; }
        public int? CMDTYPE { get; set; }
        public int? AnnaulIncreaseMethod { get; set; }
        public string? EvaluationFromDate { get; set; }
        public string? EvaluationToDate { get; set; }

    }

  

    [Keyless]
    public class HrEmployeeIncremenResultDto
    {
        public long? ID { get; set; }
        public string? Emp_ID { get; set; }
        public string? Emp_name { get; set; }
        public string? Emp_name2 { get; set; }
        public decimal? Salary { get; set; }
        public decimal? NewSalary { get; set; }
        public decimal? Increase_Amount { get; set; }
        public int? Allowances { get; set; } = 0;
        public int? Deductions { get; set; } = 0;
        public int? Status_ID { get; set; } = 1;
        public int? Apply_Type { get; set; } = 1;
        public int? Cur_Cat_Job_ID { get; set; }
        public int? New_Cat_Job_ID { get; set; }
        public long? Cur_Job_ID { get; set; }
        public long? New_Job_ID { get; set; }
        public int? Cur_Level_ID { get; set; }
        public int? New_Level_ID { get; set; }
        public int? Cur_Grad_ID { get; set; }
        public long? New_Grad_ID { get; set; }
        public string? Cur_Level_Name { get; set; }
        public string? Cur_Grad_Name { get; set; }
        public string? New_Grad_Name { get; set; }
        public int? Trans_Type_ID { get; set; } = 1;
        public int? AnnualIncreaseMethod { get; set; }
        public decimal? Evaluation { get; set; }
        public string? LastIncrementDate { get; set; }
        public string? DueDate { get; set; }
    }
    public class MakeApproveDto
    {
        public string? ToDate { get; set; }
        public string? emp_ID { get; set; }
        public long? new_Grad_ID { get; set; }
        public decimal? increase_Amount { get; set; }
        public decimal? NewSalary { get; set; }

    }
    public class HrPromotionsFilterDto
    {
        public int? BranchId { get; set; }
        public string? EmpName { get; set; }
        public string? EmpId { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int? LocationId { get; set; }
        public int? DeptId { get; set; }
    }


}



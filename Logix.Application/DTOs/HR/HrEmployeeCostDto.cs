
using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace Logix.Application.DTOs.HR
{
    public class HrEmployeeCostDto
    {
        public long? Id { get; set; }
        public int? EmpId { get; set; }
        public int? CostTypeId { get; set; }
        public int? TypeCalculation { get; set; }
        public int? CostRate { get; set; }
        public decimal? CostValue { get; set; }
        public DateTime? TransDate { get; set; }
        public string? Description { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public bool? Active { get; set; }
        public bool? IsDeleted { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public long? ProjectId { get; set; }
    }
    public class HrEmployeeCostEditDto
    {
        public long Id { get; set; }
        public int? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public int? CostTypeId { get; set; }
        public int? TypeCalculation { get; set; }
        public int? CostRate { get; set; }
        public decimal? CostValue { get; set; }
        public DateTime? TransDate { get; set; }
        public string? Description { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public bool? Active { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? ProjectId { get; set; }
        public string? ProjectCode { get; set; }
    }

    public class HrEmployeeCostFilterDto
    {
        public long? Id { get; set; } 
        public string? EmpCode { get; set; } 

        public string? EmpName { get; set; }
        public int? NationalityId { get; set; }
        public int? Location { get; set; }
        public int? DeptId { get; set; }
        ///////////////////////////////////////
        public string? TypeName { get; set; }
        public decimal? CostValue { get; set; }
        public string? TypeCalculationName { get; set; }
       
    }

    public class HrEmployeeCostDataDto
    {
        public long Id { get; set; }
        [Required]
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? ProjectCod { get; set; }
         public decimal? Salary { get; set; }
        public decimal AllowancesAmount { get; set; }
        public decimal DeductionsAmount { get; set; }
        public decimal? NetSalary { get; set; }
        public int? NationalityId { get; set; }
        public List<HrDeductionVM>? deduction { get; set; }
        public List<HrAllowanceVM>? allowance { get; set; }
        public List<HrCostTypeDto>? costType { get; set; }

    }
   
    public class HrEmployeeCostReportFilterDto
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? fromDate { get; set; }
        public string? ToDate { get; set; }
        public long? Nationality { get; set; }
        public long? Location { get; set; }
        public long? Department { get; set; }
        public long? StatusId { get; set; }
        public long? BranchId { get; set; }
        public string? BranchsId { get; set; }
      
    }



    public class HrEmployeeCostDataForEditDto
    {
        public long Id { get; set; }
        [Required]
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? ProjectCode { get; set; }
        public decimal? Salary { get; set; }
        public decimal AllowancesAmount { get; set; }
        public decimal DeductionsAmount { get; set; }
        public decimal? NetSalary { get; set; }
        public int? NationalityId { get; set; }
        public string? ProjectName { get; set; }

        public long? ProjectId { get; set; }
        public decimal? CostValue { get; set; }
        public string? TypeCalculationName { get; set; }
        public string? ExpenseArName { get; set; }
        public decimal? CalculationValue { get; set; }
        public bool? Active { get; set; }
        public int? CostRate { get; set; }
        public string? Description { get; set; }

    }
}

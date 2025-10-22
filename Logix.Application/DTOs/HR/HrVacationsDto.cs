using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrVacationsDto
    {
        public long VacationId { get; set; }
        public string? EmpCode { get; set; }
        public long? EmpId { get; set; }
        public int? VacationTypeId { get; set; }
        public string? VacationSdate { get; set; }
        public string? VacationEdate { get; set; }
        public int? VacationsDayTypeId { get; set; }
        public decimal? VacationAccountDay { get; set; }
        public Decimal? Balance { get; set; } //TxtBlance_Vacation
        public bool? IsSalary { get; set; }
        public bool? NeedJoinRequest { get; set; }
        public string? Note { get; set; }
        // this for HR_Allowance_Deduction
        public bool ChkAddAsDeduction { get; set; }
        public decimal? DeductionAmount { get; set; }
        public int? DeductionType { get; set; }
        [StringLength(10)]
        public string? DueDate { get; set; }




        public string? VacationRdate { get; set; }
        public int? HrVdtId { get; set; }
        public string? DecisionNo { get; set; }
        public string? DecisionDate { get; set; }
        public int? FinancelYear { get; set; }
        public bool? Approve { get; set; }
        public int? StatusId { get; set; }
        public int? LocationId { get; set; }
        public int? ShiftId { get; set; }
        public int? TimeTableId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        //public string? Tdate { get; set; }
        public long? AlternativeEmpId { get; set; }
        public long? ApplicationId { get; set; }
        public int? TransTypeId { get; set; }
    }

    public class HrVacationsEditDto
    {
        public long VacationId { get; set; }
        public string? EmpCode { get; set; }
        public long? EmpId { get; set; }
        public string? VacationSdate { get; set; }
        public string? VacationEdate { get; set; }
        public decimal? VacationAccountDay { get; set; }
        public int VacationTypeId { get; set; }
        public bool? IsSalary { get; set; }
        public bool? NeedJoinRequest { get; set; }
        // تاريخ المباشرة بعد الإجازة
        public string? VacationRdate { get; set; }
        public string? Note { get; set; }

        public Decimal? Balance { get; set; }
        public int? VacationsDayType { get; set; } = 1;
        public long? AlternativeEmpId { get; set; }
        public int? VacationsDayTypeId { get; set; }
        public long? ApplicationId { get; set; }
        public int? TransTypeId { get; set; }
    }

    public class HrVacationsFilterDto
    {
        public long? VacationId { get; set; }
        public int? VacationTypeId { get; set; }

        public string? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public int? BranchId { get; set; }
        public string? EmpName { get; set; }
        public string? EmpName2 { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public int? DeptId { get; set; }
        public int? LocationId { get; set; }
        public int? ClearnceId { get; set; }
        public string? BraName { get; set; }
        public string? BraName2 { get; set; }
        public string? LocationName { get; set; }
        public string? LocationName2 { get; set; }
        public string? DeptName { get; set; }
        public string? DeptName2 { get; set; }
        public string? VacationTypeName { get; set; }
        public string? VacationTypeName2 { get; set; }
        public decimal? VacationAccountDay { get; set; }
        public bool? IsSalary { get; set; }
        public bool? ChkGroupByEmpAndVacation { get; set; } = false;
        public string? Note { get; set; }
        public long? ApplicationId { get; set; }
        public int? TransTypeId { get; set; }
    }

    public class HrRPVacationEmployeeFilterDto
    {
        public long? VacationId { get; set; }
        public int? VacationTypeId { get; set; }

        public string? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public int? BranchId { get; set; }
        public string? EmpName { get; set; }
        public string? EmpName2 { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public int? DeptId { get; set; }
        public int? LocationId { get; set; }
        public int? ClearnceId { get; set; }
        public string? BraName { get; set; }
        public string? BraName2 { get; set; }
        public string? LocationName { get; set; }
        public string? LocationName2 { get; set; }
        public string? DeptName { get; set; }
        public string? DeptName2 { get; set; }
        public string? VacationTypeName { get; set; }
        public string? VacationTypeName2 { get; set; }
        public decimal? VacationAccountDay { get; set; }
        public bool? IsSalary { get; set; }
        public string? Note { get; set; }
        public string? VacationAlternetiveEmpNo { get; set; }
        public string? VacationAlternetiveEmpName { get; set; }
    }

    public class GetDaysClickDto
    {
        public string? SDate { get; set; }
        public string? EDate { get; set; }
        public string? EmpCode { get; set; }
        public int VacationTypeId { get; set; }
        public int VacationsDayType { get; set; } = 1;

        public decimal? DeductionDays { get; set; }
        public decimal? TotalSlary { get; set; }
    }
}

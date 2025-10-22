using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrVacationBalanceDto
    {
        public long VacBalanceId { get; set; }
        [Required]
        public decimal? VacationBalance { get; set; }
        public decimal? VBalanceRate { get; set; }
        [Required]
        public string EmpCode { get; set; } = null!;
        [Required]
        public string? StartDate { get; set; }
        public bool? Isacitve { get; set; }
        [Required]
        public int? VacationTypeId { get; set; }
        public string? Note { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class HrVacationBalanceEditDto
    {
        public long VacBalanceId { get; set; }
        [Required]
        public decimal? VacationBalance { get; set; }
        public decimal? VBalanceRate { get; set; }
        [Required]
        public string EmpCode { get; set; } = null!;
        [Required]
        public string? StartDate { get; set; }
        //public bool? Isacitve { get; set; }
        [Required]
        public int? VacationTypeId { get; set; }
        public string? Note { get; set; }
    }

    public class HrVacationBalanceFilterDto
    {
        public long? Id { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? EmpName2 { get; set; }
        public int? DeptId { get; set; }
        public int? Location { get; set; }
        public int? BranchId { get; set; }
        public int? VacationTypeId { get; set; }


        //////////////////////
        public string? DepName { get; set; }
        public string? DepName2 { get; set; }
        public string? LocationName { get; set; }
        public string? LocationName2 { get; set; }
        public string? BraName { get; set; }
        public string? BraName2 { get; set; }
        public string? StartDate { get; set; }
        public string? VacationTypeName { get; set; }
        public string? VacationTypeName2 { get; set; }
        public decimal? VacationBalance { get; set; }
        public decimal? VBalanceRate { get; set; }
        public string? Note { get; set; }
    }

    public class HrVacationBalanceALLSendFilterDto
    {
        public string? EmpId { get; set; }
        public string? EmpName { get; set; }
        public long? BranchId { get; set; }
        public string? BranchsId { get; set; }
        public string? CurrentDate { get; set; }
        public int? VacationTypeId { get; set; }
        public int? FacilityId { get; set; }
        public int? Location { get; set; }
        public int? DeptId { get; set; }
        public int? NationalityId { get; set; }
        public int? StatusId { get; set; }
        public int? JobCatagoriesId { get; set; }
        //////////////////////
    }

    [Keyless]
    public class HrVacationBalanceALLFilterDto
    {
        public long? ID { get; set; }
        public string? Emp_ID { get; set; }
        public string? Emp_Name { get; set; }
        public string? Emp_Name2 { get; set; }
        public string? Vacation_Type_Name { get; set; }
        public string? Vacation_Type_Name2 { get; set; }
        public decimal? Balance { get; set; }
        public string? Facility_Name { get; set; }
        public string? Facility_Name2 { get; set; }
        public string? Location_Name { get; set; }
        public string? Location_Name2 { get; set; }
        public string? BRA_NAME { get; set; }
        public string? BRA_NAME2 { get; set; }
        public string? Dep_name { get; set; }
        public string? Dep_name2 { get; set; }
        public string? cat_name { get; set; }
        public string? cat_name2 { get; set; }
        public string? Nationality_Name { get; set; }
        public string? Nationality_Name2 { get; set; }
        public decimal? Vacations { get; set; }
    }

    [Keyless]
    public class HrVacationEmpBalanceDto
    {
        public long? Emp_ID { get; set; }
        public string? Emp_Code { get; set; }
        public string? emp_name { get; set; }
        public string? Currentdate { get; set; }
        public decimal? Vac_Open_balance { get; set; }
        public decimal? Vacation_Account_DayAll { get; set; }
        public decimal? Vac_Merit { get; set; }
        public decimal? Vacmerit1 { get; set; }
        public decimal? Vact_Balance_Total { get; set; }
        public string? Vacation_Account_DayInYear { get; set; }
        public string? VacStartDate { get; set; }
    }
}

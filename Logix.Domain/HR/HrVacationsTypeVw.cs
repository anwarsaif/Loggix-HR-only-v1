using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrVacationsTypeVw
    {
        [Column("Vacation_Type_Id")]
        public int VacationTypeId { get; set; }
        [Column("Vacation_Type_Name")]
        [StringLength(500)]
        public string? VacationTypeName { get; set; }
        [Column("Vacation_Type_Name2")]
        [StringLength(500)]
        public string? VacationTypeName2 { get; set; }
        [Column("Vacation_Type_Minmam", TypeName = "decimal(18, 2)")]
        public decimal? VacationTypeMinmam { get; set; }
        [Column("Vacation_Type_Maxmam", TypeName = "decimal(18, 2)")]
        public decimal? VacationTypeMaxmam { get; set; }
        [Column("Emp_Type_ID")]
        public int? EmpTypeId { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        [Column("SS_Code")]
        public string? SsCode { get; set; }
        [Column("Deduction_Days", TypeName = "decimal(18, 2)")]
        public decimal? DeductionDays { get; set; }
        [Column("Validate_Balance")]
        public bool? ValidateBalance { get; set; }
        [Column("Cat_ID")]
        public int? CatId { get; set; }
        [Column("Weekend_Include")]
        public bool? WeekendInclude { get; set; }
        [Column("Deducted_Service_Period")]
        public bool? DeductedServicePeriod { get; set; }
        [Column("Deducted_Balance_Vacation")]
        public bool? DeductedBalanceVacation { get; set; }
        [Column("Rate_Type")]
        public int? RateType { get; set; }
        [Column("Salary_Basic")]
        public bool? SalaryBasic { get; set; }
        public string? Allowance { get; set; }
        public string? Deduction { get; set; }
        public bool? NeedJoinRequest { get; set; }
    }
}

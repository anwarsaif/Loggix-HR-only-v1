using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR;

[Keyless]
public partial class HrClearanceMonthsVw
{
    [Column("ID")]
    public long Id { get; set; }

    [Column("Clearance_ID")]
    public long? ClearanceId { get; set; }

    public int? FinancelYear { get; set; }

    [Column("MS_Month")]
    [StringLength(2)]
    public string? MsMonth { get; set; }

    [Column("MS_Date")]
    [StringLength(10)]
    public string? MsDate { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Salary { get; set; }

    [Column("allowance", TypeName = "decimal(18, 2)")]
    public decimal? Allowance { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Deduction { get; set; }

    [Column("Count_Day_Work")]
    public int? CountDayWork { get; set; }

    [Column("Day_Absence")]
    public int? DayAbsence { get; set; }

    [Column("M_Delay")]
    public long? MDelay { get; set; }

    [Column("H_Extra_time", TypeName = "decimal(18, 2)")]
    public decimal? HExtraTime { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Absence { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Delay { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Loan { get; set; }

    [Column("Deduction_Other", TypeName = "decimal(18, 2)")]
    public decimal? DeductionOther { get; set; }

    [Column("Extra_time", TypeName = "decimal(18, 2)")]
    public decimal? ExtraTime { get; set; }

    public bool? IsDeleted { get; set; }

    [Column("allowance_Other", TypeName = "decimal(18, 2)")]
    public decimal? AllowanceOther { get; set; }

    [Column("Due_Day_Work", TypeName = "decimal(18, 2)")]
    public decimal? DueDayWork { get; set; }

    [Column("Day_PrevMonth")]
    public int? DayPrevMonth { get; set; }

    [Column("Due_PrevMonth", TypeName = "decimal(18, 2)")]
    public decimal? DuePrevMonth { get; set; }

    [Column("Package_No")]
    [StringLength(50)]
    public string? PackageNo { get; set; }

    [Column("Facility_ID")]
    public int? FacilityId { get; set; }

    public string? Note { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Commission { get; set; }

    [Column("Payroll_Type_ID")]
    public int? PayrollTypeId { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Penalties { get; set; }

    [Column("Emp_ID")]
    public long? EmpId { get; set; }

    [Column("Date_C")]
    [StringLength(10)]
    public string? DateC { get; set; }
}

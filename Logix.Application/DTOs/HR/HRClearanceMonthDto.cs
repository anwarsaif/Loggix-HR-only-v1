using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;

namespace Logix.Application.DTOs.HR
{
    public class HrClearanceMonthDto : TraceEntity
    {
        [Column("ID")]
        public long Id { get; set; }
        public long? ClearanceId { get; set; }
        public int? FinancelYear { get; set; }
        [StringLength(2)]
        public string? MsMonth { get; set; }
        [StringLength(10)]
        public string? MsDate { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Allowance { get; set; }
        public decimal? Deduction { get; set; }
        public int? CountDayWork { get; set; }
        public int? DayAbsence { get; set; }
        public long? MDelay { get; set; }
        public decimal? HExtraTime { get; set; }
        public decimal? Absence { get; set; }
        public decimal? Delay { get; set; }
        public decimal? Loan { get; set; }
        public decimal? DeductionOther { get; set; }
        public decimal? ExtraTime { get; set; }

        public decimal? AllowanceOther { get; set; }
        public decimal? DueDayWork { get; set; }
        public int? DayPrevMonth { get; set; }
        public decimal? DuePrevMonth { get; set; }
        [StringLength(50)]
        public string? PackageNo { get; set; }
        public int? FacilityId { get; set; }
        public string? Note { get; set; }
        public decimal? Commission { get; set; }
        public int? PayrollTypeId { get; set; }
        public decimal? Penalties { get; set; }
    }

    public class HrClearanceMonthEditDto
    {
        [Column("ID")]
        public long Id { get; set; }
        public long? ClearanceId { get; set; }
        public int? FinancelYear { get; set; }
        [StringLength(2)]
        public string? MsMonth { get; set; }
        [StringLength(10)]
        public string? MsDate { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Allowance { get; set; }
        public decimal? Deduction { get; set; }
        public int? CountDayWork { get; set; }
        public int? DayAbsence { get; set; }
        public long? MDelay { get; set; }
        public decimal? HExtraTime { get; set; }
        public decimal? Absence { get; set; }
        public decimal? Delay { get; set; }
        public decimal? Loan { get; set; }
        public decimal? DeductionOther { get; set; }
        public decimal? ExtraTime { get; set; }

        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public decimal? AllowanceOther { get; set; }
        public decimal? DueDayWork { get; set; }
        public int? DayPrevMonth { get; set; }
        public decimal? DuePrevMonth { get; set; }
        [StringLength(50)]
        public string? PackageNo { get; set; }
        public int? FacilityId { get; set; }
        public string? Note { get; set; }
        public decimal? Commission { get; set; }
        public int? PayrollTypeId { get; set; }
        public decimal? Penalties { get; set; }
    }

    public class HrClearanceMonthVm
    {
        // used in vacation clearance2 screen
        public long Id { get; set; }
        public int? FinancialYear { get; set; }
        public int? CountDayWork { get; set; }
        public int? DayAbsence { get; set; }
        public long? MDelay { get; set; }
        public int? DayPrevMonth { get; set; }
        public string? Note { get; set; }
        [StringLength(2)]
        public string? Month { get; set; } //MsMonth
        [StringLength(10)]
        public string? MsDate { get; set; }
        public decimal? Salary { get; set; }
        public decimal? Allowance { get; set; }
        public decimal? Deduction { get; set; }
        public decimal? HExtraTime { get; set; }
        public decimal? Absence { get; set; }
        public decimal? Delay { get; set; }
        public decimal? Loan { get; set; }
        public decimal? DeductionOther { get; set; }
        public decimal? ExtraTime { get; set; }
        public decimal? AllowancesOther { get; set; }
        public decimal? DueDayWork { get; set; }
        public decimal? PrevMonth { get; set; }//DuePrevMonth
        public decimal? Commission { get; set; }
        public decimal? Penalties { get; set; }
        public decimal? Total { get; set; }

        public long? ClearanceId { get; set; }
        public string? EmpCode { get; set; }
    }
}

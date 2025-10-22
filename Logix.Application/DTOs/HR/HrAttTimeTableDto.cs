using Castle.MicroKernel.SubSystems.Conversion;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Logix.Application.DTOs.HR
{
    public class HrAttTimeTableDto
    {
        public long Id { get; set; }
        [StringLength(50)]
        [Required]
        public string? TimeTableName { get; set; }

        [Required]
        public string OnDutyTimeString { get; set; } = null!;
        [Required]
        public string OffDutyTimeString { get; set; } = null!;
        [Required]
        public int? LateTimeM { get; set; }

        [Required]
        public int? LeaveEarlyTimeM { get; set; }

        [Required]
        public string BeginInString { get; set; } = null!;
        [Required]
        public string EndInString { get; set; } = null!;

        [Required]
        public string BeginOutString { get; set; } = null!;
        [Required]
        public string EndOutString { get; set; } = null!;
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? FlexibleAttendance { get; set; }
        public string? FlexibleStartString { get; set; } 

        public string ?FlexibleEndString { get; set; }
        public bool? ExitOnNextDate { get; set; }
        public bool? Overtime { get; set; }
        public bool? Is24hourShift { get; set; }
        [Required]
        public decimal? ShiftWorkHour { get; set; }
        [Required]
        public long? CheckoutTimeAllowed { get; set; }
        public bool? EntryPreviousDay { get; set; }
        // ارقام الأيام
        public string? DaysNumbers { get; set; }

    }
    public class HrAttTimeTableEditDto
    {
        public long Id { get; set; }
        [StringLength(50)]
        [Required]
        public string? TimeTableName { get; set; }
        public string OnDutyTimeString { get; set; } = null!;
        [Required]
        public string OffDutyTimeString { get; set; } = null!;
        [Required]
        public int? LateTimeM { get; set; }

        [Required]
        public int? LeaveEarlyTimeM { get; set; }

        public string BeginInString { get; set; } = null!;
        [Required]
        public string EndInString { get; set; } = null!;

        [Required]
        public string BeginOutString { get; set; } = null!;
        [Required]
        public string EndOutString { get; set; } = null!;
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public bool? FlexibleAttendance { get; set; }
        public string? FlexibleStartString { get; set; }

        public string? FlexibleEndString { get; set; }
        public bool? ExitOnNextDate { get; set; }
        public bool? Overtime { get; set; }
        public bool? Is24hourShift { get; set; }
        [Required]
        public decimal? ShiftWorkHour { get; set; }
        [Required]
        public long? CheckoutTimeAllowed { get; set; }
        public bool? EntryPreviousDay { get; set; }
        // ارقام الأيام
        public string? DaysNumbers { get; set; }
    }
}

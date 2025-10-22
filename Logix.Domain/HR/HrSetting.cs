using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Setting")]
    public  class HrSetting
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }

        [Column("Facility_ID")]
        public long? FacilityId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? OverTime { get; set; }

        [Column("Apply_Att_Disciplinary")]
        public int? ApplyAttDisciplinary { get; set; }

        [Column("Apply_Att_Delay")]
        public int? ApplyAttDelay { get; set; }

        [Column("Month_Start_Day")]
        [StringLength(2)]
        public string? MonthStartDay { get; set; }

        [Column("Month_End_Day")]
        [StringLength(2)]
        public string? MonthEndDay { get; set; }

        [Column("App_Absence_Disciplinary")]
        public int? AppAbsenceDisciplinary { get; set; }

        [Column("Housing_allowance")]
        public int? HousingAllowance { get; set; }

        [Column("Transport_allowance")]
        public int? TransportAllowance { get; set; }

        [Column("Mobile_allowance")]
        public int? MobileAllowance { get; set; }

        [Column("PrevMonth_allowance")]
        public int? PrevMonthAllowance { get; set; }

        [Column("Bonuses_allowance")]
        public int? BonusesAllowance { get; set; }

        [Column("Badalat_allowance")]
        public int? BadalatAllowance { get; set; }

        [Column("Food_allowance")]
        public int? FoodAllowance { get; set; }

        [Column("GOSI_Deduction")]
        public int? GosiDeduction { get; set; }

        [Column("Moror_Deduction")]
        public int? MororDeduction { get; set; }

        [Column("Housing_Deduction")]
        public int? HousingDeduction { get; set; }

        [Column("Mobile_Deduction")]
        public int? MobileDeduction { get; set; }

        [Column("Updet_Dep_Loc_Exl")]
        public int? UpdetDepLocExl { get; set; }

        [Column("Other_Deduction")]
        public int? OtherDeduction { get; set; }

        [Column("Penalties_Deduction")]
        public int? PenaltiesDeduction { get; set; }

        [Column("Vacation_Due_allowance")]
        public int? VacationDueAllowance { get; set; }

        [Column("Leave_Benefits_allowance")]
        public int? LeaveBenefitsAllowance { get; set; }

        [Column("Ticket_allowance")]
        public int? TicketAllowance { get; set; }

        [Column("Leave_Deduction")]
        public int? LeaveDeduction { get; set; }

        [Column("Apply_Shortfall")]
        public int? ApplyShortfall { get; set; }

        public int? ApplyingDisciplinaryShortfall { get; set; }
    }
}

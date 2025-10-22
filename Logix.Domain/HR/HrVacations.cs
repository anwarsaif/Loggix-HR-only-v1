using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Logix.Domain.HR
{

    [Table("HR_Vacations")]
    [Index("EmpId", "VacationTypeId", "VacationAccountDay", "VacationSdate", "VacationEdate", "IsSalary", "StatusId", Name = "Ind_Emp_ID")]
    [Index("IsDeleted", Name = "Ind_Isdel")]
    public class HrVacation : TraceEntity
    {
        [Key]
        [Column("Vacation_ID")]
        public long VacationId { get; set; }

        [Column("Emp_ID")]
        public long? EmpId { get; set; }

        [Column("Vacation_Type_Id")]
        public int? VacationTypeId { get; set; }

        [Column("Vacation_Account_Day", TypeName = "decimal(18, 2)")]
        public decimal? VacationAccountDay { get; set; }

        [Column("Vacation_SDate")]
        [StringLength(10)]
        [Unicode(false)]
        public string? VacationSdate { get; set; }

        [Column("Vacation_EDate")]
        [StringLength(10)]
        [Unicode(false)]
        public string? VacationEdate { get; set; }

        [Column("Vacation_RDate")]
        [StringLength(10)]
        [Unicode(false)]
        public string? VacationRdate { get; set; }

        /// <summary>
        /// نوع القرار
        /// </summary>
        [Column("HR_VDT_Id")]
        public int? HrVdtId { get; set; }

        [Column("Decision_No")]
        [StringLength(50)]
        public string? DecisionNo { get; set; }

        [Column("Decision_Date")]
        [StringLength(10)]
        [Unicode(false)]
        public string? DecisionDate { get; set; }

        [StringLength(4000)]
        public string? Note { get; set; }

        public int? FinancelYear { get; set; }

        public bool? Approve { get; set; }

        public bool? IsSalary { get; set; }

        [Column("Status_Id")]
        public int? StatusId { get; set; }

        public bool? NeedJoinRequest { get; set; }

        [Column("Location_ID")]
        public int? LocationId { get; set; }

        [Column("Shift_ID")]
        public int? ShiftId { get; set; }

        [Column("TimeTable_ID")]
        public int? TimeTableId { get; set; }

        [Column("Alternative_Emp_ID")]
        public long? AlternativeEmpId { get; set; }

        [Column("Vacations_Day_Type_ID")]
        public int? VacationsDayTypeId { get; set; }

        [Column("Application_ID")]
        public long? ApplicationId { get; set; }

        [Column("Trans_Type_Id")]
        public int? TransTypeId { get; set; }
    }
}

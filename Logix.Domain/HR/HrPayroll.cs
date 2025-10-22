using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Payroll")]
    public partial class HrPayroll : TraceEntity
    {
        [Key]
        [Column("MS_ID")]
        public long MsId { get; set; }

        [Column("MS_Code")]
        public long? MsCode { get; set; }

        [Column("MS_Date")]
        [StringLength(10)]
        public string? MsDate { get; set; }

        [Column("MS_Title")]
        [StringLength(4000)]
        public string? MsTitle { get; set; }

        [Column("MS_Month")]
        [StringLength(2)]
        public string? MsMonth { get; set; }

        [Column("MS_MothTxt")]
        [StringLength(500)]
        public string? MsMothTxt { get; set; }

        public int? FinancelYear { get; set; }

        public int? State { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? AuditBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? AuditOn { get; set; }

        [StringLength(10)]
        [Unicode(false)]
        public string? ApproveBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ApproveOn { get; set; }

        [Column("Facility_ID")]
        public int? FacilityId { get; set; }

        [Column("Payroll_Type_ID")]
        public int? PayrollTypeId { get; set; }

        [Column("Start_Date")]
        [StringLength(10)]
        public string? StartDate { get; set; }

        [Column("End_Date")]
        [StringLength(10)]
        public string? EndDate { get; set; }

        [Column("Payment_Date")]
        [StringLength(10)]
        public string? PaymentDate { get; set; }

        [Column("Due_Date")]
        [StringLength(10)]
        public string? DueDate { get; set; }

        [Column("App_ID")]
        public long? AppId { get; set; }

        [Column("Branch_ID")]
        public long? BranchId { get; set; }

        public bool? Posted { get; set; }
    }
}

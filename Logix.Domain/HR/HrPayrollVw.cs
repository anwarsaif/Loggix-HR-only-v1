using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrPayrollVw
    {
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
        public string? AuditBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? AuditOn { get; set; }
        [StringLength(10)]
        public string? ApproveBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ApproveOn { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        [Column("Facility_ID")]
        public int? FacilityId { get; set; }
        [Column("Status_Name")]
        [StringLength(50)]
        public string? StatusName { get; set; }
        [Column("Payroll_Type_ID")]
        public int? PayrollTypeId { get; set; }
        [Column("Type_Name")]
        [StringLength(50)]
        public string? TypeName { get; set; }
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
        [Column("Step_Name")]
        [StringLength(250)]
        public string? StepName { get; set; }
        [StringLength(50)]
        public string? Expr1 { get; set; }
        [Column("Application_Code")]
        public long? ApplicationCode { get; set; }
        [Column("Branch_ID")]
        public long? BranchId { get; set; }
        [Column("Type_Name2")]
        [StringLength(50)]
        public string? TypeName2 { get; set; }
        [Column("Status_Name2")]
        [StringLength(50)]
        public string? StatusName2 { get; set; }
        [Column("Dept_ID")]
        public int? DeptId { get; set; }
        public int? Location { get; set; }
    }
}

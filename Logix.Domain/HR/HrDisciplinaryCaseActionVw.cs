using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrDisciplinaryCaseActionVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Disciplinary_Case_ID")]
        public long? DisciplinaryCaseId { get; set; }
        [Column("Action_Type")]
        public int? ActionType { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Due_Date")]
        [StringLength(10)]
        public string? DueDate { get; set; }
        [Column("Status_ID")]
        public int? StatusId { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
        public long CreatedBy { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("Case_Name")]
        [StringLength(2500)]
        public string? CaseName { get; set; }
        [Column("Count_Rept")]
        public int? CountRept { get; set; }
        [Column("Deducted_Rate", TypeName = "decimal(18, 2)")]
        public decimal? DeductedRate { get; set; }
        [Column("Deducted_Amount", TypeName = "decimal(18, 2)")]
        public decimal? DeductedAmount { get; set; }
        [Column("Action_Name")]
        [StringLength(250)]
        public string? ActionName { get; set; }
        [StringLength(50)]
        public string? Email { get; set; }
        [Column("Manager_ID")]
        public long? ManagerId { get; set; }
        public int? Location { get; set; }
        [Column("Dept_ID")]
        public int? DeptId { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column("Visit_Schedule_D_ID")]
        public long? VisitScheduleDId { get; set; }
        [Column("Action_Name2")]
        [StringLength(250)]
        public string? ActionName2 { get; set; }
        [Column("Case_Name2")]
        public string? CaseName2 { get; set; }
        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }
        [Column("Type_ID")]
        public int? TypeId { get; set; }
    }
}

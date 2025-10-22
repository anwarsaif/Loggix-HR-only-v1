using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrAttCheckShiftEmployeeVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Shit_ID")]
        public long? ShitId { get; set; }
        [Column("Begin_Date")]
        [StringLength(10)]
        public string? BeginDate { get; set; }
        [Column("End_Date")]
        [StringLength(10)]
        public string? EndDate { get; set; }
        public bool IsDeleted { get; set; }
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [StringLength(50)]
        public string? Name { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column("Job_Catagories_ID")]
        public int? JobCatagoriesId { get; set; }
        [Column("Status_ID")]
        public int? StatusId { get; set; }
        [Column("ISDEL")]
        public bool? Isdel { get; set; }
        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }
        public int? Location { get; set; }
        [Column("Dept_ID")]
        public int? DeptId { get; set; }
    }
}

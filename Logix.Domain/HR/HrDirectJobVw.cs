using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public  class HrDirectJobVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("date1")]
        [StringLength(10)]
        public string? Date1 { get; set; }
        [Column("Date_Direct")]
        [StringLength(10)]
        public string? DateDirect { get; set; }
        public string? Note { get; set; }
        public bool IsDeleted { get; set; }
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [Column("Type_ID")]
        public int? TypeId { get; set; }
        [Column("Type_Name")]
        [StringLength(250)]
        public string? TypeName { get; set; }
        [Column("Vacation_ID")]
        public long? VacationId { get; set; }
        [Column("Vacation_Type_Name")]
        [StringLength(500)]
        public string? VacationTypeName { get; set; }
        [Column("Vacation_SDate")]
        [StringLength(10)]
        [Unicode(false)]
        public string? VacationSdate { get; set; }
        [Column("Vacation_EDate")]
        [StringLength(10)]
        [Unicode(false)]
        public string? VacationEdate { get; set; }
        [Column("Vacation_Account_Day")]
        public decimal? VacationAccountDay { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column("BRA_NAME")]
        public string? BraName { get; set; }
        [Column("Dept_ID")]
        public int? DeptId { get; set; }
        public int? Location { get; set; }
        [Column("Dep_Name")]
        [StringLength(200)]
        public string? DepName { get; set; }
        [Column("Location_Name")]
        [StringLength(200)]
        public string? LocationName { get; set; }
        [Column("Dep_Name2")]
        [StringLength(200)]
        public string? DepName2 { get; set; }
        [Column("Location_Name2")]
        [StringLength(200)]
        public string? LocationName2 { get; set; }
        [Column("BRA_NAME2")]
        public string? BraName2 { get; set; }
        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }
    }
}

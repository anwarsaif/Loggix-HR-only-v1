using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
    [Keyless]

    public class HrCheckInOutVw
    {

        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("CHECKTIME", TypeName = "datetime")]
        public DateTime? Checktime { get; set; }
        [Column("CHECKTYPE")]
        public int? Checktype { get; set; }
        [Column(TypeName = "date")]
        public DateTime? CheckDate { get; set; }
        [StringLength(4000)]
        public string? CheckTimeStr { get; set; }

        [Column("Day_No")]
        public int? DayNo { get; set; }
        [Column("Day_Name")]
        [StringLength(50)]
        public string? DayName { get; set; }
        [Column("Day_Name2")]
        [StringLength(50)]
        public string? DayName2 { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        public int? Location { get; set; }
        public bool? IsSend { get; set; }
        [Column("ISDEL")]
        public bool? Isdel { get; set; }
        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }
        [Column("BRA_NAME")]
        public string? BraName { get; set; }
        [Column("BRA_NAME2")]
        public string? BraName2 { get; set; }
        [Column("Dep_Name")]
        [StringLength(200)]
        public string? DepName { get; set; }
        [Column("Dep_Name2")]
        [StringLength(200)]
        public string? DepName2 { get; set; }
        [Column("Dept_ID")]
        public int? DeptId { get; set; }
        [Column("Location_Name")]
        [StringLength(200)]
        public string? LocationName { get; set; }
        [Column("Location_Name2")]
        [StringLength(200)]
        public string? LocationName2 { get; set; }
        [Column("CHECKTYPEName")]
        [StringLength(9)]
        public string Checktypename { get; set; } = null!;
        [Column("CHECKTYPEName2")]
        [StringLength(9)]
        public string Checktypename2 { get; set; } = null!;
    }
}

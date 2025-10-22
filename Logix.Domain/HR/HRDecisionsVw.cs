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

    public  class HrDecisionsVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Dec_Date")]
        [StringLength(10)]
        public string? DecDate { get; set; }
        [Column("Dec_Type")]
        public int? DecType { get; set; }
        [Column("Dec_Code")]
        public long? DecCode { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        public string? Note { get; set; }
        [Column("Refrance_Code")]
        [StringLength(50)]
        public string? RefranceCode { get; set; }
        [Column("Refrance_Date")]
        [StringLength(10)]
        public string? RefranceDate { get; set; }
        public bool IsDeleted { get; set; }
        [Column("App_ID")]
        public long? AppId { get; set; }
        [Column("File_URL")]
        public string? FileUrl { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string? EmpCode { get; set; }
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("Dec_Type_Name")]
        [StringLength(250)]
        public string? DecTypeName { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column("Dep_Name")]
        [StringLength(200)]
        public string? DepName { get; set; }
        [Column("Location_Name")]
        [StringLength(200)]
        public string? LocationName { get; set; }
        [Column("Dept_ID")]
        public int? DeptId { get; set; }
        [Column("BRA_NAME")]
        public string? BraName { get; set; }
        public int? Location { get; set; }
        [Column("Cat_name")]
        [StringLength(250)]
        public string? CatName { get; set; }
        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }
        [Column("Dec_Type_Name2")]
        [StringLength(250)]
        public string? DecTypeName2 { get; set; }
        [Column("Dep_Name2")]
        [StringLength(200)]
        public string? DepName2 { get; set; }
        [Column("Location_Name2")]
        [StringLength(200)]
        public string? LocationName2 { get; set; }
        [Column("BRA_NAME2")]
        public string? BraName2 { get; set; }
    }
}

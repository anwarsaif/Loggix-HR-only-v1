using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrTransfersVw
    {
        [Column("ID")]
        public long Id { get; set; }

        [Column("Emp_ID")]
        public long? EmpId { get; set; }

        [Column("Resolution_No")]
        [StringLength(50)]
        public string? ResolutionNo { get; set; }

        [Column("Trans_Location_From")]
        public int? TransLocationFrom { get; set; }

        [Column("Trans_Location_To")]
        public int? TransLocationTo { get; set; }

        [Column("Trans_Department_From")]
        public int? TransDepartmentFrom { get; set; }

        [Column("Trans_Department_To")]
        public int? TransDepartmentTo { get; set; }

        public string? Note { get; set; }

        [Column("Transfer_Date")]
        [StringLength(10)]
        public string? TransferDate { get; set; }

        public bool? IsDeleted { get; set; }

        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;

        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }

        [Column("LocationFrom_name")]
        [StringLength(200)]
        public string LocationFromName { get; set; } = null!;

        [Column("LocationTo_Name")]
        [StringLength(200)]
        public string LocationToName { get; set; } = null!;

        public int? Location { get; set; }

        [Column("Dept_ID")]
        public int? DeptId { get; set; }

        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }

        [Column("BRANCH_ID_To")]
        public int? BranchIdTo { get; set; }

        [Column("BRANCHFrom_Name")]
        public string? BranchfromName { get; set; }

        [Column("BRANCHTo_Name")]
        public string? BranchtoName { get; set; }

        [Column("ID_No")]
        [StringLength(50)]
        public string? IdNo { get; set; }
    }
}

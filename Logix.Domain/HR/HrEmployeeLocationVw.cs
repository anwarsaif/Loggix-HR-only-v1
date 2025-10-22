using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrEmployeeLocationVw
    {
        [Column("GPSLocation_name")]
        [StringLength(250)]
        public string? GpslocationName { get; set; }
        [Column("Emp_ID")]
        [StringLength(50)]
        public string EmpId { get; set; } = null!;
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("ISDEL")]
        public bool? Isdel { get; set; }
        [Column("USER_ID")]
        public long? UserId { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column("Job_Catagories_ID")]
        public int? JobCatagoriesId { get; set; }
        [Column("Status_ID")]
        public int? StatusId { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("IBAN")]
        [StringLength(50)]
        public string? Iban { get; set; }
        [Column("Dept_ID")]
        public int? DeptId { get; set; }
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }
        [Column("Emp_ID_Int")]
        public long? EmpIdInt { get; set; }
        public int? Location { get; set; }
        [Column("Location_ID")]
        public long? LocationId { get; set; }
    }

}

using Logix.Domain.Base;
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
    public partial class HrAssignmenVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [StringLength(10)]
        public string? AssignmentDate { get; set; }
        [Column("Type_ID")]
        public long? TypeId { get; set; }
        [StringLength(10)]
        public string? FromDate { get; set; }
        [StringLength(10)]
        public string? ToDate { get; set; }
        public string? Note { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("EMP_ID")]
        public long? EmpId { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("Facility_ID")]
        public int? FacilityId { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        public int? Location { get; set; }
        [Column("Type_Name")]
        [StringLength(250)]
        public string? TypeName { get; set; }
    }
}

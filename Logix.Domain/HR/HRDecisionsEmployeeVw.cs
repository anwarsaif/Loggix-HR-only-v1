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

    public class HrDecisionsEmployeeVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Decisions_ID")]
        public long? DecisionsId { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("ISDEL")]
        public bool? Isdel { get; set; }
        [Column("USER_ID")]
        public long? UserId { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column("Job_Type")]
        public int? JobType { get; set; }
        [Column("Dept_ID")]
        public int? DeptId { get; set; }
        public int? Location { get; set; }
        public string? Note { get; set; }
        [Column("Dec_Date")]
        [StringLength(10)]
        public string? DecDate { get; set; }
        [Column("Dec_Code")]
        public long? DecCode { get; set; }
        [Column("Cat_name")]
        [StringLength(250)]
        public string? CatName { get; set; }
        public bool? DecisionSigning { get; set; }
        public string? Signature { get; set; }
        [Column("USER_FULLNAME")]
        [StringLength(50)]
        public string? UserFullname { get; set; }
    }
}

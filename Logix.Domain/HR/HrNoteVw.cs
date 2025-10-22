using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrNoteVw
    {
        [Column("NoteID")]
        public long NoteId { get; set; }
        [Column("Emp_Id")]
        public long? EmpId { get; set; }
        [StringLength(4000)]
        public string? NoteText { get; set; }
        [StringLength(50)]
        public string? NoteDate { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
    }
}

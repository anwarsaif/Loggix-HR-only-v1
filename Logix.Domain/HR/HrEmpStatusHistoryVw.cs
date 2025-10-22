using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrEmpStatusHistoryVw
    {
        [Column("ID")]
        public long Id { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("Status_ID")]
        public int? StatusId { get; set; }
        public string? Note { get; set; }
        [Column("Status_ID_Old")]
        public int? StatusIdOld { get; set; }
        [Column("Status_Old_Name")]
        [StringLength(250)]
        public string? StatusOldName { get; set; }
        [Column("Status_New_Name")]
        [StringLength(250)]
        public string? StatusNewName { get; set; }
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("USER_FULLNAME")]
        [StringLength(50)]
        public string? UserFullname { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column("Emp_ID")]
        [StringLength(50)]
        public string EmpId { get; set; } = null!;
        [Column("Catagories_ID")]
        public int? CatagoriesId { get; set; }
        public int? Expr2 { get; set; }
    }
}

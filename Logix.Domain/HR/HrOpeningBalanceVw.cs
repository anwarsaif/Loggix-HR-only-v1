using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrOpeningBalanceVw
    {
        [Column("Type_Name")]
        [StringLength(50)]
        public string? TypeName { get; set; }
        [Column("Type_Name2")]
        [StringLength(50)]
        public string? TypeName2 { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Start_Date")]
        [StringLength(10)]
        public string? StartDate { get; set; }
        [Column("Type_ID")]
        public long? TypeId { get; set; }
        [Column("OB_Value", TypeName = "decimal(18, 2)")]
        public decimal? ObValue { get; set; }
        public long CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }
    }
}

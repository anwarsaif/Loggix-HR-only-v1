using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Fixing_Employee_Salary")]
    public partial class HrFixingEmployeeSalary
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Facility_ID")]
        public long? FacilityId { get; set; }
        [Column("Branch_ID")]
        public long? BranchId { get; set; }
        [Column("Emp_ID")]
        public int? EmpId { get; set; }
        [Column("Fixing_Type")]
        public int? FixingType { get; set; }
        [Column("The_Fixing_SentTo")]
        [StringLength(50)]
        public string? TheFixingSentTo { get; set; }
        [Column("Fixing_Date")]
        [StringLength(10)]
        public string? FixingDate { get; set; }
        public int? Status { get; set; }
        public string? Notes { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
}

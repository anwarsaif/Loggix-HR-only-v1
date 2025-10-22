using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrVisaVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Visa_Type")]
        public int? VisaType { get; set; }
        [Column("Visa_Date")]
        [StringLength(10)]
        public string? VisaDate { get; set; }
        [Column("Place_of_visit")]
        [StringLength(250)]
        public string? PlaceOfVisit { get; set; }
        [Column("Start_date")]
        [StringLength(10)]
        public string? StartDate { get; set; }
        [Column("End_Date")]
        [StringLength(10)]
        public string? EndDate { get; set; }
        [Column("Visa_days")]
        public int? VisaDays { get; set; }
        public string? Purpose { get; set; }
        [Column("Is_billable")]
        public int? IsBillable { get; set; }
        public string? Note { get; set; }
        public bool IsDeleted { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("Visa_Type_Type")]
        [StringLength(250)]
        public string? VisaTypeType { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column("Dept_ID")]
        public int? DeptId { get; set; }
        public int? Location { get; set; }
    }
}

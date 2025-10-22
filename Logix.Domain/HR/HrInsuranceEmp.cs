using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Logix.Domain.HR
{
    [Table("HR_Insurance_Emp")]
    public partial class HrInsuranceEmp
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Insurance_ID")]
        public long? InsuranceId { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("To_Dependents")]
        public bool? ToDependents { get; set; }
        [Column("Dependent_ID")]
        public long? DependentId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }
        public string? Note { get; set; }
        [Column("Class_ID")]
        public int? ClassId { get; set; }
        public long CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        [Column("Refrance_Ins_Emp_ID")]
        public long? RefranceInsEmpId { get; set; }
        [Column("Insurance_Card_No")]
        [StringLength(50)]
        public string? InsuranceCardNo { get; set; }
    }
}

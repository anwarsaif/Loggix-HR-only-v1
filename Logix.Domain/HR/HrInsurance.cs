using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Logix.Domain.HR
{
    [Table("HR_Insurance")]
    public partial class HrInsurance
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        public long? Code { get; set; }
        [Column("Trans_Type_ID")]
        public int? TransTypeId { get; set; }
        [Column("Insurance_Type")]
        public int? InsuranceType { get; set; }
        [Column("Policy_ID")]
        public long? PolicyId { get; set; }
        [Column("Insurance_Date")]
        [StringLength(10)]
        public string? InsuranceDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Total { get; set; }
        public string? Note { get; set; }
        [Column("App_ID")]
        public long? AppId { get; set; }
        [Column("Wf_Status_ID")]
        public int? WfStatusId { get; set; }
        public long CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
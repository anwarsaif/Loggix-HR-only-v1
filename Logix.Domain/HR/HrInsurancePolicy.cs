using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Logix.Domain.HR
{
    [Table("HR_Insurance_Policy")]
    public partial class HrInsurancePolicy
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [StringLength(250)]
        public string? Code { get; set; }
        [StringLength(250)]
        public string? Name { get; set; }
        [Column("Start_Date")]
        [StringLength(10)]
        public string? StartDate { get; set; }
        [Column("End_Date")]
        [StringLength(10)]
        public string? EndDate { get; set; }
        [Column("Company_ID")]
        public long? CompanyId { get; set; }
        [Column("Facility_ID")]
        public long? FacilityId { get; set; }
        public string? Note { get; set; }
        public long CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
    }
}
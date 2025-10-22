using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrCompetencesCatagoryDto
    {
        public long Id { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class HrCompetencesCatagoryEditDto
    {
        public long Id { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

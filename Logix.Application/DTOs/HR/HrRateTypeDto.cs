using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrRateTypeDto
    {
        public long Id { get; set; }
        [StringLength(50)]
        public string? RateName { get; set; }
        [StringLength(50)]
        public string? RateName2 { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class HrRateTypeEditDto
    {
        public long Id { get; set; }
        [StringLength(50)]
        public string? RateName { get; set; }
        [StringLength(50)]
        public string? RateName2 { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }

}

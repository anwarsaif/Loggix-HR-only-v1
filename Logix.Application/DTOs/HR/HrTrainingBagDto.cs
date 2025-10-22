using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrTrainingBagDto
    {
        public long Id { get; set; }

        public int? TypeId { get; set; }
        [StringLength(2500)]
        public string? Name { get; set; }
        [StringLength(2500)]
        public string? Name2 { get; set; }

        public bool? IsActive { get; set; }

        public string? Note { get; set; }

        public string? FileUrl { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class HrTrainingBagEditDto
    {
        public long Id { get; set; }

        public int? TypeId { get; set; }
        [StringLength(2500)]
        public string? Name { get; set; }
        [StringLength(2500)]
        public string? Name2 { get; set; }

        public bool? IsActive { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public string? Note { get; set; }

        public string? FileUrl { get; set; }
    }

    public class HrTrainingBagFilterDto
    {
        public int? TypeId { get; set; }
        public string? Name { get; set; }
    }


}

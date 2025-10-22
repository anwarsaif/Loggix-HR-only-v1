using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrJobDescriptionDto
    {
        public long Id { get; set; }
        [Required]
        public long? JobCatId { get; set; }
        public string? JobTitle { get; set; }
        [Required]
        public string? JobDescription { get; set; }
        public string? FileUrl { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class HrJobDescriptionEditDto
    {
        public long Id { get; set; }
        [Required]
        public long? JobCatId { get; set; }
        public string? JobTitle { get; set; }
        [Required]
        public string? JobDescription { get; set; }
        public string? FileUrl { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

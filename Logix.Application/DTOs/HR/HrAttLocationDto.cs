using System.ComponentModel.DataAnnotations;


namespace Logix.Application.DTOs.HR
{
    public class HrAttLocationDto
    {
        public long? Id { get; set; }
        [Required]
        public string? LocationName { get; set; }

        [Required]
        public string? Latitude { get; set; }

        [Required]
        public string? Longitude { get; set; }
        public long? ProjectId { get; set; }
        public bool? IsDeleted { get; set; }

    }


    public class HrAttLocationEditDto
    {
        public long Id { get; set; }
        [Required]

        public string? LocationName { get; set; }

        [Required]
        public string? Latitude { get; set; }

        [Required]
        public string? Longitude { get; set; }
        public long? ProjectId { get; set; }

        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }
}

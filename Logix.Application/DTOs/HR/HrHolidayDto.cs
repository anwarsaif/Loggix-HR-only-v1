using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrHolidayDto
    {
        public long HolidayId { get; set; }
        [Required]
        public string? HolidayName { get; set; }
        [Required]
        public string? HolidayDateFrom { get; set; }
        [Required]

        public string? HolidayDateTo { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public long? FacilityId { get; set; }
    }
    public class HrHolidayEditDto
    {
        public long HolidayId { get; set; }
        [Required]
        public string? HolidayName { get; set; }
        [Required]

        public string? HolidayDateFrom { get; set; }
        [Required]
        public string? HolidayDateTo { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? FacilityId { get; set; }
    }

    public class HrHolidayFilterDto
    {
        public long? HolidayId { get; set; }

        public string? HolidayName { get; set; }

        public string? HolidayDateFrom { get; set; }
        public string? HolidayDateTo { get; set; }
    }
}

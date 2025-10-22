using Logix.Domain.HR;

namespace Logix.Application.DTOs.HR
{
    public class HrAttShiftDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? BeginDate { get; set; }
        public string? EndDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public int? OffDays { get; set; }
        public List<int?> Shift { get; set; }

    }

    public class HrAttShiftEditDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? BeginDate { get; set; }
        public string? EndDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? OffDays { get; set; }
        public List<int>? Shift { get; set; }
    }

    public class HrAttShiftFilterDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? BeginDate { get; set; }
        public string? EndDate { get; set; }
        public int? OffDays { get; set; }
    }
    public class HrAttShiftEdit2Dto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? BeginDate { get; set; }
        public string? EndDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public int? OffDays { get; set; }
        public int? Shift { get; set; }
        public List<HrAttTimeTableVw>? timeTableVws { get; set; }
    }
}

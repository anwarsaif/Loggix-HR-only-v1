namespace Logix.Application.DTOs.HR
{
    public class HrAttTimeTableDayDto
    {
        public long Id { get; set; }
        public int? DayNo { get; set; }
        public long? TimeTableId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class HrAttTimeTableDayEditDto
    {
        public long Id { get; set; }
        public int? DayNo { get; set; }
        public long? TimeTableId { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

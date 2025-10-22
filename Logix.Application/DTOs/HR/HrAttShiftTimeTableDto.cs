namespace Logix.Application.DTOs.HR
{
    public class HrAttShiftTimeTableDto
    {
        public long Id { get; set; }
        public long? ShiftId { get; set; }
        public long? TimeTableId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
    } 
    public class HrAttShiftTimeTableEditDto
    {
        public long Id { get; set; }
        public long? ShiftId { get; set; }
        public long? TimeTableId { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }

}

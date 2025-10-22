namespace Logix.Application.DTOs.HR
{
    public class HrPayrollNoteDto
    {
        public long Id { get; set; }
        public long? MsId { get; set; }
        public int? StateId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string? Note { get; set; }
    }

}

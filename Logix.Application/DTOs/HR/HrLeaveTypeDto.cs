namespace Logix.Application.DTOs.HR
{
    public class HrLeaveTypeDto
    {
        public int TypeId { get; set; }
        public string? TypeName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? ParentId { get; set; }
        public string? TypeName2 { get; set; }
    }

}

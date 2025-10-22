namespace Logix.Application.DTOs.HR
{
    public partial class HrSkillDto
    {
        public long? Id { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public int? Skill { get; set; }
        public string? YearExperience { get; set; }
        public string? Comment { get; set; }

        public bool? IsDeleted { get; set; }
    }
    public partial class HrSkillEditDto
    {
        public long Id { get; set; }
        public long? EmpId { get; set; }
        public int? Skill { get; set; }
        public string? YearExperience { get; set; }
        public string? Comment { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }

}

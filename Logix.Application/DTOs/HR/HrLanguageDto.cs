namespace Logix.Application.DTOs.HR
{
    public partial class HrLanguageDto
    {
        public long? Id { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public int? Language { get; set; }
        public int? SkillLang { get; set; }
        public int? FluencyLevel { get; set; }
        public string? Comment { get; set; }

        public bool? IsDeleted { get; set; }
    }
    public partial class HrLanguageEditDto
    {
        public long Id { get; set; }
        public long? EmpId { get; set; }
        public int? Language { get; set; }
        public int? SkillLang { get; set; }
        public int? FluencyLevel { get; set; }
        public string? Comment { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

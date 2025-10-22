namespace Logix.Application.DTOs.HR
{
    public partial class HrEducationDto
    {
        public long Id { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public int? LevelEdu { get; set; }
        public string? Institute { get; set; }
        public string? Specialization { get; set; }
        public string? YearEdu { get; set; }
        public string? Score { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }


        public bool? IsDeleted { get; set; }
    }
    public partial class HrEducationEditDto
    {
        public long Id { get; set; }
        public long? EmpId { get; set; }
        public int? LevelEdu { get; set; }
        public string? Institute { get; set; }
        public string? Specialization { get; set; }
        public string? YearEdu { get; set; }
        public string? Score { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

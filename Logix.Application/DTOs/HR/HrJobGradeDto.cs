

namespace Logix.Application.DTOs.HR
{
    public class HrJobGradeDto
    {
        public long Id { get; set; }
        public string? GradeName { get; set; }
        public string? GradeNo { get; set; }
        public long? LevelId { get; set; }
        public decimal? Salary { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class HrJobGradeEditDto
    {
        public long Id { get; set; }
        public string? GradeName { get; set; }
        public string? GradeNo { get; set; }
        public long? LevelId { get; set; }
        public decimal? Salary { get; set; }
    }

    public class HrJobGradeFilterDto
    {
        public long Id { get; set; }
        public string? GradeName { get; set; }
        public string? GradeNo { get; set; }
        public long? LevelId { get; set; }
        public string? LevelName { get; set; }
        public decimal? Salary { get; set; }
    }
}

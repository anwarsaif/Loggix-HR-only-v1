using Logix.Domain.HR;

namespace Logix.Application.DTOs.HR
{
    public class HrKpiDto
    {
        public long? Id { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public int? KpiTemId { get; set; }
        public string? EvaDate { get; set; }
        public int? StatusId { get; set; }
        public long? CreatedBy { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public long? AppId { get; set; }
        public long? PerformanceId { get; set; }
        public string? Achievements { get; set; }
        public string? StrengthsPoints { get; set; }
        public string? WeaknessesPoints { get; set; }
        public string? SuggestedTraining { get; set; }
        public string? Recommendations { get; set; }
        public int? TypeId { get; set; }
        public decimal? FinalRating { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? Note { get; set; }
        public long? ProbationResult { get; set; }
        public decimal? FinalRatingKpi { get; set; }
        public decimal? FinalRatingCompetences { get; set; }
        public List<HrKpiTemplatesCompetenceDto> Compatence { get; set; }
        public List<HrKpiRateDto> RatingKPI { get; set; }
        public int? AppTypeId { get; set; } = 0;

    }

    public class HrKpiEditDto
    {
        public long Id { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        public int? KpiTemId { get; set; }
        public string? EvaDate { get; set; }
        public int? StatusId { get; set; }
        public long? PerformanceId { get; set; }
        public string? Achievements { get; set; }
        public string? StrengthsPoints { get; set; }
        public string? WeaknessesPoints { get; set; }
        public string? SuggestedTraining { get; set; }
        public string? Recommendations { get; set; }
        public int? TypeId { get; set; }
        public decimal? FinalRating { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? Note { get; set; }
        public long? ProbationResult { get; set; }
        public decimal? FinalRatingKpi { get; set; }
        public decimal? FinalRatingCompetences { get; set; }

        public List<HrKpiDetaileDto>? Compatence { get; set; }
        //public List<HrKpiDetaileEditDto>? RatingKPI { get; set; }
        //public List<HrKpiTemplatesCompetenceDto> Compatence { get; set; }
        public List<HrKpiRateDto> RatingKPI { get; set; }
    }
    public class HRKpiFilterDto
    {
        public long? Id { get; set; }

        public string? EmpName { get; set; }
        public string? EmpCode { get; set; }
        public int? DeptId { get; set; }
        public int? BranchId { get; set; }
        public int? Location { get; set; }
        //  طلب التقييم
        public int? PerformanceId { get; set; }

        public string? FinancialYear { get; set; }
        public int? Month { get; set; }
        //  حالة التقييم
        public int? EvaluationStatus { get; set; }
        /////////////////////////////////////////////////////////////////////////////
        public string? BranchName { get; set; }
        public string? EvaDate { get; set; }
        public string? TemplateName { get; set; }
        public decimal? TotalDegree { get; set; }
        public string? StatusName { get; set; }

    }

    public class HrKpiRateDto
    {
        public long Id { get; set; }

        public int? TemplateId { get; set; }
         //المستهدف    
        public decimal? Target { get; set; }
        // المحقق من المستهدف
        public decimal? ActualTarget { get; set; }
        public decimal? UnitRate { get; set; }
        public decimal? DueDegree { get; set; }
        public decimal? Score { get; set; }
        public decimal? Weight { get; set; }
        public int? MethodId { get; set; }
        public long? CompetencesId { get; set; }

    }
}

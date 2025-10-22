namespace Logix.Application.DTOs.HR
{
    public class HrEmpGoalIndicatorDto
    {
        public long Id { get; set; }
        public long? PeriodId { get; set; }
        public long? KpiTemplatesId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class HrEmpGoalIndicatorEditDto
    {
        public long Id { get; set; }
        public long? PeriodId { get; set; }
        public long? KpiTemplatesId { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
    public class EmployeeGoalIndicatorFilterDto
    {
        public string? EvaluationPeriod { get; set; }
        public string? KpiTemplateName { get; set; }
        public long? Id { get; set; }
        public string? GoalName { get; set; }
        public decimal? Weight { get; set; }
        public decimal? Degree { get; set; }
        public decimal? Score { get; set; }
        public int? PeriodId { get; set; }
        public int? KpiTemplatesId { get; set; }
    }

    public class HrEmpGoalIndicatorAddDto
    {
        public long? Id { get; set; }
        public long? PeriodId { get; set; }
        public long? KpiTemplatesId { get; set; }
        public List<GoalIndicatorsEmployeeDto> Employee { get; set; }
        public List<GoalIndicatorsCompetenceDto> Competence { get; set; }
    }
    public class GoalIndicatorsEmployeeDto
    {
        public long? Id { get; set; }

        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public long? TemplateId { get; set; }
        public bool IsDeleted { get; set; }

    }
    public class GoalIndicatorsCompetenceDto
    {
        public long CompetencesId { get; set; }
        public int? Weight { get; set; }
        public int? Target { get; set; } 
        public string? Note { get; set; }
        public long? Id { get; set; }
        public bool IsDeleted { get; set; }


    }


}

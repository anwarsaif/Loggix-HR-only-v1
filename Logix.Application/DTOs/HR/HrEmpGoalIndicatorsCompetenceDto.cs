namespace Logix.Application.DTOs.HR
{
    public class HrEmpGoalIndicatorsCompetenceDto
    {
        public long Id { get; set; }
        public long CompetencesId { get; set; }
        public long? GoalIndicatorsId { get; set; }
        public string? Weight { get; set; }
        public string? Target { get; set; }
        public string? Note { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
    public class HrEmpGoalIndicatorsCompetenceEditDto
    {
        public long Id { get; set; }
        public long CompetencesId { get; set; }
        public long? GoalIndicatorsId { get; set; }
        public string? Weight { get; set; }
        public string? Target { get; set; }
        public string? Note { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }

    public class HrEmpGoalIndicatorsCompetenceResultDto
    {
        public long Id { get; set; }
        public long CompetencesId { get; set; }
        public long? GoalIndicatorsId { get; set; }
        public string? Weight { get; set; }
        public string? Target { get; set; }
        public string? Note { get; set; }
        public string? CompetencesName { get; set; }


    }

}

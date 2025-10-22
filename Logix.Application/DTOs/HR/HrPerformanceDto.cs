namespace Logix.Application.DTOs.HR
{
    public class HrPerformanceDto
    {
        public long? Id { get; set; }
        public string? Description { get; set; }
        public long? TypeId { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? DueDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public long? StatusId { get; set; }
        public string? GroupsId { get; set; }
        public long? EvaluationFor { get; set; }
        public long? FacilityId { get; set; }
        public string? DepartmentsIds { get; set; }
        public string? LocationIds { get; set; }
        public string? JobsCatIds { get; set; }
    }
    public class HrPerformanceEditDto
    {
        public long Id { get; set; }
        public string? Description { get; set; }
        public long? TypeId { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }
        public string? DueDate { get; set; }

        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? StatusId { get; set; }
        public string? GroupsId { get; set; }
        public long? EvaluationFor { get; set; }
        public long? FacilityId { get; set; }
        public string? DepartmentsIds { get; set; }
        public string? LocationIds { get; set; }
        public string? JobsCatIds { get; set; }
    }

    public class HrPerformanceFilterDto
    {
        public long? TypeId { get; set; }
        public long? StatusId { get; set; }

        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        //////////////////////////////////////
        public long? Id { get; set; }
        public int? EvaluatedCount { get; set; }
        public string? Description { get; set; }
        public string? EvaluationName { get; set; }
        public string? EvaluationName2 { get; set; }
        public string? StartDate { get; set; }
        public string? EndDate { get; set; }

        public string? DueDate { get; set; }
        public string? StatusName { get; set; }
        public string? StatusName2 { get; set; }

        public string? TypeName { get; set; }
        public string? TypeName2 { get; set; }

        public string? GroupsId { get; set; }
        public long? EvaluationFor { get; set; }
        public string? DepartmentsIds { get; set; }
        public string? LocationIds { get; set; }
        public string? JobsCatIds { get; set; }
    }
}

namespace Logix.Application.DTOs.HR
{
    public class HrRecruitmentCandidateKpiDDto
    {
        public long Id { get; set; }
        public long? KpiId { get; set; }
        public string? KpiTemComId { get; set; }
        public decimal? Degree { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }

    }

    public class HrRecruitmentCandidateKpiDEditDto
    {
        public long Id { get; set; }
        public long? KpiId { get; set; }
        public string? KpiTemComId { get; set; }
        public decimal? Degree { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}

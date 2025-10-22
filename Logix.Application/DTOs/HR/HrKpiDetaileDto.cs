namespace Logix.Application.DTOs.HR
{
    public class HrKpiDetaileDto
    {
        public long Id { get; set; }
        public long? KpiId { get; set; }
        public string? KpiTemComId { get; set; }
        public decimal? Degree { get; set; }
        public bool? IsDeleted { get; set; }
        public decimal? ActualTarget { get; set; }
        public decimal? Target { get; set; }
        public decimal? Weight { get; set; }

    }
    public class HrKpiDetaileEditDto
    {
        public long Id { get; set; }
        public long? KpiId { get; set; }
        public string? KpiTemComId { get; set; }
        public decimal? Degree { get; set; }
        public decimal? ActualTarget { get; set; }
        public decimal? Target { get; set; }
        public decimal? Weight { get; set; }
        public decimal? DueDegree { get; set; }
    }
}

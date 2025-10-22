
using System.ComponentModel.DataAnnotations.Schema;

namespace Logix.Application.DTOs.HR
{
    public class HrJobLevelDto
    {
        public long Id { get; set; }
        public string? LevelName { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        public decimal? AnnualIncrease { get; set; }
        public int? MedInsuranceType { get; set; }
        public decimal? Mandate { get; set; }
        public int? TicketType { get; set; }
        public decimal? TicketValue { get; set; }
        public int? TicketCount { get; set; }
        public int? VacationYear { get; set; }
        public int? VacationNecessity { get; set; }
        public bool? IsDeleted { get; set; }
        public decimal? MandateOut { get; set; }
        public decimal? RatePerNight { get; set; }
        public decimal? TransportAmount { get; set; }
        [Column(TypeName = "decimal(4, 2)")]
        public decimal? DurationStay { get; set; }
        public int? GroupId { get; set; }
    }
    public class HrJobLevelEditDto
    {
        public long Id { get; set; }
        public string? LevelName { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        public decimal? AnnualIncrease { get; set; }
        public int? MedInsuranceType { get; set; }
        public decimal? Mandate { get; set; }
        public int? TicketType { get; set; }
        public decimal? TicketValue { get; set; }
        public int? TicketCount { get; set; }
        public int? VacationYear { get; set; }
        public int? VacationNecessity { get; set; }
        public decimal? MandateOut { get; set; }
        public decimal? RatePerNight { get; set; }
        public decimal? TransportAmount { get; set; }
        [Column(TypeName = "decimal(4, 2)")]
        public decimal? DurationStay { get; set; }

        public int? GroupId { get; set; }
    }


    public class HrJobLevelFilterDto
    {
        public long Id { get; set; }
        public string? LevelName { get; set; }
        public decimal? MinSalary { get; set; }
        public decimal? MaxSalary { get; set; }
        public decimal? RatePerNight { get; set; }
        public decimal? TransportAmount { get; set; }
		public int? GroupId { get; set; }
	}

}

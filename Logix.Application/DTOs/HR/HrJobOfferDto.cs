namespace Logix.Application.DTOs.HR
{
    public class HrJobOfferDto
    {
        public long? Id { get; set; }
        public int? ApplicantId { get; set; }
        public int? RecruApplicantId { get; set; }
        public int? JobCatId { get; set; }
        public int? ShiftId { get; set; }
        public int? TypeDurationExperimentId { get; set; }
        public int? DurationExperiment { get; set; }
        public decimal? BasicSalary { get; set; }
        public decimal? TotalSalary { get; set; }
        public decimal? NetSalary { get; set; }
        public decimal? MedicalInsurance { get; set; }
        public bool? IsFamilyMedicalInsurance { get; set; }
        public decimal? FlightTickets { get; set; }
        public bool? IsFamilyFlightTickets { get; set; }
        public bool IsDeleted { get; set; }
        public int? TrialType { get; set; }
        public int? TrialCount { get; set; }
        public int? ContractTypeId { get; set; }
        public string? FileUrl { get; set; }
        public decimal? HousingAllowance { get; set; }
        public decimal? TransportAllowance { get; set; }
        public decimal? OtherAllowance { get; set; }
        public string? AdvantagesList { get; set; }
    }
    public class HrJobOfferEditDto
    {
        public long Id { get; set; }
        public int? RecruApplicantId { get; set; }
        public int? JobCatId { get; set; }
        public int? ShiftId { get; set; }
        public int? TypeDurationExperimentId { get; set; }
        public int? DurationExperiment { get; set; }
        public decimal? BasicSalary { get; set; }
        public decimal? TotalSalary { get; set; }
        public decimal? NetSalary { get; set; }
        public decimal? MedicalInsurance { get; set; }
        public bool? IsFamilyMedicalInsurance { get; set; }
        public decimal? FlightTickets { get; set; }
        public bool? IsFamilyFlightTickets { get; set; }
        public bool IsDeleted { get; set; }
        public int? TrialType { get; set; }
        public int? TrialCount { get; set; }
        public int? ContractTypeId { get; set; }
        public string? FileUrl { get; set; }
        public decimal? HousingAllowance { get; set; }
        public decimal? TransportAllowance { get; set; }
        public decimal? OtherAllowance { get; set; }
    }

    public class HrJobOfferFilterDto
    {
        public long? Id { get; set; }
        public int? ApplicantId { get; set; }
        public string? ApplicantName { get; set; }
        public string? From { get; set; }
        public string? To { get; set; }
        ////////////////////////////////////

        public string? VacancyName { get; set; }
        public string? Date { get; set; }
        public decimal? TotalSalary { get; set; }
        public string? FileUrl { get; set; }

    }

}

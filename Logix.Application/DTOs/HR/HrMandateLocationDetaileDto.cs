namespace Logix.Application.DTOs.HR
{
    public partial class HrMandateLocationDetaileDto
    {
        public long? Id { get; set; }
        public long? MlId { get; set; }
        public long? JobLevelId { get; set; }
        public decimal? AllowanceValue { get; set; }
        public bool? HouseingIsSecured { get; set; }
        public decimal? RatePerNight { get; set; }
        public bool? TransportIsInsured { get; set; }
        public decimal? TransportAmount { get; set; }
        public decimal? TicketValue { get; set; }
        public string? Note { get; set; }
        public bool? IsDeleted { get; set; }
    }

    public partial class HrMandateLocationDetaileEditDto
    {
        public long Id { get; set; }
        public long? MlId { get; set; }
        public long? JobLevelId { get; set; }
        public decimal? AllowanceValue { get; set; }
        public bool? HouseingIsSecured { get; set; }
        public decimal? RatePerNight { get; set; }
        public bool? TransportIsInsured { get; set; }
        public decimal? TransportAmount { get; set; }
        public decimal? TicketValue { get; set; }
        public string? Note { get; set; }
        public bool? IsDeleted { get; set; }
    }


}

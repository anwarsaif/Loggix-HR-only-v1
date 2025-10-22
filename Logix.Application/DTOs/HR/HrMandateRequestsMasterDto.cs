namespace Logix.Application.DTOs.HR
{
    public class HrMandateRequestsMasterDto
    {
        public long? Id { get; set; }
        public string? Name { get; set; }
        public string? Name2 { get; set; }
        public long? CountryClassificationId { get; set; }
        public string? Note { get; set; }
        public long? TypeId { get; set; }
        public string? FromLocation { get; set; }
        public string? ToLocation { get; set; }
        public long? FacilityId { get; set; }
        public long? MandateLocationId { get; set; }
        public long? AppId { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string? Objective { get; set; }

    }

    public class HrMandateRequestsMasterEditDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Name2 { get; set; }
        public long? CountryClassificationId { get; set; }
        public string? Note { get; set; }
        public long? TypeId { get; set; }
        public string? FromLocation { get; set; }
        public string? ToLocation { get; set; }
        public long? FacilityId { get; set; }
        public long? MandateLocationId { get; set; }
        public long? AppId { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? FromDate { get; set; }
        public string? FromDateStr { get; set; }
        public DateTime? ToDate { get; set; }
        public string? ToDateStr { get; set; }
        public string? Objective { get; set; }

    }
    public partial class HrMandateRequestsMasterFilterDto
    {
        public string? Name { get; set; }
        public string? Name2 { get; set; }
        public long? CountryClassificationId { get; set; }
        public string? Note { get; set; }
        public long? TypeId { get; set; }
        public long? AppId { get; set; }
        public long? MandateLocationId { get; set; }
        public string? FromLocation { get; set; }
        public string? ToLocation { get; set; }
    }

}

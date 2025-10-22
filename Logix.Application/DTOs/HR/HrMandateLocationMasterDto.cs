namespace Logix.Application.DTOs.HR
{

    public partial class HrMandateLocationMasterDto
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
        public bool? IsDeleted { get; set; }
        public List<HrMandateLocationDetaileDto> Detaile { get; set; }
    }


    public partial class HrMandateLocationMasterEditDto
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
    }


    public partial class HrMandateLocationFilterDto
    {
        public string? Name { get; set; }
        public string? Name2 { get; set; }
        public long? CountryClassificationId { get; set; }
        public string? Note { get; set; }
        public long? TypeId { get; set; }
        public string? FromLocation { get; set; }
        public string? ToLocation { get; set; }
    }

}

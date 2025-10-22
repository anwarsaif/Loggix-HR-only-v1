namespace Logix.Application.DTOs.HR
{
    public  class HrJobOfferAdvantageDto
    {
        public long? Id { get; set; }
        public long? RecruApplicantId { get; set; }
        public long? JobOfferId { get; set; }
        public long? AdvantagesId { get; set; }
        public string? AdvantagesName { get; set; }
        public bool IsDeleted { get; set; }
    }  
    public  class HrJobOfferAdvantageEditDto
    {
        public long Id { get; set; }
        public long? RecruApplicantId { get; set; }
        public long? JobOfferId { get; set; }
        public long? AdvantagesId { get; set; }
        public string? AdvantagesName { get; set; }
        public bool IsDeleted { get; set; }
    }

}

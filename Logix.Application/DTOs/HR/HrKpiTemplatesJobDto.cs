namespace Logix.Application.DTOs.HR
{
    public class HrKpiTemplatesJobDto
    {
        public long Id { get; set; }
        public long? CatJobId { get; set; }
        public long? TemplateId { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public long? FacilityId { get; set; }

    }
    public class HrKpiTemplatesJobEditDto
    {
        public long Id { get; set; }
        public long? CatJobId { get; set; }
        public long? TemplateId { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public long? FacilityId { get; set; }

    }
}

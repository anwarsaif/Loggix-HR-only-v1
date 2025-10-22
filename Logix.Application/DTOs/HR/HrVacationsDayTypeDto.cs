namespace Logix.Application.DTOs.HR
{
    public class HrVacationsDayTypeDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Name2 { get; set; }
        public decimal? Value { get; set; }
        public string? Note { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class HrVacationsDayTypeEditDto
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Name2 { get; set; }
        public decimal? Value { get; set; }
        public string? Note { get; set; }

        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }

}

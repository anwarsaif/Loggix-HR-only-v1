using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrDecisionsTypeEmployeeDto
    {
        public long Id { get; set; }
        [Required]
        public long? DecisionsTypeId { get; set; }
        public long? EmpId { get; set; }
        [Required]
        public string? EmpCode { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
    public class HrDecisionsTypeEmployeeEditDto
    {
        public long Id { get; set; }
        public long? DecisionsTypeId { get; set; }
        public long? EmpId { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }

}

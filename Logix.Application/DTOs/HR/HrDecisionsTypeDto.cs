using Castle.MicroKernel.SubSystems.Conversion;

using Logix.Domain.HR;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrDecisionsTypeDto
    {
        public long Id { get; set; }
        [Required]
        public string? DecType { get; set; }
        public int? RefTypeId { get; set; }
        public string? Note { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public List<HrDecisionsTypeEmployeeVw>? HrDecisionsTypeEmployee { get; set; }

    }
    public class HrDecisionsTypeEditDto
    {
        public long Id { get; set; }
        public string? DecType { get; set; }
        public int? RefTypeId { get; set; }
        public string? Note { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public List<HrDecisionsTypeEmployeeVw>? HrDecisionsTypeEmployee { get; set; }

    }
    public class HrDecisionsTypeFilterDto
    {
        public long? Id { get; set; }
        public string? DecType { get; set; }
        public int? RefTypeID { get; set; }
        public string? RefTypeName { get; set; }
    }

    public class HrDecisionsTypeGetByIdDto
    {
        public long Id { get; set; }
        public string? DecType { get; set; }
        public int? RefTypeId { get; set; }
        public string? Note { get; set; }
        public int? CatagoriesId { get; set; }
        public string? RefTypeName { get; set; }
        public List<HrDecisionsTypeEmployeeVw>? HrDecisionsTypeEmployee { get; set; }

    }

}

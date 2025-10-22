using Castle.MicroKernel.SubSystems.Conversion;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR
{
    public class HrEmpWarnDto
    {
        public long EmpWarnId { get; set; }
        public int? WarnWhy { get; set; }
        [StringLength(50)]
        public string? WarnDate { get; set; }
        public long? EmpId { get; set; }
        public string? Note { get; set; }
        public int? AdId { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public int? DeducationDays { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }

        public bool? IsDeleted { get; set; }

    }
    public class HrEmpWarnEditDto
    {

        public long EmpWarnId { get; set; }
        public int? WarnWhy { get; set; }
        [StringLength(50)]
        public string? WarnDate { get; set; }
        public long? EmpId { get; set; }
        public string? Note { get; set; }
        public int? AdId { get; set; }
        public decimal? Rate { get; set; }
        public decimal? Amount { get; set; }
        public int? DeducationDays { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }

}

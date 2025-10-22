using Castle.MicroKernel.SubSystems.Conversion;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace Logix.Application.DTOs.HR
{
    public class HrAttShiftEmployeeDto
    {
        public long Id { get; set; }
        
        public long? EmpId { get; set; }
        public long? ShitId { get; set; }
        [StringLength(10)]
        public string? BeginDate { get; set; }

        [StringLength(10)]
        public string? EndDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }

    }


    public class HrAttShiftEmployeeEditDto
    {
        public long Id { get; set; }

        public long? EmpId { get; set; }
        public long? ShitId { get; set; }
        [StringLength(10)]
        public string? BeginDate { get; set; }

        [StringLength(10)]
        public string? EndDate { get; set; }

        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }

    public class HrAttShiftEmployeeFilterDto
    {
        public long? LocationId { get; set; }
        public int? BranchId { get; set; }
        public string? EmpName { get; set; }
        public string? EmpCode { get; set; }
        [Required]
        public string BeginDate { get; set; } = null!;
        [Required]
        public string EndDate { get; set; } = null!;
        //===============search 2=====================
        public int? JobCatagoriesId { get; set; }
        public int? DeptId { get; set; }

        public int? Location { get; set; }

        public long? ShitId { get; set; }

    }
    public class HrAttShiftEmployeeAddDto
    {
        [Required]
        public string BeginDate { get; set; } = null!;
        [Required]
        public string EndDate { get; set; } = null!;

        public long? ShitId { get; set; }
        [Required]
        public List<long>? EmpId { get; set; }

    }
    public class HrAttShiftEmployeeCancelDto
    {
        [Required]
        public string BeginDate { get; set; } = null!;
        [Required]
        public string EndDate { get; set; } = null!;

        public int? Location { get; set; }
        public long? ShitId { get; set; }
        [Required]
        public long empId { get; set; }


    }

    public class HrAttShiftEmployeeAddFromExcelDto
    {
        [Required]
        public string BeginDate { get; set; } = null!;
        [Required]
        public string EndDate { get; set; } = null!;

        public long? ShitId { get; set; }
        [Required]
        public string? empCode { get; set; }

    }

}

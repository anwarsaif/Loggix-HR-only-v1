using Castle.MicroKernel.SubSystems.Conversion;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Logix.Application.DTOs.Main;
using Logix.Domain.HR;


namespace Logix.Application.DTOs.HR
{
    public class HrOverTimeMDto
    {
        public long Id { get; set; }
        public long? EmpId { get; set; }
        public string? EmpCode { get; set; }
        [StringLength(10)]
        public string? DateFrom { get; set; }
        [StringLength(10)]
        public string? DateTo { get; set; }
        [StringLength(10)]
        public string? DateTran { get; set; }
        [StringLength(50)]
        public string? RefranceId { get; set; }
        public int? PaymentType { get; set; }
        public string? Note { get; set; }
        public decimal? CntHoursTotal { get; set; }
        public decimal? CntHoursDay { get; set; }
        public decimal? CntHoursMonth { get; set; }
        public long? ProjectId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }

    }

    public class HrOverTimeMEditDto
    {
        public long Id { get; set; }
        public long? EmpId { get; set; }
        [StringLength(10)]
        public string? DateFrom { get; set; }
        [StringLength(10)]
        public string? DateTo { get; set; }
        [StringLength(10)]
        public string? DateTran { get; set; }
        [StringLength(50)]
        public string? RefranceId { get; set; }
        public int? PaymentType { get; set; }
        public string? Note { get; set; }
        public decimal? CntHoursTotal { get; set; }
        public decimal? CntHoursDay { get; set; }
        public decimal? CntHoursMonth { get; set; }
        public long? ProjectId { get; set; }
        public string? EmpCode { get; set; }
        public string? ProjectCode { get; set; }
        public List<HrOverTimeDEditDto>? hrOverTimeDDtos { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }



    }
    public class HrOverTimeMAddDto
    {
        public long? Id { get; set; }
        public string? EmpCode { get; set; }

        public long? EmpId { get; set; }
        [StringLength(10)]
        public string? DateFrom { get; set; }
        [StringLength(10)]
        public string? DateTo { get; set; }
        [StringLength(10)]
        public string? DateTran { get; set; }
        [StringLength(50)]
        public string? RefranceId { get; set; }
        public int? PaymentType { get; set; }
        public string? Note { get; set; }
        public decimal? CntHoursTotal { get; set; }
        public decimal? CntHoursDay { get; set; }
        public decimal? CntHoursMonth { get; set; }
        public long? ProjectId { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public int? Type { get; set; }
        public string? ProjectCode { get; set; }
        public List<HrOverTimeDDto?> hrOverTimeDDtos { get; set; }
        public List<SaveFileDto>? fileDtos { get; set; }
    }
    public class HrOverTimeMAddUsingExcelDto
    {
        public long Id { get; set; }
        [Required]
        public string? DateFrom { get; set; }
        [Required]
        public string? DateTo { get; set; }
        [Required]
        public string? DateTran { get; set; }
        public int? PaymentType { get; set; }
        // احتساب الإضافي من
        [Required]
        public int? Type { get; set; }
        [Required]
        public int? OverTimeType { get; set; }
        public string? Note { get; set; }
        [Required]
        public decimal OverTimeHCost { get; set; }
        public List<HrOverTimeDDto?> hrOverTimeDDtos { get; set; }
    }

    public class HrOverTimeMFilterDto
    {
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public string? RefranceId { get; set; }

        public int? Location { get; set; }
        public int? BranchId { get; set; }
        public int? DeptId { get; set; }
    }

    public partial class HrOverTimeMGetByIdDto
    {
        public long Id { get; set; }
        public long? EmpId { get; set; }
        public string? DateFrom { get; set; }
        public string? DateTo { get; set; }
        public string? DateTran { get; set; }
        public string? RefranceId { get; set; }
        public int? PaymentType { get; set; }
        public decimal? CntHoursTotal { get; set; }
        public decimal? CntHoursDay { get; set; }
        public decimal? CntHoursMonth { get; set; }
        public string? EmpName { get; set; }
        public string? EmpName2 { get; set; }
        public string? EmpCode { get; set; }
        public long? ProjectId { get; set; }
        public long? ProjectCode { get; set; }
        public string? ProjectName { get; set; }
        public List<HrOverTimeDVw>? hrOverTimeDVws { get; set; }
        public List<SaveFileDto> fileDtos { get; set; }

    }
    public class HrEmpSalaryAndOverTimeDto
    {

        public decimal? DailyWorkingHours { get; set; }
        public decimal? BasicSalary { get; set; }
        public decimal? TotalSalary { get; set; }
        public decimal? OverTimeHCost { get; set; }
        public decimal? Amount { get; set; }

    }

    public partial class HrOverTimeMAdd4Dto
    {
        [Required]
        public string DateFrom { get; set; } = null!;
        [Required]
        public string? DateTo { get; set; } = null!;
        [Required]
        public string? DateTran { get; set; } = null!;
        public string? RefranceId { get; set; }
        [Required]
        public int PaymentType { get; set; }
        public string? Note { get; set; }

        public List<HrOverTimeMAdd4DetailsDto?> hrOverTimeMAdd4DetailsDto { get; set; }
    }

    public partial class HrOverTimeMAdd4DetailsDto
    {
        [Required]
        public string EmpCode { get; set; } = null!;
        [Required]
        public string EmpName { get; set; } = null!;
        [Required]
        public decimal? CntHoursTotal { get; set; } = null!;
        [Required]
        public decimal? CntHoursMonth { get; set; } = null!;
        [Required]
        public decimal? CntHoursDay { get; set; } = null!;
    }

    /// <summary>
    ///  this Dto for search on screen Of Name (سحب من سجلات الحضور والإنصراف)
    /// </summary>
    public class HrGetAttendanceButtonClickDto
    {
        [Required]
        public string DateFrom { get; set; } = null!;
        [Required]
        public string DateTo { get; set; } = null!;
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? RefranceID { get; set; }
        public int? BRANCHID { get; set; }
        public int? Location { get; set; }
        public int? DeptID { get; set; }
    }

    public class HrGetAttendanceButtonResult
    {


        public long? IdM { get; set; }

        public int? OverTimeTybe { get; set; }

        public decimal? OverTimeHCost { get; set; }

        public decimal? Hours { get; set; }

        public decimal? Amount { get; set; }
        public decimal? Total { get; set; }
        public int? CurrencyId { get; set; }
        public string? CurrencyName { get; set; }
        public string? Description { get; set; }
        public decimal? AssignmentHours { get; set; }


        public string? OverTimeDate { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? RefranceId { get; set; }
        public double? HoursWork { get; set; }

    }
}

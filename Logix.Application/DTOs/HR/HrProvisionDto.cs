using Castle.MicroKernel.SubSystems.Conversion;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Logix.Domain.HR;
using Microsoft.EntityFrameworkCore;

namespace Logix.Application.DTOs.HR
{
    public partial class HrProvisionDto
    {
        public int? Id { get; set; }
        public long? TypeId { get; set; }
        public string? Code { get; set; }
        public long? No { get; set; }
        public string? PDate { get; set; }
        public long? FinYear { get; set; }
        public int? MonthId { get; set; }
        public string? Description { get; set; }
        public long? YearlyOrMonthly { get; set; }
        public long? FacilityId { get; set; }
        public bool IsDeleted { get; set; }
        public List<string> EmpCodes { get; set; }
        public string? ToDate { get; set; }

    }
    public partial class HrProvisionEditDto
    {
        public int Id { get; set; }
        public long? TypeId { get; set; }
        public string? Code { get; set; }
        public long? No { get; set; }
        public string? PDate { get; set; }
        public long? FinYear { get; set; }
        public int? MonthId { get; set; }
        public string? Description { get; set; }
        public long? YearlyOrMonthly { get; set; }
        public long? FacilityId { get; set; }
        public List<HrProvisionEmployeeResultDto> ProvisionsEmployee { get; set; }
        public string? JCode { get; set; }

    }
    public partial class HrProvisionFilterDto
    {
		public int Id { get; set; }
		public long? TypeId { get; set; }
		[StringLength(50)]
		public string? Code { get; set; }
		[StringLength(10)]
		public string? PDate { get; set; }
		public long? FinYear { get; set; }
		public int? MonthId { get; set; }
		public string? Description { get; set; }
		public string? YearlyOrMonthlyName { get; set; }
		public string? MonthName { get; set; }
		public string? FacilityName { get; set; }
		public string? ToDate { get; set; }
		public string? FromDate { get; set; }
		public long? YearlyOrMonthly { get; set; }
		public long? FacilityId { get; set; }
	}
    public class HrProvisionEmployeeResultDto
    {
        public long? Id { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public string? DepName { get; set; }
        public string? LocationName { get; set; }
        public decimal? BasicSalary { get; set; }
        public string? DOAppointment { get; set; }
        public int? DeptID { get; set; }
        public decimal? TotalSalary { get; set; }
        //public int? CountYear { get; set; }
        public decimal? TotalDeductions { get; set; }
        public decimal? TotalAllowances { get; set; }
        public decimal? Amount { get; set; }
        public decimal? NetSalary { get; set; }
        public bool IsDeleted { get; set; }
        public long? SalaryGroup { get; set; }
        public int? CutYear { get; set; }
    }
    public class ProvisionSearchOnAddFilter
    {
        public long? YearlyOrMonthly { get; set; }
        public string? StatusList { get; set; }
        public string? ToDate { get; set; } 
        public int? BranchId { get; set; }
        public int? DepartmentId { get; set; }
        public int? LocationId { get; set; }
        public int? NationalityId { get; set; }
        public long? JobCategory { get; set; }
        public string? EmpCode { get; set; }
        public string? EmpName { get; set; }
        public long? MonthId { get; set; }
        public long? FinYear { get; set; }
        public long? TypeId { get; set; }
        public long? SalaryGroupId { get; set; }

    }
    public class HrProvisionEntryAddDto
    {
        public long Id { get; set; }

        [Required]

        public string JournalDate { get; set; } = null!;
        public int? Type { get; set; }
        public int? DocTypeId { get; set; }


    }

}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
	public partial class HrProvisionsMedicalInsuranceDto
	{
		public int Id { get; set; }
		[StringLength(50)]
		public string? Code { get; set; }
		public long? No { get; set; }
		[StringLength(10)]
		public string? PDate { get; set; }
		public long? FinYear { get; set; }
		public long? SalaryGroupID { get; set; }
		public int? MonthId { get; set; }
		public int? BranchId { get; set; }
		public int? DeptId { get; set; }
		public int? LocationId { get; set; }
		public int? NationalityID { get; set; }
		public int? JobCatagoriesID { get; set; }
		public string? Description { get; set; }
		public string? EmpCode { get; set; }
		public string? EmpName { get; set; }
		public List<long> EmpId { get; set; }
		public long? YearlyOrMonthly { get; set; }
		public long? PolicyId { get; set; }
		public long? FacilityId { get; set; }
		public List<string>? EmpCodes { get; set; }
		public bool? IsDeleted { get; set; }
	}

	public partial class HrProvisionsMedicalInsuranceEditDto
	{
		public int Id { get; set; }
		public string? Code { get; set; }
		public long? No { get; set; }
		public string? PDate { get; set; }
		public long? FinYear { get; set; }
		public int? MonthId { get; set; }
		public string? Description { get; set; }
		public long? YearlyOrMonthly { get; set; }
		public long? FacilityId { get; set; }
		public List<HrProvisionsMedicalInsuranceEmployeeResultDto> ProvisionsMedicalInsuranceEmployee { get; set; }
		public string? JCode { get; set; }

	}
	public partial class HrProvisionsMedicalInsuranceFilterDto
	{
		public int Id { get; set; }
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
	public class HrProvisionsMedicalInsuranceEmployeeResultDto
	{
		public long? Id { get; set; }
		public long? PId { get; set; }
		public long? PolicyId { get; set; }
		public decimal? InsuranceAmount { get; set; }
		public decimal? TotalPreInsuranceAmount { get; set; }
		public decimal? CurrentInsuranceAmount { get; set; }
		public long? DeptId { get; set; }
		public long? LocationId { get; set; }
		public long? FacilityId { get; set; }
		public long? BranchId { get; set; }
		public long? CcId { get; set; }
		[StringLength(50)]
		public string EmpCode { get; set; } = null!;
		[StringLength(250)]
		public string? EmpName { get; set; }
		[StringLength(200)]
		public string? DepName2 { get; set; }
		[StringLength(200)]
		public string? LocationName2 { get; set; }
		[StringLength(200)]
		public string? DepName { get; set; }
		[StringLength(200)]
		public string? LocationName { get; set; }
		[StringLength(250)]
		public string? PolicyCode { get; set; }
		[StringLength(250)]
		public string? EmpName2 { get; set; }
		public long? EmpId { get; set; }
		public decimal? ExcludedValue { get; set; }
		public decimal? RemainingInsuranceAmount { get; set; }
		public bool IsDeleted { get; set; }
	}
	public class HrProvisionsMedicalInsuranceSearchOnAddFilter
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
		public long? SalaryGroupId { get; set; }
		public long? PolicyId { get; set; }

	}
	public class HrProvisionsMedicalInsuranceEntryAddDto
	{
		public long Id { get; set; }

		[Required]

		public string JournalDate { get; set; } = null!;
		public int? Type { get; set; }
		public int? DocTypeId { get; set; }


	}
}

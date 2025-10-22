using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
	public class HrProvisionsMedicalInsuranceEmployeeDto
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
		public long? CreatedBy { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? CreatedOn { get; set; }
		public bool? IsDeleted { get; set; }
		public decimal? ExcludedValue { get; set; }
		public decimal? RemainingInsuranceAmount { get; set; }
	}

	public class HrProvisionsMedicalInsuranceEmployeeEditDto
	{
		public long Id { get; set; }
		public long? PId { get; set; }
		public long? EmpId { get; set; }
		public decimal? BasicSalary { get; set; }
		public decimal? TotalAllowances { get; set; }
		public decimal? TotalDeductions { get; set; }
		public decimal? NetSalary { get; set; }
		public decimal? TotalSalary { get; set; }
		public decimal? Amount { get; set; }
		public long? DeptId { get; set; }
		public long? LocationId { get; set; }
		public long? FacilityId { get; set; }
		public long? BranchId { get; set; }
		public long? SalaryGroupId { get; set; }
		public long? CcId { get; set; }
	}
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{

	[Keyless]
	public partial class HrProvisionsMedicalInsuranceEmployeeVw
	{
		[Column("P_ID")]
		public long? PId { get; set; }

		[Column("Policy_ID")]
		public long? PolicyId { get; set; }

		[Column("Insurance_Amount", TypeName = "decimal(18, 2)")]
		public decimal? InsuranceAmount { get; set; }

		[Column("Total_PreInsurance_Amount", TypeName = "decimal(18, 2)")]
		public decimal? TotalPreInsuranceAmount { get; set; }

		[Column("Current_Insurance_Amount", TypeName = "decimal(18, 2)")]
		public decimal? CurrentInsuranceAmount { get; set; }

		[Column("Dept_ID")]
		public long? DeptId { get; set; }

		[Column("Location_ID")]
		public long? LocationId { get; set; }

		[Column("Facility_ID")]
		public long? FacilityId { get; set; }

		[Column("Branch_ID")]
		public long? BranchId { get; set; }

		[Column("CC_ID")]
		public long? CcId { get; set; }

		public long? CreatedBy { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? CreatedOn { get; set; }

		public long? ModifiedBy { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? ModifiedOn { get; set; }

		public bool? IsDeleted { get; set; }

		[Column("Emp_Code")]
		[StringLength(50)]
		public string EmpCode { get; set; } = null!;

		[Column("Emp_name")]
		[StringLength(250)]
		public string? EmpName { get; set; }

		[Column("Dep_Name2")]
		[StringLength(200)]
		public string? DepName2 { get; set; }

		[Column("Location_Name2")]
		[StringLength(200)]
		public string? LocationName2 { get; set; }

		[Column("Dep_Name")]
		[StringLength(200)]
		public string? DepName { get; set; }

		[Column("Location_Name")]
		[StringLength(200)]
		public string? LocationName { get; set; }

		[Column("Policy_Code")]
		[StringLength(250)]
		public string? PolicyCode { get; set; }

		[Column("ID")]
		public long Id { get; set; }

		[Column("Emp_name2")]
		[StringLength(250)]
		public string? EmpName2 { get; set; }

		[Column("Emp_ID")]
		public long? EmpId { get; set; }

		[Column("Excluded_Value", TypeName = "decimal(18, 2)")]
		public decimal? ExcludedValue { get; set; }
	}

}

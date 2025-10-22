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
	public partial class HrProvisionsMedicalInsuranceEmployeeAccVw
	{
		[Column("Emp_ID")]
		public long? EmpId { get; set; }

		[Column("Emp_Code")]
		[StringLength(50)]
		public string EmpCode { get; set; } = null!;

		[Column("Emp_name")]
		[StringLength(250)]
		public string? EmpName { get; set; }

		[Column("ID_D")]
		public long IdD { get; set; }

		[Column("P_ID")]
		public long? PId { get; set; }

		[Column("Policy_ID")]
		public long? PolicyId { get; set; }

		[Column("Insurance_Amount", TypeName = "decimal(18, 2)")]
		public decimal? InsuranceAmount { get; set; }

		[Column("Total_PreInsurance_Amount", TypeName = "decimal(18, 2)")]
		public decimal? TotalPreInsuranceAmount { get; set; }

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

		[Column("IsDeleted_D")]
		public bool? IsDeletedD { get; set; }

		[Column("Current_Insurance_Amount", TypeName = "decimal(18, 2)")]
		public decimal? CurrentInsuranceAmount { get; set; }

		[Column("ID")]
		public int Id { get; set; }

		[StringLength(50)]
		public string? Code { get; set; }

		public long? No { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? CreatedOn { get; set; }

		public long? ModifiedBy { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? ModifiedOn { get; set; }

		[Column("P_Date")]
		[StringLength(10)]
		public string? PDate { get; set; }

		[Column("Fin_Year")]
		public long? FinYear { get; set; }

		[Column("Month_ID")]
		public int? MonthId { get; set; }

		public string? Description { get; set; }

		[Column("Yearly_or_Monthly")]
		public long? YearlyOrMonthly { get; set; }

		[Column("FacilityID")]
		public long? FacilityId1 { get; set; }

		public bool? IsDeleted { get; set; }

		[Column("Salary_Group_ID")]
		public long? SalaryGroupId { get; set; }

		[Column("Excluded_Value", TypeName = "decimal(18, 2)")]
		public decimal? ExcludedValue { get; set; }
	}

	public class ProvisionsMedicalInsuranceEmployeeAccVwDto
	{
		public long? AccountID { get; set; }
		public decimal? Credit { get; set; }
		public decimal? Debit { get; set; }
		public string Description { get; set; }
		public long? CCID { get; set; }
		public long? CCID2 { get; set; }
		public long? CCID3 { get; set; }
		public long? CCID4 { get; set; }
		public long? CCID5 { get; set; }
		public long? ReferenceNo { get; set; }
		public int? ReferenceTypeID { get; set; }
	}

}

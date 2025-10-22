using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{

	[Keyless]
	public partial class HrMandateVw
	{
		[Column("ID")]
		public long Id { get; set; }

		[Column("From_Date")]
		[StringLength(10)]
		public string? FromDate { get; set; }

		[Column("To_Date")]
		[StringLength(10)]
		public string? ToDate { get; set; }

		[Column("Emp_ID")]
		public long? EmpId { get; set; }

		[Column("From_Location")]
		[StringLength(250)]
		public string? FromLocation { get; set; }

		[Column("To_location")]
		[StringLength(250)]
		public string? ToLocation { get; set; }

		public string? Objective { get; set; }

		[Column("Visa_Travel")]
		public int? VisaTravel { get; set; }

		[Column("Travel_By")]
		public int? TravelBy { get; set; }

		public int? Accommodation { get; set; }

		[Column("No_of_night")]
		public int? NoOfNight { get; set; }

		[Column("Rate_per_night", TypeName = "decimal(18, 2)")]
		public decimal? RatePerNight { get; set; }

		[Column("Other_Expenses", TypeName = "decimal(18, 2)")]
		public decimal? OtherExpenses { get; set; }

		[Column("Actual_Expenses", TypeName = "decimal(18, 2)")]
		public decimal? ActualExpenses { get; set; }

		public string? Note { get; set; }

		public bool? IsDeleted { get; set; }

		[Column("Emp_Code")]
		[StringLength(50)]
		public string EmpCode { get; set; } = null!;

		[Column("Emp_name")]
		[StringLength(250)]
		public string? EmpName { get; set; }

		[Column("Transport_Amount", TypeName = "decimal(18, 2)")]
		public decimal? TransportAmount { get; set; }

		[Column("Job_Catagories_ID")]
		public int? JobCatagoriesId { get; set; }

		[Column("Dept_ID")]
		public int? DeptId { get; set; }

		public int? Location { get; set; }

		[Column("BRANCH_ID")]
		public int? BranchId { get; set; }

		[Column("Type_ID")]
		public int? TypeId { get; set; }

		[Column("Ticket_Type")]
		public int? TicketType { get; set; }

		[Column("Ticket_Value", TypeName = "decimal(18, 2)")]
		public decimal? TicketValue { get; set; }

		[Column("Account_No")]
		[StringLength(50)]
		public string? AccountNo { get; set; }

		[Column("Bank_ID")]
		public int? BankId { get; set; }

		[Column("Payroll_ID")]
		public long? PayrollId { get; set; }

		[Column(TypeName = "decimal(18, 2)")]
		public decimal? Salary { get; set; }

		[Column("Emp_name2")]
		[StringLength(250)]
		public string? EmpName2 { get; set; }

		[Column("Location_Name2")]
		[StringLength(200)]
		public string? LocationName2 { get; set; }

		[Column("Dep_Name2")]
		[StringLength(200)]
		public string? DepName2 { get; set; }

		[Column("Cat_name2")]
		[StringLength(250)]
		public string? CatName2 { get; set; }

		[Column("Nationality_Name2")]
		[StringLength(250)]
		public string? NationalityName2 { get; set; }

		[Column("Qualification_Name2")]
		[StringLength(250)]
		public string? QualificationName2 { get; set; }

		[Column("Facility_Name2")]
		[StringLength(500)]
		public string? FacilityName2 { get; set; }

		[Column("Type_Name")]
		[StringLength(250)]
		public string? TypeName { get; set; }

		[Column("Location_Name")]
		[StringLength(200)]
		public string? LocationName { get; set; }

		[Column("Dep_Name")]
		[StringLength(200)]
		public string? DepName { get; set; }

		[Column("BRA_NAME2")]
		public string? BraName2 { get; set; }

		[Column("BRA_NAME")]
		public string? BraName { get; set; }

		[Column("Cat_ID")]
		public int? CatId { get; set; }

		[Column("App_ID")]
		public long? AppId { get; set; }

		[Column("Facility_ID")]
		public int? FacilityId { get; set; }
	}

}

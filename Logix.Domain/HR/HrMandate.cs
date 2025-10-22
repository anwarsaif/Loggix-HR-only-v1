using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{

	[Table("HR_Mandate")]
	public partial class HrMandate
	{
		[Key]
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

		public long? CreatedBy { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? CreatedOn { get; set; }

		public long? ModifiedBy { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? ModifiedOn { get; set; }

		public bool? IsDeleted { get; set; }

		public string? Note { get; set; }

		[Column("Transport_Amount", TypeName = "decimal(18, 2)")]
		public decimal? TransportAmount { get; set; }

		[Column("Type_ID")]
		public int? TypeId { get; set; }

		[Column("Ticket_Type")]
		public int? TicketType { get; set; }

		[Column("Ticket_Value", TypeName = "decimal(18, 2)")]
		public decimal? TicketValue { get; set; }

		[Column("Payroll_ID")]
		public long? PayrollId { get; set; }

		[Column("Cat_ID")]
		public int? CatId { get; set; }

		[Column("App_ID")]
		public long? AppId { get; set; }
	}

}

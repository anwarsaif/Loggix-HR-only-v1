using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{

	[Table("HR_Job_Levels")]
	public partial class HrJobLevel
	{
		[Key]
		[Column("ID")]
		public long Id { get; set; }

		[Column("Level_Name")]
		[StringLength(250)]
		public string? LevelName { get; set; }

		[Column("Min_Salary", TypeName = "decimal(18, 2)")]
		public decimal? MinSalary { get; set; }

		[Column("Max_Salary", TypeName = "decimal(18, 2)")]
		public decimal? MaxSalary { get; set; }

		[Column("Annual_Increase", TypeName = "decimal(18, 2)")]
		public decimal? AnnualIncrease { get; set; }

		[Column("Med_Insurance_Type")]
		public int? MedInsuranceType { get; set; }

		[Column(TypeName = "decimal(18, 2)")]
		public decimal? Mandate { get; set; }

		[Column("Ticket_Type")]
		public int? TicketType { get; set; }

		[Column("Ticket_Value", TypeName = "decimal(18, 2)")]
		public decimal? TicketValue { get; set; }

		[Column("Ticket_Count")]
		public int? TicketCount { get; set; }

		[Column("Vacation_year")]
		public int? VacationYear { get; set; }

		[Column("Vacation_Necessity")]
		public int? VacationNecessity { get; set; }

		public long? CreatedBy { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? CreatedOn { get; set; }

		public long? ModifiedBy { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? ModifiedOn { get; set; }

		public bool? IsDeleted { get; set; }

		[Column(TypeName = "decimal(18, 2)")]
		public decimal? MandateOut { get; set; }

		[Column("Rate_per_night", TypeName = "decimal(18, 2)")]
		public decimal? RatePerNight { get; set; }

		[Column("Transport_Amount", TypeName = "decimal(18, 2)")]
		public decimal? TransportAmount { get; set; }

		[Column(TypeName = "decimal(4, 2)")]
		public decimal? DurationStay { get; set; }

		[Column("Group_ID")]
		public int? GroupId { get; set; }
	}
}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
	[Keyless]
	public partial class HrJobAllowanceDeductionVw
	{
		[Column("ID")]
		public long Id { get; set; }

		[Column("Job_ID")]
		public long? JobId { get; set; }

		[Column("Type_ID")]
		public int? TypeId { get; set; }

		[Column("AD_ID")]
		public int? AdId { get; set; }

		[Column(TypeName = "decimal(18, 2)")]
		public decimal? Rate { get; set; }

		[Column(TypeName = "decimal(18, 2)")]
		public decimal? Amount { get; set; }

		[Column("Start_Date")]
		[StringLength(10)]
		public string? StartDate { get; set; }

		[Column("End_Date")]
		[StringLength(10)]
		public string? EndDate { get; set; }

		[Column("Is_Active")]
		public bool? IsActive { get; set; }

		public bool IsDeleted { get; set; }
	}

}
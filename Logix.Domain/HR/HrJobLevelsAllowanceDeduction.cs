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
	[Table("HR_Job_Levels_Allowance_Deduction")]
	public partial class HrJobLevelsAllowanceDeduction
	{
		[Key]
		[Column("ID")]
		public long Id { get; set; }

		[Column("Level_ID")]
		public long? LevelId { get; set; }

		[Column("Type_ID")]
		public int? TypeId { get; set; }

		[Column("AD_ID")]
		public int? AdId { get; set; }

		[Column(TypeName = "decimal(18, 2)")]
		public decimal? Rate { get; set; }

		[Column(TypeName = "decimal(18, 2)")]
		public decimal? Amount { get; set; }

		public long CreatedBy { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? CreatedOn { get; set; }

		public long? ModifiedBy { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? ModifiedOn { get; set; }

		public bool IsDeleted { get; set; }

		[Column("Start_Date")]
		[StringLength(10)]
		public string? StartDate { get; set; }

		[Column("Is_Active")]
		public bool? IsActive { get; set; }

		[Column("End_Date")]
		[StringLength(10)]
		[Unicode(false)]
		public string? EndDate { get; set; }
	}

}
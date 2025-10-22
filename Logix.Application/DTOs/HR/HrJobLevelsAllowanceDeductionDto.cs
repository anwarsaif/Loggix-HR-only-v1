using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
	public partial class HrJobLevelsAllowanceDeductionDto
	{
		[Key]
		public long Id { get; set; }
		public long? LevelId { get; set; }
		public int? TypeId { get; set; }
		public int? AdId { get; set; }
		public decimal? Rate { get; set; }
		public decimal? Amount { get; set; }
		public bool IsDeleted { get; set; }
		[StringLength(10)]
		public string? StartDate { get; set; }
		public bool? IsActive { get; set; }
		[StringLength(10)]
		[Unicode(false)]
		public string? EndDate { get; set; }
	}

	public partial class HrJobLevelsAllowanceDeductionEditDto
	{
		[Key]
		public long Id { get; set; }
		public long? LevelId { get; set; }
		public int? TypeId { get; set; }
		public int? AdId { get; set; }
		public decimal? Rate { get; set; }
		public decimal? Amount { get; set; }
		[StringLength(10)]
		public string? StartDate { get; set; }
		public bool? IsActive { get; set; }
		[StringLength(10)]
		[Unicode(false)]
		public string? EndDate { get; set; }
	}
	public partial class HrJobLevelName
	{
		public string? LevelName { get; set; }
	}
}

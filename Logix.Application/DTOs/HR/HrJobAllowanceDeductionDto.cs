using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
	public partial class HrJobAllowanceDeductionDto
	{
		[Key]
		public long Id { get; set; }
		public long? JobId { get; set; }
		public int? TypeId { get; set; }
		public int? AdId { get; set; }
		public decimal? Rate { get; set; }
		public decimal? Amount { get; set; }
		public bool IsDeleted { get; set; }
		[StringLength(10)]
		public string? StartDate { get; set; }
		[StringLength(10)]
		public string? EndDate { get; set; }
		public bool? IsActive { get; set; }
	}
	public partial class HrJobDataByIdDto
	{
		public string? JobNo { get; set; }
		public string? JobName { get; set; }
		public long? LevelId { get; set; }
		public string? MofCode { get; set; }
		public string? LevelName { get; set; }
	}
	public partial class HrJobAllowanceDeductionEditDto
	{
		[Key]
		public long Id { get; set; }
		public long? JobId { get; set; }
		public int? TypeId { get; set; }
		public int? AdId { get; set; }
		public decimal? Rate { get; set; }
		public decimal? Amount { get; set; }
		public bool IsDeleted { get; set; }
		[StringLength(10)]
		public string? StartDate { get; set; }
		[StringLength(10)]
		public string? EndDate { get; set; }
		public bool? IsActive { get; set; }
	}

	public class BulkSaveRequestDto
	{
		public long JobId { get; set; }
		public List<HrJobAllowanceDeductionDto> Allowances { get; set; }
		public List<HrJobAllowanceDeductionDto> Deductions { get; set; }
		public int UserId { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
	public partial class HrJobCategoryDto
	{
		public long Id { get; set; }
		[StringLength(50)]
		public string? Code { get; set; }
		[StringLength(250)]
		public string? Name { get; set; }
		[StringLength(250)]
		public string? Name2 { get; set; }
		public long? GroupId { get; set; }
		[StringLength(50)]
		public string? RefranceCode { get; set; }
		[StringLength(250)]
		public string? RefranceName { get; set; }
		public int? StatusId { get; set; }
		public bool IsDeleted { get; set; }
	}
	public partial class HrJobCategoryEditDto
	{
		public long Id { get; set; }
		[StringLength(50)]
		public string? Code { get; set; }
		[StringLength(250)]
		public string? Name { get; set; }
		[StringLength(250)]
		public string? Name2 { get; set; }
		public long? GroupId { get; set; }
		[StringLength(50)]
		public string? RefranceCode { get; set; }
		[StringLength(250)]
		public string? RefranceName { get; set; }
		public int? StatusId { get; set; }
	}
	public partial class HrJobCategoryFilterDto
	{
		public long Id { get; set; }
		[StringLength(50)]
		public string? Code { get; set; }
		[StringLength(250)]
		public string? Name { get; set; }
		[StringLength(250)]
		public string? Name2 { get; set; }
		public long? GroupId { get; set; }
		[StringLength(50)]
		public string? GroupName { get; set; }
		public string? GroupName2 { get; set; }
		public string? RefranceCode { get; set; }
		[StringLength(250)]
		public string? RefranceName { get; set; }
		public int? StatusId { get; set; }
		public string? StatusName { get; set; }
		public string? StatusName2 { get; set; }
	}

}

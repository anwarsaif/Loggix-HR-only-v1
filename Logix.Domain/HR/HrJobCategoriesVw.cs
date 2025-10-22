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
	public partial class HrJobCategoriesVw
	{
		[Column("ID")]
		public long Id { get; set; }

		[StringLength(250)]
		public string? Name { get; set; }

		[StringLength(250)]
		public string? Name2 { get; set; }

		[StringLength(50)]
		public string? Code { get; set; }

		[Column("Group_Name")]
		[StringLength(250)]
		public string? GroupName { get; set; }

		[Column("Group_Name2")]
		[StringLength(250)]
		public string? GroupName2 { get; set; }

		[Column("Group_ID")]
		public long? GroupId { get; set; }

		[Column("Refrance_Code")]
		[StringLength(50)]
		public string? RefranceCode { get; set; }

		[Column("Refrance_Name")]
		[StringLength(250)]
		public string? RefranceName { get; set; }

		[Column("Status_ID")]
		public int? StatusId { get; set; }

		public long? CreatedBy { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? CreatedOn { get; set; }

		public long? ModifiedBy { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? ModifiedOn { get; set; }

		public bool IsDeleted { get; set; }

		[Column("Status_Name")]
		[StringLength(250)]
		public string? StatusName { get; set; }

		[Column("Status_Name2")]
		[StringLength(250)]
		public string? StatusName2 { get; set; }
	}

}

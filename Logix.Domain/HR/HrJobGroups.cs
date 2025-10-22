using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
	[Table("HR_Job_Groups")]
	public partial class HrJobGroups
	{
		[Key]
		[Column("ID")]
		public long Id { get; set; }

		[StringLength(50)]
		public string? Code { get; set; }

		[StringLength(250)]
		public string? Name { get; set; }

		[StringLength(250)]
		public string? Name2 { get; set; }

		[Column("Parent_ID")]
		public long? ParentId { get; set; }

		public bool? HasSubGroup { get; set; }

		[Column("Status_ID")]
		public int? StatusId { get; set; }

		public long? CreatedBy { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? CreatedOn { get; set; }

		public long? ModifiedBy { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? ModifiedOn { get; set; }

		public bool IsDeleted { get; set; }
	}

}

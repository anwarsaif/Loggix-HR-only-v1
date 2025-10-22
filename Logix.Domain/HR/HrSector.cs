using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
	[Table("HR_Sectors")]
	public partial class HrSector
	{
		[Key]
		[Column("ID")]
		public long Id { get; set; }

		public string? Name { get; set; }

		public string? Name2 { get; set; }

		public long? CreatedBy { get; set; }

		[Column("Facility_ID")]
		public long? FacilityId { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? CreatedOn { get; set; }

		public long? ModifiedBy { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? ModifiedOn { get; set; }

		public bool? IsDeleted { get; set; }
	}
}

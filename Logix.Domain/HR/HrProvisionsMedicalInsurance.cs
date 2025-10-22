using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
	[Table("HR_Provisions_MedicalInsurance")]
	public partial class HrProvisionsMedicalInsurance
	{
		[Key]
		[Column("ID")]
		public int Id { get; set; }

		[StringLength(50)]
		public string? Code { get; set; }

		public long? No { get; set; }

		[Column("P_Date")]
		[StringLength(10)]
		public string? PDate { get; set; }

		[Column("Fin_Year")]
		public long? FinYear { get; set; }

		[Column("Month_ID")]
		public int? MonthId { get; set; }

		public string? Description { get; set; }

		[Column("Yearly_or_Monthly")]
		public long? YearlyOrMonthly { get; set; }

		[Column("FacilityID")]
		public long? FacilityId { get; set; }

		public long CreatedBy { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? CreatedOn { get; set; }

		public long? ModifiedBy { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? ModifiedOn { get; set; }

		public bool? IsDeleted { get; set; }
	}

}

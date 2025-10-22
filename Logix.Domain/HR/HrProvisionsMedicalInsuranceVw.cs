using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
	[Keyless]
	public partial class HrProvisionsMedicalInsuranceVw
	{
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

		public string? Description { get; set; }

		public long CreatedBy { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? CreatedOn { get; set; }

		public bool? IsDeleted { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? ModifiedOn { get; set; }

		public long? ModifiedBy { get; set; }

		[Column("Month_Code")]
		[StringLength(50)]
		public string? MonthCode { get; set; }

		[Column("Month_Name")]
		[StringLength(50)]
		public string? MonthName { get; set; }

		[Column("Facility_Name")]
		[StringLength(500)]
		public string? FacilityName { get; set; }

		[Column("Facility_Name2")]
		[StringLength(500)]
		public string? FacilityName2 { get; set; }

		[Column("Yearly_or_Monthly_Name")]
		[StringLength(250)]
		public string? YearlyOrMonthlyName { get; set; }

		[Column("Catagories_ID")]
		public int? CatagoriesId { get; set; }

		[Column("Month_ID")]
		public int? MonthId { get; set; }

		[Column("Yearly_or_Monthly")]
		public long? YearlyOrMonthly { get; set; }

		[Column("FacilityID")]
		public long? FacilityId { get; set; }
	}

}

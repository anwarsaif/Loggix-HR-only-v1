using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
	[Table("HR_Job")]
	public partial class HrJob
	{
		[Key]
		[Column("ID")]
		public long Id { get; set; }

		[Column("Job_No")]
		[StringLength(50)]
		public string? JobNo { get; set; }

		[Column("Job_Name")]
		[StringLength(255)]
		public string? JobName { get; set; }

		[Column("Level_ID")]
		public long? LevelId { get; set; }

		[Column("Status_ID")]
		public int? StatusId { get; set; }

		[Column("Dept_ID")]
		public long? DeptId { get; set; }

		[Column("Location_ID")]
		public long? LocationId { get; set; }

		[Column("Create_Date")]
		[StringLength(10)]
		public string? CreateDate { get; set; }

		[Column("Dec_No")]
		[StringLength(50)]
		public string? DecNo { get; set; }

		[Column("Dec_Date")]
		[StringLength(50)]
		public string? DecDate { get; set; }

		[Column("Dec_Source_ID")]
		public long? DecSourceId { get; set; }

		[Column("Job_Catagories_ID")]
		public int? JobCatagoriesId { get; set; }

		[Column("Facility_ID")]
		public long? FacilityId { get; set; }

		[Column("Branch_ID")]
		public long? BranchId { get; set; }

		public long CreatedBy { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime CreatedOn { get; set; }

		public long? ModifiedBy { get; set; }

		[Column(TypeName = "datetime")]
		public DateTime? ModifiedOn { get; set; }

		public bool IsDeleted { get; set; }

		public string? Note { get; set; }

		[Column("MOF_Code")]
		public string? MofCode { get; set; }

		[Column("Sector_ID")]
		public long? SectorId { get; set; }
	}

}

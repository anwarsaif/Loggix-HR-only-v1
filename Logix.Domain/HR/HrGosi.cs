using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_GOSI", Schema = "dbo")]
    public partial class HrGosi
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("T_Date")]
        [StringLength(50)]
        public string? TDate { get; set; }
        [Column("T_Month")]
        [StringLength(2)]
        public string? TMonth { get; set; }
        public int? FinancelYear { get; set; }
        [Column("Facility_ID")]
        public long? FacilityId { get; set; }
        [Column("Start_Date")]
        [StringLength(10)]
        public string? StartDate { get; set; }
        [Column("End_Date")]
        [StringLength(10)]
        public string? EndDate { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        [Column("Branch_ID")]
        public long? BranchId { get; set; }
    }
}

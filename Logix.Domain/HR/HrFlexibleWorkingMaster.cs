using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Flexible_Working_Master", Schema = "dbo")]
    public partial class HrFlexibleWorkingMaster
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [StringLength(50)]
        public string? Code { get; set; }
        [Column("Date_From")]
        [StringLength(10)]
        public string? DateFrom { get; set; }
        [Column("Date_To")]
        [StringLength(10)]
        public string? DateTo { get; set; }
        public string? Note { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
}

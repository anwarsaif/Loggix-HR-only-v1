using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_TimeZone")]
    public partial class HrTimeZone
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        [StringLength(255)]
        public string? DisplayName { get; set; }

        [StringLength(255)]
        public string? StandardName { get; set; }

        [Column("TimeZoneID")]
        [StringLength(255)]
        public string? TimeZoneId { get; set; }

        [StringLength(255)]
        public string? CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }

        [StringLength(255)]
        public string? ModifiedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }
    }

}

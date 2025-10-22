using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Card_Templates")]
    public partial class HrCardTemplate: TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [StringLength(500)]
        public string? Name { get; set; }
        [StringLength(500)]
        public string? ImgUrl { get; set; }
        public int? Status { get; set; }
        [Column("TxtXPosition", TypeName = "decimal(18, 0)")]
        public decimal? TxtXposition { get; set; }
        [Column("TxtYPosition", TypeName = "decimal(18, 0)")]
        public decimal? TxtYposition { get; set; }
        [StringLength(50)]
        public string? TxtSize { get; set; }
        [StringLength(50)]
        public string? TxtFont { get; set; }
        [StringLength(50)]
        public string? TxtColor { get; set; }
        [Column("Facility_ID")]
        public long? FacilityId { get; set; }
       
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Att_Location")]
    public partial class HrAttLocation:TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Location_name")]
        [StringLength(250)]
        public string? LocationName { get; set; }
        [Column("latitude")]
        [StringLength(2400)]
        public string? Latitude { get; set; }
        [Column("longitude")]
        [StringLength(2400)]
        public string? Longitude { get; set; }
        [Column("Project_ID")]
        public long? ProjectId { get; set; }
    }
}

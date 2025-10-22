using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrPermissionReasonVw
    {
        [Column("Reason_ID")]
        public long? ReasonId { get; set; }
        [Column("Reason_Name")]
        [StringLength(250)]
        public string? ReasonName { get; set; }
        [Column("Reason_Name2")]
        [StringLength(250)]
        public string? ReasonName2 { get; set; }
        [Column("Catagories_ID")]
        public int? CatagoriesId { get; set; }
        [Column("ISDEL")]
        public bool? Isdel { get; set; }
    }
}

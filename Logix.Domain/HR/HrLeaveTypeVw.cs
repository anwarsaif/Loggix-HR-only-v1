using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrLeaveTypeVw
    {
        [Column("Type_ID")]
        public int TypeId { get; set; }
        [Column("Type_name")]
        [StringLength(550)]
        public string? TypeName { get; set; }
        [Column("Type_name2")]
        [StringLength(550)]
        public string? TypeName2 { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("Parent_ID")]
        public long? ParentId { get; set; }
    }
}

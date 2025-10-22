using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrPermissionTypeVw
    {
        [Column("Type_ID")]
        public long? TypeId { get; set; }
        [Column("Type_Name")]
        [StringLength(250)]
        public string? TypeName { get; set; }
        [Column("Type_Name2")]
        [StringLength(250)]
        public string? TypeName2 { get; set; }
        [Column("Catagories_ID")]
        public int? CatagoriesId { get; set; }
        [Column("ISDEL")]
        public bool? Isdel { get; set; }
    }
}

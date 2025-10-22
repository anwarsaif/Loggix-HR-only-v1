using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrJobProgramVw
    {
        [Column("Program_ID")]
        public long? ProgramId { get; set; }
        [Column("Program_Name")]
        [StringLength(250)]
        public string? ProgramName { get; set; }
        [Column("ID")]
        public long Id { get; set; }
        [Column("Catagories_ID")]
        public int? CatagoriesId { get; set; }
        [Column("ISDEL")]
        public bool? Isdel { get; set; }
        [Column("USER_ID")]
        public long? UserId { get; set; }
        [Column("Sort_no")]
        public int? SortNo { get; set; }
        [StringLength(50)]
        public string? Note { get; set; }
        [Column("Refrance_No")]
        [StringLength(250)]
        public string? RefranceNo { get; set; }
        [Column("Color_ID")]
        public int? ColorId { get; set; }
        [StringLength(250)]
        public string? Icon { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Increment_Type", Schema = "dbo")]
    public partial class HrIncrementType
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }
        [StringLength(50)]
        public string? Name2 { get; set; }
    }
}

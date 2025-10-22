using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Opening_Balance_Type")]
    public partial class HrOpeningBalanceType
    {
        [Key]
        [Column("Type_ID")]
        public int TypeId { get; set; }
        [Column("Type_Name")]
        [StringLength(50)]
        public string? TypeName { get; set; }
        [Column("Type_Name2")]
        [StringLength(50)]
        public string? TypeName2 { get; set; }
        public bool? IsDeleted { get; set; }
    }
}

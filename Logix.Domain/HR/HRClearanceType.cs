using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
    [Table("HR_Clearance_Type")]
    public partial class HrClearanceType
    {
        [Key]
        [Column("Type_ID")]
        public int TypeId { get; set; }
        [Column("Type_name")]
        [StringLength(50)]
        public string? TypeName { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("Type_name2")]
        [StringLength(50)]
        public string? TypeName2 { get; set; }

    }
}

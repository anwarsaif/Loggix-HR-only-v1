using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrClearanceTypeVw
    {
        [Column("Type_ID")]
        public int TypeId { get; set; }
        [Column("Type_name")]
        [StringLength(50)]
        public string? TypeName { get; set; }
        [Column("Type_name2")]
        [StringLength(50)]
        public string? TypeName2 { get; set; }
        public bool? IsDeleted { get; set; }
    }
}

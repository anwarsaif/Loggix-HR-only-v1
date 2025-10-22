using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
    [Table("HR_Custody_Type")]

    public class HrCustodyType
    {

        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }
        [StringLength(50)]
        public string? Name2 { get; set; }
        [Column("Parent_ID")]
        public int? ParentId { get; set; }
    }
}

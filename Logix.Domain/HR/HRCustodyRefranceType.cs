using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
    [Table("HR_Custody_Refrance_Type")]

    public class HrCustodyRefranceType
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(50)]
        public string? Name { get; set; }
    }
}

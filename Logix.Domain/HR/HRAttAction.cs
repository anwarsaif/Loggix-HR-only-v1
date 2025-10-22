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
    [Table("HR_Att_Action")]
    public partial class HrAttAction
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Time { get; set; }
        [Column("Type_ID")]
        public int? TypeId { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("SENSORID")]
        [StringLength(5)]
        [Unicode(false)]
        public string? Sensorid { get; set; }
        [StringLength(30)]
        [Unicode(false)]
        public string? Memoinfo { get; set; }
        [StringLength(24)]
        [Unicode(false)]
        public string? WorkCode { get; set; }
        [Column("sn")]
        [StringLength(20)]
        [Unicode(false)]
        public string? Sn { get; set; }
        public short? UserExtFmt { get; set; }
        public int? IsManual { get; set; }
    }
}

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Logix.Domain.Base;

namespace Logix.Domain.HR
{
    [Table("HR_OverTime_D")]
    [Index("IdM", Name = "NonClusteredIndex-20200316-123904")]
    public partial class HrOverTimeD:TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("ID_M")]
        public long? IdM { get; set; }
        [Column("OverTime_Tybe")]
        public int? OverTimeTybe { get; set; }
        [Column("OverTime_H_Cost", TypeName = "decimal(18, 2)")]
        public decimal? OverTimeHCost { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Hours { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Amount { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Total { get; set; }
        [Column("Currency_ID")]
        public int? CurrencyId { get; set; }
        public string? Description { get; set; }
    
        [Column("OverTime_Date")]
        [StringLength(10)]
        public string? OverTimeDate { get; set; }
    }
}

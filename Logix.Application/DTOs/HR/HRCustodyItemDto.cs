using Logix.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR
{
    public class HrCustodyItemDto : TraceEntity
    {
        [Column("ID")]
        public long Id { get; set; }
        public long? CustodyId { get; set; }
        public long? ItemId { get; set; }
        public long? UnitId { get; set; }
        [Column("Qty_In", TypeName = "decimal(18, 2)")]
        public decimal? QtyIn { get; set; }
        [Column("Qty_Out", TypeName = "decimal(18, 2)")]
        public decimal? QtyOut { get; set; }
        public string? SerialNumber { get; set; }
        public string? Note { get; set; }

      

        public int? ReasonId { get; set; }
        [Column("Custody_Date")]
        [StringLength(10)]
        public string? CustodyDate { get; set; }
    } 
    public class HrCustodyItemEditDto
    {
        [Column("ID")]
        public long Id { get; set; }
        public long? CustodyId { get; set; }
        public long? ItemId { get; set; }
        public long? UnitId { get; set; }
        [Column("Qty_In", TypeName = "decimal(18, 2)")]
        public decimal? QtyIn { get; set; }
        [Column("Qty_Out", TypeName = "decimal(18, 2)")]
        public decimal? QtyOut { get; set; }
        public string? SerialNumber { get; set; }
        public string? Note { get; set; }

        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public int? ReasonId { get; set; }
        [Column("Custody_Date")]
        [StringLength(10)]
        public string? CustodyDate { get; set; }
    }
}

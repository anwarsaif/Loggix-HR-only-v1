using Logix.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Domain.HR
{
    [Table("HR_Custody_Items")]

    public class HrCustodyItem:TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Custody_ID")]
        public long? CustodyId { get; set; }
        [Column("Item_ID")]
        public long? ItemId { get; set; }
        [Column("Unit_ID")]
        public long? UnitId { get; set; }
        [Column("Qty_In", TypeName = "decimal(18, 2)")]
        public decimal? QtyIn { get; set; }
        [Column("Qty_Out", TypeName = "decimal(18, 2)")]
        public decimal? QtyOut { get; set; }
        [Column("Serial_Number")]
        public string? SerialNumber { get; set; }
        public string? Note { get; set; }
      
        [Column("Reason_ID")]
        public int? ReasonId { get; set; }
        [Column("Custody_Date")]
        [StringLength(10)]
        public string? CustodyDate { get; set; }
    }
}

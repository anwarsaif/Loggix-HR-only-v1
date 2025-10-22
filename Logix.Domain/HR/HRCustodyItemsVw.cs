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

    public class HrCustodyItemsVw
    {
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
        public bool IsDeleted { get; set; }
        [Column("Item_Code")]
        [StringLength(250)]
        public string? ItemCode { get; set; }
        [Column("Item_Name")]
        [StringLength(2500)]
        public string? ItemName { get; set; }
        [StringLength(150)]
        public string? UnitName { get; set; }
    }
}

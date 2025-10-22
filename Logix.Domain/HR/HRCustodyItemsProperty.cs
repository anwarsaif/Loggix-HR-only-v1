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
    [Table("HR_Custody_Items_Property")]

    public class HrCustodyItemsProperty:TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Custody_Item_ID")]
        public long? CustodyItemId { get; set; }
        [Column("Item_ID")]
        public long? ItemId { get; set; }
        [Column("Property_ID")]
        public long? PropertyId { get; set; }
        [Column("Property_Value")]
        public bool? PropertyValue { get; set; }
        public string? Note { get; set; }
       


    }
}

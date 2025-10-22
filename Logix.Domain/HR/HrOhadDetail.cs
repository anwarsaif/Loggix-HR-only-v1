using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_OhadDetails")]
    public partial class HrOhadDetail : TraceEntity
    {
        [Key]
        [Column("Ohad_Det_Id")]
        public long OhadDetId { get; set; }
        public long? OhdaId { get; set; }
        public long? ItemId { get; set; }
        [StringLength(500)]
        public string? ItemName { get; set; }
        [StringLength(150)]
        public string? ItemState { get; set; }
        [StringLength(1500)]
        public string? ItemDetails { get; set; }
        [StringLength(50)]
        public string? Note { get; set; }
        [Column("Qty_in", TypeName = "decimal(18, 2)")]
        public decimal? QtyIn { get; set; }
        [Column("Qty_Out", TypeName = "decimal(18, 2)")]
        public decimal? QtyOut { get; set; }
        [Column("ItemState_ID")]
        public int? ItemStateId { get; set; }
        [Column("Orgnal_Id")]
        public long? OrgnalId { get; set; }
    }
}

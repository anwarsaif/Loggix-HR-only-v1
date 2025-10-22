using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_KPI_Type")]
    public partial class HrKpiType
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }
        [StringLength(250)]
        public string? Name { get; set; }
        [StringLength(250)]
        public string? Name2 { get; set; }
        public bool? Isdeleted { get; set; }
        [Column("System_ID")]
        public int? SystemId { get; set; }
    }

}

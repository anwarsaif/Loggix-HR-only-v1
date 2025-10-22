using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrGosiTypeVw
    {
        [Column("Gosi_Type")]
        public long? GosiType { get; set; }
        [Column("Gosi_TypeName")]
        [StringLength(250)]
        public string? GosiTypeName { get; set; }
        [Column("Gosi_TypeName2")]
        [StringLength(250)]
        public string? GosiTypeName2 { get; set; }
        [Column("Sort_no")]
        public int? SortNo { get; set; }
    }
}

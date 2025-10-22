using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{

    [Keyless]
    public partial class HrDecisionsTypeVw
    {
        [Column("Dec_Type_ID")]
        public long? DecTypeId { get; set; }

        [Column("Dec_Type_Name")]
        [StringLength(250)]
        public string? DecTypeName { get; set; }

        [Column("Dec_Type_Name2")]
        [StringLength(250)]
        public string? DecTypeName2 { get; set; }

        [Column("ID")]
        public long Id { get; set; }

        [Column("Catagories_ID")]
        public int? CatagoriesId { get; set; }

        [Column("ISDEL")]
        public bool? Isdel { get; set; }

        [Column("USER_ID")]
        public long? UserId { get; set; }

        [Column("Sort_no")]
        public int? SortNo { get; set; }

        public string? Note { get; set; }

        [Column("Refrance_No")]
        [StringLength(250)]
        public string? RefranceNo { get; set; }

        [Column("Color_ID")]
        public int? ColorId { get; set; }

        [StringLength(250)]
        public string? Icon { get; set; }

        [Column("Acc_Account_ID")]
        public long? AccAccountId { get; set; }

        [Column("CC_ID")]
        public long? CcId { get; set; }
    }
}

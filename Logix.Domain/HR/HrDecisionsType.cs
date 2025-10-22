using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Decisions_Type")]
    public partial class HrDecisionsType
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Dec_Type")]
        public string? DecType { get; set; }
        [Column("Ref_Type_ID")]
        public int? RefTypeId { get; set; }
        public string? Note { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
    }
}

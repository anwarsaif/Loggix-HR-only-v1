using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrTrainingBagVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Type_ID")]
        public int? TypeId { get; set; }
        [Column("Type_Name")]
        [StringLength(250)]
        public string? TypeName { get; set; }
        [StringLength(2500)]
        public string? Name { get; set; }
        [StringLength(2500)]
        public string? Name2 { get; set; }
        public bool? IsActive { get; set; }
        public string? Note { get; set; }
        [Column("File_URL")]
        public string? FileUrl { get; set; }
        public bool IsDeleted { get; set; }
    }
}

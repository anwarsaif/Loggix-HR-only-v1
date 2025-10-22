using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrVacancyStatusVw
    {
        [Column("Status_ID")]
        public long? StatusId { get; set; }
        [Column("Status_Name")]
        [StringLength(250)]
        public string? StatusName { get; set; }
        [Column("Catagories_ID")]
        public int? CatagoriesId { get; set; }
        [Column("ISDEL")]
        public bool? Isdel { get; set; }
    }
}

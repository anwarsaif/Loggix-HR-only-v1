using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrPerformanceForVw
    {
        [Column("Evaluation_ID")]
        public long? EvaluationId { get; set; }
        [Column("Evaluation_Name")]
        [StringLength(250)]
        public string? EvaluationName { get; set; }
        [Column("Evaluation_Name2")]
        [StringLength(250)]
        public string? EvaluationName2 { get; set; }
        [Column("Catagories_ID")]
        public int? CatagoriesId { get; set; }
        [Column("ISDEL")]
        public bool? Isdel { get; set; }
    }
}

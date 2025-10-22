using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    [Table("HR_Vacations_Day_Type", Schema = "dbo")]
    public partial class HrVacationsDayType
    {
        [Column("ID")]
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Name2 { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Value { get; set; }
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

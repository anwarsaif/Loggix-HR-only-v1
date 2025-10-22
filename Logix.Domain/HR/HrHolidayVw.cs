using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrHolidayVw
    {
        [Column("holidayID")]
        public long HolidayId { get; set; }
        [Column("holidayName")]
        [StringLength(250)]
        public string? HolidayName { get; set; }
        [Column("holidayDateFrom")]
        [StringLength(50)]
        public string? HolidayDateFrom { get; set; }
        [Column("holidayDateTo")]
        [StringLength(50)]
        public string? HolidayDateTo { get; set; }
        public long? CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        public long? ModifiedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("Facility_ID")]
        public long? FacilityId { get; set; }
    }
}

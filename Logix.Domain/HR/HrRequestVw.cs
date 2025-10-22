using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrRequestVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("RE_Date")]
        [StringLength(10)]
        public string? ReDate { get; set; }
        [Column("Branch_ID")]
        public long? BranchId { get; set; }
        [Column("Facility_ID")]
        public long? FacilityId { get; set; }
        [Column("App_ID")]
        public long? AppId { get; set; }
        [Column("Status_ID")]
        public int? StatusId { get; set; }
        [Column("EMP_ID")]
        public long? EmpId { get; set; }
        [Column("Request_Type")]
        public long? RequestType { get; set; }
        [Column("Date_From")]
        [StringLength(10)]
        public string? DateFrom { get; set; }
        [Column("Date_To")]
        [StringLength(10)]
        public string? DateTo { get; set; }
        public string? Subject { get; set; }
        public string? Note { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("BRA_NAME")]
        public string? BraName { get; set; }
        [Column("Request_Name")]
        [StringLength(250)]
        public string? RequestName { get; set; }
        [Column("Status_Name")]
        [StringLength(50)]
        public string? StatusName { get; set; }
        [Column("Application_Code")]
        public long? ApplicationCode { get; set; }
    }
}

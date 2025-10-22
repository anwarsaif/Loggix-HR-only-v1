using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Request_Detailes")]
    public partial class HrRequestDetaile:TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }
        [Column("Request_ID")]
        public long? RequestId { get; set; }
        [Column("EMP_ID")]
        public long? EmpId { get; set; }
        [Column("Request_Type")]
        public long? RequestType { get; set; }
        [Column("Allownce_ID")]
        public long? AllownceId { get; set; }
        [Column("Deduction_ID")]
        public long? DeductionId { get; set; }
        [Column("OverTime_ID")]
        public long? OverTimeId { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Value { get; set; }
        [Column("ID_No")]
        [StringLength(50)]
        public string? IdNo { get; set; }
        [Column("ID_Expire_Date")]
        [StringLength(10)]
        public string? IdExpireDate { get; set; }
        [Column("Pass_Expire_Date")]
        [StringLength(10)]
        public string? PassExpireDate { get; set; }
        public string? Description { get; set; }
        [Column("Absence_Type_Id")]
        public long? AbsenceTypeId { get; set; }
        [Column("Absence_Date")]
        [StringLength(10)]
        public string? AbsenceDate { get; set; }
        /// <summary>
        /// /////// new properties
        /// </summary>
        public long? RegularHours { get; set; }

        public long? RegularDays { get; set; }

        public long? Holidays { get; set; }

        public long? HolidayHours { get; set; }

        [StringLength(10)]
        public string? StartDate { get; set; }

        [StringLength(10)]
        public string? EndDate { get; set; }
    }
}

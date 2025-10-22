using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrRequestDetailesVw
    {
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
        public bool? IsDeleted { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("Dep_Name")]
        [StringLength(200)]
        public string? DepName { get; set; }
        [Column("Cat_name")]
        [StringLength(250)]
        public string? CatName { get; set; }
        [Column("Request_Name")]
        [StringLength(250)]
        public string? RequestName { get; set; }
        [Column("Allownce_Name")]
        [StringLength(250)]
        public string? AllownceName { get; set; }
        [Column("Deduction_Name")]
        [StringLength(250)]
        public string? DeductionName { get; set; }
        [Column("OverTime_Name")]
        [StringLength(250)]
        public string? OverTimeName { get; set; }
        [Column("BRA_NAME")]
        public string? BraName { get; set; }
        [Column("Absence_Type_Id")]
        public long? AbsenceTypeId { get; set; }
        [Column("Absence_Date")]
        [StringLength(10)]
        public string? AbsenceDate { get; set; }
        [Column("Absence_Type_Name")]
        [StringLength(250)]
        public string? AbsenceTypeName { get; set; }
        [Column("Absence_Type_Name2")]
        [StringLength(250)]
        public string? AbsenceTypeName2 { get; set; }

        /// new properties
        /// 
        public long? RegularHours { get; set; }

        public long? RegularDays { get; set; }

        public long? Holidays { get; set; }

        public long? HolidayHours { get; set; }

        [StringLength(10)]
        public string? StartDate { get; set; }

        [StringLength(10)]
        public string? EndDate { get; set; }

        [Column("Location_Name")]
        [StringLength(200)]
        public string? LocationName { get; set; }

        [Column("Location_Name2")]
        [StringLength(200)]
        public string? LocationName2 { get; set; }
    }
}

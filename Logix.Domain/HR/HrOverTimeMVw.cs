using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrOverTimeMVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("Date_From")]
        [StringLength(10)]
        public string? DateFrom { get; set; }
        [Column("Date_To")]
        [StringLength(10)]
        public string? DateTo { get; set; }
        [Column("Date_Tran")]
        [StringLength(10)]
        public string? DateTran { get; set; }
        [Column("Refrance_ID")]
        [StringLength(50)]
        public string? RefranceId { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("Payment_Type")]
        public int? PaymentType { get; set; }
        public string? Note { get; set; }
        [Column("Cnt_Hours_Total", TypeName = "decimal(18, 2)")]
        public decimal? CntHoursTotal { get; set; }
        [Column("Cnt_Hours_Day", TypeName = "decimal(18, 2)")]
        public decimal? CntHoursDay { get; set; }
        [Column("Cnt_Hours_Month", TypeName = "decimal(18, 2)")]
        public decimal? CntHoursMonth { get; set; }
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("IBAN")]
        [StringLength(50)]
        public string? Iban { get; set; }
        [Column("Bank_Name")]
        [StringLength(250)]
        public string? BankName { get; set; }
        [Column("BRA_NAME")]
        public string? BraName { get; set; }
        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }
        [Column("Nationality_Name")]
        [StringLength(250)]
        public string? NationalityName { get; set; }
        [Column("Dep_Name")]
        [StringLength(200)]
        public string? DepName { get; set; }
        [Column("Location_Name")]
        [StringLength(200)]
        public string? LocationName { get; set; }
        [Column("Facility_Name")]
        [StringLength(500)]
        public string? FacilityName { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column("Dept_ID")]
        public int? DeptId { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        public int? Location { get; set; }
        [Column("Bank_ID")]
        public int? BankId { get; set; }
        [Column("Account_No")]
        [StringLength(50)]
        public string? AccountNo { get; set; }
        [Column("Facility_ID")]
        public int? FacilityId { get; set; }
        [Column("Project_ID")]
        public long? ProjectId { get; set; }
        [Column("Project_Code")]
        public long? ProjectCode { get; set; }
        [Column("Project_Name")]
        [StringLength(2500)]
        public string? ProjectName { get; set; }
    }
}

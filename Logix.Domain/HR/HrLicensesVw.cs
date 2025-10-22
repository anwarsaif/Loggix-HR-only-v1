using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrLicensesVw
    {
        [Column("ID")]
        public long Id { get; set; }
        [Column("Emp_ID")]
        public long? EmpId { get; set; }
        [Column("License_Type")]
        public int? LicenseType { get; set; }
        [Column("License_No")]
        [StringLength(50)]
        public string? LicenseNo { get; set; }
        [Column("Issued_Date")]
        [StringLength(10)]
        public string? IssuedDate { get; set; }
        [Column("Expiry_Date")]
        [StringLength(10)]
        public string? ExpiryDate { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("License_Type_Name")]
        [StringLength(250)]
        public string? LicenseTypeName { get; set; }
        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }
        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }
        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;
        public string? Note { get; set; }
        [Column("License_Former_Place")]
        [StringLength(50)]
        public string? LicenseFormerPlace { get; set; }
        [Column("Type_and_Name")]
        [StringLength(503)]
        public string? TypeAndName { get; set; }
        [Column("Type_and_NameEn")]
        [StringLength(503)]
        public string? TypeAndNameEn { get; set; }
        public int? JobCat { get; set; }
        [Column("File_URL")]
        [StringLength(250)]
        public string? FileUrl { get; set; }
        [Column("BRANCH_ID")]
        public int? BranchId { get; set; }
        [Column("Status_ID")]
        public int? StatusId { get; set; }
        [Column("License_Type_Name2")]
        [StringLength(250)]
        public string? LicenseTypeName2 { get; set; }
        [Column("BRA_NAME2")]
        public string? BraName2 { get; set; }
        [Column("Status_Name2")]
        [StringLength(250)]
        public string? StatusName2 { get; set; }
        [Column("Facility_Name2")]
        [StringLength(500)]
        public string? FacilityName2 { get; set; }
        [Column("Qualification_Name2")]
        [StringLength(250)]
        public string? QualificationName2 { get; set; }
        [Column("Nationality_Name2")]
        [StringLength(250)]
        public string? NationalityName2 { get; set; }
        [Column("Cat_name")]
        [StringLength(250)]
        public string? CatName { get; set; }
        [Column("Dep_Name2")]
        [StringLength(200)]
        public string? DepName2 { get; set; }
    }
}

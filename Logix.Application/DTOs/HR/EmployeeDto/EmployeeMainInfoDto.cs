using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logix.Application.DTOs.HR.EmployeeDto
{
    public class EmployeeMainInfoDto
    {
        [Column("ID")]
        public long Id { get; set; }

        [Column("Emp_ID")]
        [StringLength(50)]
        public string EmpId { get; set; } = null!;

        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }

        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }

        [Column("ID_No")]
        [StringLength(50)]
        public string? IdNo { get; set; }
        [Column("Nationality_ID")]
        public int? NationalityId { get; set; }

        [Column("Marital_Status")]
        public int? MaritalStatus { get; set; }

        public int? Gender { get; set; }

        [Column("Emp_Code2")]
        [StringLength(50)]
        public string? EmpCode2 { get; set; }

        [Column("ID_Issuer")]
        [StringLength(250)]
        public string? IdIssuer { get; set; }

        [Column("ID_Issuer_Date")]
        [StringLength(10)]
        public string? IdIssuerDate { get; set; }

        [Column("ID_Expire_Date")]
        [StringLength(10)]
        public string? IdExpireDate { get; set; }

        [StringLength(10)]
        public string? BirthDate { get; set; }

        [Column("Birth_Place")]
        [StringLength(500)]
        public string? BirthPlace { get; set; }

        [Column("Passport_No")]
        [StringLength(50)]
        public string? PassportNo { get; set; }

        [Column("Pass_Issuer_Date")]
        [StringLength(50)]
        public string? PassIssuerDate { get; set; }

        [Column("Pass_Expire_Date")]
        [StringLength(50)]
        public string? PassExpireDate { get; set; }

        [Column("Entry_NO")]
        [StringLength(50)]
        public string? EntryNo { get; set; }

        [Column("Entry_Date")]
        [StringLength(10)]
        public string? EntryDate { get; set; }

        [Column("Entry_Port")]
        [StringLength(50)]
        public string? EntryPort { get; set; }

        [Column("Occupation_ID")]
        [StringLength(50)]
        public string? OccupationId { get; set; }

        [Column("Religion_ID")]
        public int? ReligionId { get; set; }

        [Column("Card_Expiration_Date")]
        [StringLength(10)]
        public string? CardExpirationDate { get; set; }

        [Column("Visa_No")]
        [StringLength(50)]
        public string? VisaNo { get; set; }

        public int? BloodType { get; set; }
    }
}

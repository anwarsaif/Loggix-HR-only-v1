using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logix.Application.DTOs.HR.EmployeeDto
{
    public class EmployeeContactInfoDto
    {
        public long Id { get; set; }
        [Column("Postal_Code")]
        [StringLength(20)]
        public string? PostalCode { get; set; }

        [Column("POBox")]
        [StringLength(20)]
        public string? Pobox { get; set; }

        [Column("Home_Phone")]
        [StringLength(20)]
        public string? HomePhone { get; set; }

        [Column("Office_Phone")]
        [StringLength(20)]
        public string? OfficePhone { get; set; }

        [Column("Office_Phone_Ex")]
        [StringLength(20)]
        public string? OfficePhoneEx { get; set; }

        [StringLength(20)]
        public string? Mobile { get; set; }

        [StringLength(50)]
        public string? Email { get; set; }

        [StringLength(50)]
        public string? Email2 { get; set; }

        public int? SponsorsId { get; set; }

        [StringLength(50)]
        public string? PhoneCountry { get; set; }


        [StringLength(250)]
        public string? AddressCountry { get; set; }

        [StringLength(250)]
        public string? Address { get; set; }
        public bool? ShareContactInfo { get; set; }

        public string? DirectPhoneNumber { get; set; }
    }

}

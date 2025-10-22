using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR.EmployeeDto
{
    public class EmployeeMedicalInsuranceDto
    {
        public long Id { get; set; }

        //فئة التأمين
        public int? InsuranceCategory { get; set; }

        //صلاحية التأمين
        [StringLength(10)]
        public string? InsuranceDateValidity { get; set; }
        // شركة التأمين
        public int? InsuranceCompany { get; set; }

        // رقم بطاقة التأمين
        [StringLength(50)]
        public string? InsuranceCardNo { get; set; }
    }

}

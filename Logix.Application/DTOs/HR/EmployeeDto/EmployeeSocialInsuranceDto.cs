using Castle.MicroKernel.SubSystems.Conversion;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Logix.Application.DTOs.HR.EmployeeDto
{
    public class EmployeeSocialInsuranceDto
    {
        public long Id { get; set; }
        //رقم المشترك في التأمينات الاجتماعية

        [StringLength(50)]
        public string? GosiNo { get; set; }
        //        تاريخ الالتحاق بالتأمينات الاجتماعية
        [StringLength(10)]
        public string? GosiDate { get; set; }
        //الراتب الأساسي
        [Column("Gosi_Bisc_Salary", TypeName = "decimal(18, 2)")]
        public decimal? GosiBiscSalary { get; set; }
        //بدل سكن
        [Column("Gosi_House_Allowance", TypeName = "decimal(18, 2)")]
        public decimal? GosiHouseAllowance { get; set; }
        //بدل عمولات
        [Column("Gosi_Allowance_Commission", TypeName = "decimal(18, 2)")]
        public decimal? GosiAllowanceCommission { get; set; }
        // الأجر الخاضع للإشتراك

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? GosiSalary { get; set; }

        // بدلات اخرى

        [Column("Gosi_Other_Allowances", TypeName = "decimal(18, 2)")]
        public decimal? GosiOtherAllowances { get; set; }
        //  تاريخ استبعاد الموظف
        [Column("Gois_Subscription_Expiry_Date")]
        [StringLength(10)]
        public string? GoisSubscriptionExpiryDate { get; set; }
        // رقم مكتب العمل

        [StringLength(50)]
        public string? WorkNo { get; set; }
        //نسبة تحمل الشركة 

        [Column("Gosi_Rate_Facility", TypeName = "decimal(18, 2)")]
        public decimal? GosiRateFacility { get; set; }
        // نوع التأمينات

        [Column("Gosi_Type")]
        public int? GosiType { get; set; }
        public decimal? SalaryInsuranceWage { get; set; }

    }

}

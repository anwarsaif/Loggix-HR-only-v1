using Logix.Application.DTOs.Main;
using Logix.Domain.HR;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logix.Application.DTOs.HR.EmployeeDto
{
    public class EmployeeSalaryInfoDto
    {
        public long Id { get; set; }

        //نوع التعاقد
        [Column("Contract_Type_ID")]
        public int? ContractTypeId { get; set; }
        //نوع مسير الرواتب 
        public int? PayrollType { get; set; }
        //الراتب
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Salary { get; set; }
        //التحويل السريع
        [Column("Direct_Deposit")]
        public bool? DirectDeposit { get; set; }
        //عليه قرض

        [Column("Have_Bank_Loan")]
        public bool? HaveBankLoan { get; set; }
        //ايقاف الراتب
        [Column("Stop_salary")]
        public bool? StopSalary { get; set; }

        //سبب الإيقاف
        [Column("Stop_Salary_Code")]
        public int? StopSalaryCode { get; set; }
        //تاريخ الإيقاف
        [Column("Stop_Date_Salary")]
        [StringLength(12)]
        public string? StopDateSalary { get; set; }
        //البنك
        [Column("Bank_ID")]
        public int? BankId { get; set; }
        //رقم الحساب
        [Column("Account_No")]
        [StringLength(50)]
        public string? AccountNo { get; set; }
        //رقم الاي بان
        [Column("IBAN")]
        [StringLength(50)]
        public string? Iban { get; set; }
        //عدد ساعات العمل اليومية
        [Column("Daily_Working_hours", TypeName = "decimal(18, 2)")]
        public decimal? DailyWorkingHours { get; set; }
        //عدد ايام الإجازة السنوية العادية
        [Column("Vacation_Days_Year")]
        public int? VacationDaysYear { get; set; }
        //عدد ايام الإجازة السنوية الاضطرارية
        [Column("Vacation2_Days_Year", TypeName = "decimal(18, 2)")]
        public decimal? Vacation2DaysYear { get; set; }
        //طريقة الدفع 
        [Column("Payment_Type_ID")]
        public int? PaymentTypeId { get; set; }
        //حماية الاجور 
        [Column("Wages_Protection")]
        public int? WagesProtection { get; set; }
        //تاريخ اخر علاوة

        [StringLength(10)]
        public string? LastIncrementDate { get; set; }

        //تاريخ اخر ترقية

        [StringLength(10)]
        public string? LastPromotionDate { get; set; }
        //تكلفة الساعة 
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? HourCost { get; set; }

        public List<EditEmployeeSalaryInfo>? allowances { get; set; }
        public List<EditEmployeeSalaryInfo>? deduction { get; set; }

        //public List<IncrementAllowanceDeductionDto>? allowancesList { get; set; }
        //public List<IncrementAllowanceDeductionDto>? deductionsList { get; set; }

        //نوع البدل 

        // نسبة البدل 

        //مبلغ البدل  

        //نوع الحسم 


        //نسبه الحسم 

        //مبلغ الحسم 
    }

}

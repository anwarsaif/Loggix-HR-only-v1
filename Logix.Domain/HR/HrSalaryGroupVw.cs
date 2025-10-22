using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrSalaryGroupVw
    {
        [Column("ID")]
        public long Id { get; set; }

        [StringLength(250)]
        public string? Name { get; set; }

        [Column("Facility_ID")]
        public long? FacilityId { get; set; }

        [Column("Account_Salary_ID")]
        public long? AccountSalaryId { get; set; }

        [Column("Account_Allowances_ID")]
        public long? AccountAllowancesId { get; set; }

        [Column("Account_OverTime_ID")]
        public long? AccountOverTimeId { get; set; }

        [Column("Account_Deduction_ID")]
        public long? AccountDeductionId { get; set; }

        public bool? IsDeleted { get; set; }

        [Column("Account_Salary_Code")]
        [StringLength(50)]
        public string? AccountSalaryCode { get; set; }

        [Column("Account_Salary_Name")]
        [StringLength(255)]
        public string? AccountSalaryName { get; set; }

        [Column("Account_Allowances_Code")]
        [StringLength(50)]
        public string? AccountAllowancesCode { get; set; }

        [Column("Account_Allowances_Name")]
        [StringLength(255)]
        public string? AccountAllowancesName { get; set; }

        [Column("Account_OverTime_Code")]
        [StringLength(50)]
        public string? AccountOverTimeCode { get; set; }

        [Column("Account_OverTime_Name")]
        [StringLength(255)]
        public string? AccountOverTimeName { get; set; }

        [Column("Account_Deduction_Code")]
        [StringLength(50)]
        public string? AccountDeductionCode { get; set; }

        [Column("Account_Deduction_Name")]
        [StringLength(255)]
        public string? AccountDeductionName { get; set; }

        [Column("Account_Due_Salary_ID")]
        public long? AccountDueSalaryId { get; set; }

        [Column("Account_Due_Salary_Name")]
        [StringLength(255)]
        public string? AccountDueSalaryName { get; set; }

        [Column("Account_Due_Salary_Code")]
        [StringLength(50)]
        public string? AccountDueSalaryCode { get; set; }

        [Column("Account_Loan_ID")]
        public long? AccountLoanId { get; set; }

        [Column("Account_Loan_Code")]
        [StringLength(50)]
        public string? AccountLoanCode { get; set; }

        [Column("Account_Loan_Name")]
        [StringLength(255)]
        public string? AccountLoanName { get; set; }

        [Column("Account_Ohad_ID")]
        public long? AccountOhadId { get; set; }

        [Column("Account_Ohad_Code")]
        [StringLength(50)]
        public string? AccountOhadCode { get; set; }

        [Column("Account_Ohad_Name")]
        [StringLength(255)]
        public string? AccountOhadName { get; set; }

        [Column("Account_Tickets_ID")]
        public long? AccountTicketsId { get; set; }

        [Column("Account_Tickets_Code")]
        [StringLength(50)]
        public string? AccountTicketsCode { get; set; }

        [Column("Account_Tickets_Name")]
        [StringLength(255)]
        public string? AccountTicketsName { get; set; }

        [Column("Account_Vacation_salary_ID")]
        public long? AccountVacationSalaryId { get; set; }

        [Column("Account_Vacation_salary_Code")]
        [StringLength(50)]
        public string? AccountVacationSalaryCode { get; set; }

        [Column("Account_Vacation_salary_Name")]
        [StringLength(255)]
        public string? AccountVacationSalaryName { get; set; }

        [Column("Account_End_Service_ID")]
        public long? AccountEndServiceId { get; set; }

        [Column("Account_End_Service_Code")]
        [StringLength(50)]
        public string? AccountEndServiceCode { get; set; }

        [Column("Account_End_Service_Name")]
        [StringLength(255)]
        public string? AccountEndServiceName { get; set; }

        [Column("Account_Due_Tickets_Name")]
        [StringLength(255)]
        public string? AccountDueTicketsName { get; set; }

        [Column("Account_Due_Tickets_Code")]
        [StringLength(50)]
        public string? AccountDueTicketsCode { get; set; }

        [Column("Account_Due_Tickets_ID")]
        public long? AccountDueTicketsId { get; set; }

        [Column("Account_Due_End_Service_ID")]
        public long? AccountDueEndServiceId { get; set; }

        [Column("Account_Due_Vacation_ID")]
        public long? AccountDueVacationId { get; set; }

        [Column("Account_Due_End_Service_Name")]
        [StringLength(255)]
        public string? AccountDueEndServiceName { get; set; }

        [Column("Account_Due_End_Service_Code")]
        [StringLength(50)]
        public string? AccountDueEndServiceCode { get; set; }

        [Column("Account_Due_Vacation_Name")]
        [StringLength(255)]
        public string? AccountDueVacationName { get; set; }

        [Column("Account_Due_Vacation_Code")]
        [StringLength(50)]
        public string? AccountDueVacationCode { get; set; }

        [Column("Account_GOSI_Code")]
        [StringLength(50)]
        public string? AccountGosiCode { get; set; }

        [Column("Account_GOSI_Name")]
        [StringLength(255)]
        public string? AccountGosiName { get; set; }

        [Column("Account_GOSI_Name2")]
        [StringLength(255)]
        public string? AccountGosiName2 { get; set; }

        [Column("Account_GOSI_ID")]
        public long? AccountGosiId { get; set; }

        [Column("Account_Due_GOSI_ID")]
        public long? AccountDueGosiId { get; set; }

        [Column("Account_Due_GOSI_Name")]
        [StringLength(255)]
        public string? AccountDueGosiName { get; set; }

        [Column("Account_Due_GOSI_Name2")]
        [StringLength(255)]
        public string? AccountDueGosiName2 { get; set; }

        [Column("Account_Due_GOSI_Code")]
        [StringLength(50)]
        public string? AccountDueGosiCode { get; set; }

        [Column("Account_Mandate_ID")]
        public long? AccountMandateId { get; set; }

        [Column("Account_Due_Mandate_ID")]
        public long? AccountDueMandateId { get; set; }

        [Column("Account_Due_Commission_ID")]
        public long? AccountDueCommissionId { get; set; }

        [Column("Account_Commission_ID")]
        public long? AccountCommissionId { get; set; }

        [Column("Account_Mandate_Name")]
        [StringLength(255)]
        public string? AccountMandateName { get; set; }

        [Column("Account_Mandate_Code")]
        [StringLength(50)]
        public string? AccountMandateCode { get; set; }

        [Column("Account_Mandate_Name2")]
        [StringLength(255)]
        public string? AccountMandateName2 { get; set; }

        [Column("Account_Due_Mandate_Code")]
        [StringLength(50)]
        public string? AccountDueMandateCode { get; set; }

        [Column("Account_Due_Mandate_Name")]
        [StringLength(255)]
        public string? AccountDueMandateName { get; set; }

        [Column("Account_Due_Mandate_Name2")]
        [StringLength(255)]
        public string? AccountDueMandateName2 { get; set; }

        [Column("Account_Commission_Name")]
        [StringLength(255)]
        public string? AccountCommissionName { get; set; }

        [Column("Account_Commission_Name2")]
        [StringLength(255)]
        public string? AccountCommissionName2 { get; set; }

        [Column("Account_Commission_Code")]
        [StringLength(50)]
        public string? AccountCommissionCode { get; set; }

        [Column("Account_Due_Commission_Name")]
        [StringLength(255)]
        public string? AccountDueCommissionName { get; set; }

        [Column("Account_Due_Commission_Name2")]
        [StringLength(255)]
        public string? AccountDueCommissionName2 { get; set; }

        [Column("Account_Due_Commission_Code")]
        [StringLength(50)]
        public string? AccountDueCommissionCode { get; set; }

        [Column("Account_MedicalInsurance_ID")]
        public long? AccountMedicalInsuranceId { get; set; }

        [Column("Account_MedicalInsurance_Code")]
        [StringLength(50)]
        public string? AccountMedicalInsuranceCode { get; set; }

        [Column("Account_MedicalInsurance_Name")]
        [StringLength(255)]
        public string? AccountMedicalInsuranceName { get; set; }

        [Column("Account_MedicalInsurance_Name2")]
        [StringLength(255)]
        public string? AccountMedicalInsuranceName2 { get; set; }

        [Column("Account_PrepaidExpenses_ID")]
        public long? AccountPrepaidExpensesId { get; set; }

        [Column("Account_PrepaidExpenses_Code")]
        [StringLength(50)]
        public string? AccountPrepaidExpensesCode { get; set; }

        [Column("Account_PrepaidExpenses_Name")]
        [StringLength(255)]
        public string? AccountPrepaidExpensesName { get; set; }

        [Column("Account_PrepaidExpenses_Name2")]
        [StringLength(255)]
        public string? AccountPrepaidExpensesName2 { get; set; }
    }
}

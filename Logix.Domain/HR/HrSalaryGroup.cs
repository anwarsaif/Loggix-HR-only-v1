using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Table("HR_Salary_Group")]
    public partial class HrSalaryGroup : TraceEntity
    {
        [Key]
        [Column("ID")]
        public long Id { get; set; }

        [StringLength(250)]
        public string? Name { get; set; }

        [Column("Facility_ID")]
        public long? FacilityId { get; set; }

        [Column("Account_Due_Salary_ID")]
        public long? AccountDueSalaryId { get; set; }

        [Column("Account_Salary_ID")]
        public long? AccountSalaryId { get; set; }

        [Column("Account_Allowances_ID")]
        public long? AccountAllowancesId { get; set; }

        [Column("Account_OverTime_ID")]
        public long? AccountOverTimeId { get; set; }

        [Column("Account_Deduction_ID")]
        public long? AccountDeductionId { get; set; }

        [Column("Account_Loan_ID")]
        public long? AccountLoanId { get; set; }

        [Column("Account_Ohad_ID")]
        public long? AccountOhadId { get; set; }

        [Column("Account_Tickets_ID")]
        public long? AccountTicketsId { get; set; }

        [Column("Account_Vacation_salary_ID")]
        public long? AccountVacationSalaryId { get; set; }

        [Column("Account_End_Service_ID")]
        public long? AccountEndServiceId { get; set; }

        [Column("Account_Due_Tickets_ID")]
        public long? AccountDueTicketsId { get; set; }

        [Column("Account_Due_End_Service_ID")]
        public long? AccountDueEndServiceId { get; set; }

        [Column("Account_Due_Vacation_ID")]
        public long? AccountDueVacationId { get; set; }

        [Column("Account_GOSI_ID")]
        public long? AccountGosiId { get; set; }

        [Column("Account_Due_GOSI_ID")]
        public long? AccountDueGosiId { get; set; }

        [Column("Account_Mandate_ID")]
        public long? AccountMandateId { get; set; }

        [Column("Account_Due_Mandate_ID")]
        public long? AccountDueMandateId { get; set; }

        [Column("Account_Commission_ID")]
        public long? AccountCommissionId { get; set; }

        [Column("Account_Due_Commission_ID")]
        public long? AccountDueCommissionId { get; set; }

        [Column("Account_MedicalInsurance_ID")]
        public long? AccountMedicalInsuranceId { get; set; }

        [Column("Account_PrepaidExpenses_ID")]
        public long? AccountPrepaidExpensesId { get; set; }
    }
}

using Logix.Domain.HR;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logix.Application.DTOs.HR
{
    public class HrSalaryGroupFilterDto
    {



        [StringLength(250)]
        public string? Name { get; set; }


    }

    public class HrSalaryGroupDto
    {
        [Key]
        [Column("ID")]
        public long? Id { get; set; }

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

        public long? CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }

        public long? ModifiedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }

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
        ////////
        public string? AccountMedicalInsuranceCode { get; set; }
        public string? AccountPrepaidExpensesCode { get; set; }
        public string? AccountSalaryCode { get; set; }
        public string? AccountAllowancesCode { get; set; }
        public string? AccountOverTimeCode { get; set; }
        public string? AccountDeductionCode { get; set; }
        public string? AccountDueSalaryCode { get; set; }
        public string? AccountLoanCode { get; set; }
        public string? AccountOhadCode { get; set; }
        public string? AccountTicketsCode { get; set; }
        public string? AccountVacationSalaryCode { get; set; }
        public string? AccountEndServiceCode { get; set; }
        public string? AccountDueTicketsCode { get; set; }
        public string? AccountDueEndServiceCode { get; set; }
        public string? AccountDueVacationCode { get; set; }
        public string? AccountGosiCode { get; set; }
        public string? AccountDueGosiCode { get; set; }
        public string? AccountMandateCode { get; set; }
        public string? AccountDueMandateCode { get; set; }
        public string? AccountCommissionCode { get; set; }
        public string? AccountDueCommissionCode { get; set; }
    }

    //public class HrSalaryGroupEditDto

    //{
    //    public long Id { get; set; }

    //    public string? Name { get; set; }

    //    public long? FacilityId { get; set; }

    //    public long? AccountDueSalaryId { get; set; }

    //    public long? AccountSalaryId { get; set; }

    //    public long? AccountAllowancesId { get; set; }

    //    public long? AccountOverTimeId { get; set; }

    //    public long? AccountDeductionId { get; set; }

    //    public long? AccountLoanId { get; set; }

    //    public long? AccountOhadId { get; set; }

    //    [CustomDisplay("PettyCashAccount", "Hr")]
    //    public long? AccountTicketsId { get; set; }

    //    [CustomDisplay("VacationExpenseAccount", "Hr")]
    //    public long? AccountVacationSalaryId { get; set; }

    //    [CustomDisplay("EndOfServiceExpenseAccount", "Hr")]
    //    public long? AccountEndServiceId { get; set; }

    //    [CustomDisplay("TicketsProvisionAccount", "Hr")]
    //    public long? AccountDueTicketsId { get; set; }

    //    [CustomDisplay("EndOfServiceProvisionAccount", "Hr")]
    //    public long? AccountDueEndServiceId { get; set; }

    //    [CustomDisplay("VacationProvisionAccount", "Hr")]
    //    public long? AccountDueVacationId { get; set; }

    //    [CustomDisplay("GOSIAccount", "Hr")]
    //    public long? AccountGosiId { get; set; }

    //    [CustomDisplay("GOSIDueAccount", "Hr")]


    //    public long? AccountDueGosiId { get; set; }

    //    [CustomDisplay("MandateAccount", "Hr")]

    //    public long? AccountMandateId { get; set; }

    //    [CustomDisplay("MandateDueAccount", "Hr")]

    //    public long? AccountDueMandateId { get; set; }

    //    [CustomDisplay("CommissionAccount", "Hr")]

    //    public long? AccountCommissionId { get; set; }

    //    [CustomDisplay("CommissionDueAccount", "Hr")]

    //    public long? AccountDueCommissionId { get; set; }

    //    public bool IsDeleted { get; set; }
    //    ///////
    //    public string? AccountSalaryCode { get; set; }
    //    public string? AccountAllowancesCode { get; set; }
    //    public string? AccountOverTimeCode { get; set; }
    //    public string? AccountDeductionCode { get; set; }
    //    public string? AccountDueSalaryCode { get; set; }
    //    public string? AccountLoanCode { get; set; }
    //    public string? AccountOhadCode { get; set; }
    //    public string? AccountTicketsCode { get; set; }
    //    public string? AccountVacationSalaryCode { get; set; }
    //    public string? AccountEndServiceCode { get; set; }
    //    public string? AccountDueTicketsCode { get; set; }
    //    public string? AccountDueEndServiceCode { get; set; }
    //    public string? AccountDueVacationCode { get; set; }
    //    public string? AccountGosiCode { get; set; }
    //    public string? AccountDueGosiCode { get; set; }
    //    public string? AccountMandateCode { get; set; }
    //    public string? AccountDueMandateCode { get; set; }
    //    public string? AccountCommissionCode { get; set; }
    //    public string? AccountDueCommissionCode { get; set; }
    //    public List<HrSalaryGroupDeductionVw>? Deductions { get; set; }
    //    public List<HrSalaryGroupAllowanceVw>? Allowances { get; set; }
    //    //////////
    //}

    public class HrSalaryGroupEditDto
    {
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

        public long? CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }

        public long? ModifiedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }

        public bool? IsDeleted { get; set; }

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
        ///////
        public string? AccountMedicalInsuranceCode { get; set; }
        public string? AccountPrepaidExpensesCode { get; set; }
        public string? AccountSalaryCode { get; set; }
        public string? AccountAllowancesCode { get; set; }
        public string? AccountOverTimeCode { get; set; }
        public string? AccountDeductionCode { get; set; }
        public string? AccountDueSalaryCode { get; set; }
        public string? AccountLoanCode { get; set; }
        public string? AccountOhadCode { get; set; }
        public string? AccountTicketsCode { get; set; }
        public string? AccountVacationSalaryCode { get; set; }
        public string? AccountEndServiceCode { get; set; }
        public string? AccountDueTicketsCode { get; set; }
        public string? AccountDueEndServiceCode { get; set; }
        public string? AccountDueVacationCode { get; set; }
        public string? AccountGosiCode { get; set; }
        public string? AccountDueGosiCode { get; set; }
        public string? AccountMandateCode { get; set; }
        public string? AccountDueMandateCode { get; set; }
        public string? AccountCommissionCode { get; set; }
        public string? AccountDueCommissionCode { get; set; }
        public List<HrSalaryGroupDeductionVw>? Deductions { get; set; }
        public List<HrSalaryGroupAllowanceVw>? Allowances { get; set; }
        //////////
    }
}

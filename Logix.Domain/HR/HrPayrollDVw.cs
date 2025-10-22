using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Logix.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{

    [Keyless]
    public class HrPayrollDVw
    {
        [Column("MSD_ID")]
        public long MsdId { get; set; }

        [Column("MS_ID")]
        public long MsId { get; set; }

        [Column("Job_ID")]
        [StringLength(10)]
        [Unicode(false)]
        public string? JobId { get; set; }

        [Column("Emp_ID")]
        public long? EmpId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Salary { get; set; }

        [Column("Emp_Account_No")]
        [StringLength(50)]
        public string? EmpAccountNo { get; set; }

        public int? BankId { get; set; }

        public int? Attendance { get; set; }

        [Column("allowance", TypeName = "decimal(18, 2)")]
        public decimal? Allowance { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Deduction { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Absence { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Delay { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Loan { get; set; }

        [Column("Transaction_Installment", TypeName = "decimal(18, 2)")]
        public decimal? TransactionInstallment { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Net { get; set; }

        [Column(TypeName = "ntext")]
        public string? Note { get; set; }

        public long? CreatedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }

        public long? ModifiedBy { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        [Column("Emp_name")]
        [StringLength(250)]
        public string? EmpName { get; set; }

        [Column("Bank_Name")]
        [StringLength(250)]
        public string? BankName { get; set; }

        [Column("Bank_ID")]
        public int? BankId1 { get; set; }

        [Column("Account_No")]
        [StringLength(50)]
        public string? AccountNo { get; set; }

        [Column("MS_Month")]
        [StringLength(2)]
        public string? MsMonth { get; set; }

        [Column("MS_MothTxt")]
        [StringLength(500)]
        public string? MsMothTxt { get; set; }

        public int? FinancelYear { get; set; }

        public bool Expr1 { get; set; }

        [Column("Emp_Code")]
        [StringLength(50)]
        public string EmpCode { get; set; } = null!;

        [Column("ID_No")]
        [StringLength(50)]
        public string? IdNo { get; set; }

        [Column("Bank_Code")]
        public string? BankCode { get; set; }

        [Column("Account_ID")]
        public long? AccountId { get; set; }

        [Column("Sponsors_ID")]
        public int? SponsorsId { get; set; }

        [Column("Nationality_Name")]
        [StringLength(250)]
        public string? NationalityName { get; set; }

        [Column("Cat_name")]
        [StringLength(250)]
        public string? CatName { get; set; }

        [Column("IBAN")]
        [StringLength(50)]
        public string? Iban { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Commission { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? OverTime { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Mandate { get; set; }

        [Column("H_OverTime", TypeName = "decimal(18, 2)")]
        public decimal? HOverTime { get; set; }

        [Column("MS_Date")]
        [StringLength(10)]
        public string? MsDate { get; set; }

        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }

        [Column("Payroll_Type_ID")]
        public int? PayrollTypeId { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? Penalties { get; set; }

        public int? State { get; set; }

        [Column("Type_Name")]
        [StringLength(50)]
        public string? TypeName { get; set; }

        [Column("Month_Name")]
        [StringLength(50)]
        public string? MonthName { get; set; }

        [Column("Nationality_ID")]
        public int? NationalityId { get; set; }

        [Column("Basic_Salary", TypeName = "decimal(18, 2)")]
        public decimal? BasicSalary { get; set; }

        [Column("Payment_Type_ID")]
        public int? PaymentTypeId { get; set; }

        [Column("Wages_Protection")]
        public int? WagesProtection { get; set; }

        [Column("Wages_Protection_Name")]
        [StringLength(250)]
        public string? WagesProtectionName { get; set; }

        [Column("Start_Date")]
        [StringLength(10)]
        public string? StartDate { get; set; }

        [Column("End_Date")]
        [StringLength(10)]
        public string? EndDate { get; set; }

        [Column("Gosi_Rate_Facility", TypeName = "decimal(18, 2)")]
        public decimal? GosiRateFacility { get; set; }

        [Column("MS_Title")]
        [StringLength(4000)]
        public string? MsTitle { get; set; }

        [Column("Dep_Name2")]
        [StringLength(200)]
        public string? DepName2 { get; set; }

        [Column("MS_Code")]
        public long? MsCode { get; set; }

        [Column("Dept_ID")]
        public long? DeptId { get; set; }

        [Column("Facility_ID")]
        public long? FacilityId { get; set; }

        public long? Location { get; set; }

        [Column("Branch_ID")]
        public long? BranchId { get; set; }

        [Column("Dep_Name")]
        [StringLength(200)]
        public string? DepName { get; set; }

        [Column("Location_Name")]
        [StringLength(200)]
        public string? LocationName { get; set; }

        [Column("Location_CC_ID")]
        public long? LocationCcId { get; set; }

        [Column("Dept_CC_ID")]
        public long? DeptCcId { get; set; }

        [Column("Salary_Group_ID")]
        public long? SalaryGroupId { get; set; }

        [Column("CC_ID")]
        public long? CcId { get; set; }

        [Column("Cnt_Absence")]
        public int? CntAbsence { get; set; }

        [Column("Payment_Type_Name")]
        [StringLength(250)]
        public string? PaymentTypeName { get; set; }

        [Column("Payment_Type_Name2")]
        [StringLength(250)]
        public string? PaymentTypeName2 { get; set; }

        [Column("Branch_CC_Code")]
        [StringLength(50)]
        public string? BranchCcCode { get; set; }

        [Column("Branch_CC_Name2")]
        [StringLength(150)]
        public string? BranchCcName2 { get; set; }

        [Column("Branch_CC_Name")]
        [StringLength(150)]
        public string? BranchCcName { get; set; }

        [Column("Branch_CC_ID")]
        public int? BranchCcId { get; set; }

        [Column("Emp_Mobile")]
        [StringLength(20)]
        public string? EmpMobile { get; set; }

        [Column("allowance_Orignal", TypeName = "decimal(18, 2)")]
        public decimal? AllowanceOrignal { get; set; }

        [Column("Deduction_Orignal", TypeName = "decimal(18, 2)")]
        public decimal? DeductionOrignal { get; set; }

        [Column("Payroll_IBAN")]
        [StringLength(50)]
        public string? PayrollIban { get; set; }

        [Column("Location_Name2")]
        [StringLength(200)]
        public string? LocationName2 { get; set; }

        [Column("BRA_NAME")]
        public string? BraName { get; set; }

        [Column("BRA_NAME2")]
        public string? BraName2 { get; set; }

        [Column("ID")]
        public int? Id { get; set; }

        [Column("Status_Name")]
        [StringLength(50)]
        public string? StatusName { get; set; }

        [Column("Branch_Code")]
        [StringLength(50)]
        public string? BranchCode { get; set; }

        [Column("Sponser_Name")]
        [StringLength(250)]
        public string? SponserName { get; set; }

        [Column("Sponser_Name2")]
        [StringLength(250)]
        public string? SponserName2 { get; set; }

        [Column("Emp_Code2")]
        [StringLength(50)]
        public string? EmpCode2 { get; set; }

        [Column("DOAppointment")]
        [StringLength(12)]
        public string? Doappointment { get; set; }

        [Column("Contract_expiry_Date")]
        [StringLength(10)]
        public string? ContractExpiryDate { get; set; }

        [Column("Contract_Data")]
        [StringLength(10)]
        public string? ContractData { get; set; }

        [Column("H_Delay", TypeName = "decimal(18, 2)")]
        public decimal? HDelay { get; set; }

        [Column("CostCenter_Code")]
        [StringLength(50)]
        public string? CostCenterCode { get; set; }

        [Column("Job_Catagories_ID")]
        public int? JobCatagoriesId { get; set; }

        [Column("CC_ID2")]
        public long? CcId2 { get; set; }

        [Column("CC_ID3")]
        public long? CcId3 { get; set; }

        [Column("CC_ID4")]
        public long? CcId4 { get; set; }

        [Column("CC_ID5")]
        public long? CcId5 { get; set; }

        [Column("CC_Rate", TypeName = "decimal(18, 2)")]
        public decimal? CcRate { get; set; }

        [Column("CC_Rate2", TypeName = "decimal(18, 2)")]
        public decimal? CcRate2 { get; set; }

        [Column("CC_Rate3", TypeName = "decimal(18, 2)")]
        public decimal? CcRate3 { get; set; }

        [Column("CC_Rate4", TypeName = "decimal(18, 2)")]
        public decimal? CcRate4 { get; set; }

        [Column("CC_Rate5", TypeName = "decimal(18, 2)")]
        public decimal? CcRate5 { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal? IncomeTax { get; set; }

        public string? AmountWrite { get; set; }

        [Column("Dept_CC_ID2")]
        public long? DeptCcId2 { get; set; }

        [Column("Dept_CC_ID3")]
        public long? DeptCcId3 { get; set; }

        [Column("Dept_CC_ID4")]
        public long? DeptCcId4 { get; set; }

        [Column("Dept_CC_ID5")]
        public long? DeptCcId5 { get; set; }

        [Column("Cat_name2")]
        [StringLength(250)]
        public string? CatName2 { get; set; }

        [Column("Wages_Protection_Name2")]
        [StringLength(250)]
        public string? WagesProtectionName2 { get; set; }

        [Column("Nationality_Name2")]
        [StringLength(250)]
        public string? NationalityName2 { get; set; }

        [Column("Emp_Email")]
        [StringLength(50)]
        public string? EmpEmail { get; set; }

        [Column("IsSendSMS")]
        public bool? IsSendSms { get; set; }

        [Column("App_ID")]
        public long? AppId { get; set; }
    }

}

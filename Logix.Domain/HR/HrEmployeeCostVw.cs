using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Logix.Domain.HR
{
    [Keyless]
    public partial class HrEmployeeCostVw
    {
    [Column("Emp_name")]
    [StringLength(250)]
    public string? EmpName { get; set; }

    [Column("ID")]
    public long Id { get; set; }

    [Column("Emp_ID")]
    public long EmpId { get; set; }

    [Column("Cost_Type_ID")]
    public int? CostTypeId { get; set; }

    [Column("Cost_Rate")]
    public int? CostRate { get; set; }

    [Column("Trans_Date", TypeName = "datetime")]
    public DateTime? TransDate { get; set; }

    public string? Description { get; set; }

    [Column("Start_Date")]
    [StringLength(10)]
    public string? StartDate { get; set; }

    [Column("End_Date")]
    [StringLength(10)]
    public string? EndDate { get; set; }

    public bool? Active { get; set; }

    public bool? IsDeleted { get; set; }

    public long? CreatedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? CreatedOn { get; set; }

    public long? ModifiedBy { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ModifiedOn { get; set; }

    [Column("Cost_Value", TypeName = "decimal(18, 3)")]
    public decimal? CostValue { get; set; }

    [Column("Type_Name")]
    [StringLength(500)]
    public string? TypeName { get; set; }

    [Column("Nationality_ID")]
    public int? NationalityId { get; set; }

    [Column("ISDEL")]
    public bool? Isdel { get; set; }

    [Column("USER_ID")]
    public long? UserId { get; set; }

    [Column("BRANCH_ID")]
    public int? BranchId { get; set; }

    [Column("Job_Type")]
    public int? JobType { get; set; }

    [Column("Job_Catagories_ID")]
    public int? JobCatagoriesId { get; set; }

    [Column("Status_ID")]
    public int? StatusId { get; set; }

    [Column("Job_Description")]
    public string? JobDescription { get; set; }

    [Column("Marital_Status")]
    public int? MaritalStatus { get; set; }

    public int? Gender { get; set; }

    [Column("Stop_salary")]
    public bool? StopSalary { get; set; }

    [Column("Stop_Date_Salary")]
    [StringLength(12)]
    public string? StopDateSalary { get; set; }

    [Column("Stop_Salary_Code")]
    public int? StopSalaryCode { get; set; }

    [Column("POBox")]
    [StringLength(20)]
    public string? Pobox { get; set; }

    [Column("Home_Phone")]
    [StringLength(20)]
    public string? HomePhone { get; set; }

    [Column("Postal_Code")]
    [StringLength(20)]
    public string? PostalCode { get; set; }

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

    [Column("Emp_Photo")]
    [StringLength(500)]
    public string? EmpPhoto { get; set; }

    [Column("Contract_Type_ID")]
    public int? ContractTypeId { get; set; }

    [Column("DOAppointment")]
    [StringLength(12)]
    public string? Doappointment { get; set; }

    public string? Note { get; set; }

    [Column("ID_Issuer")]
    [StringLength(250)]
    public string? IdIssuer { get; set; }

    [Column("ID_Issuer_Date")]
    [StringLength(10)]
    [Unicode(false)]
    public string? IdIssuerDate { get; set; }

    [Column("ID_Expire_Date")]
    [StringLength(10)]
    [Unicode(false)]
    public string? IdExpireDate { get; set; }

    [StringLength(10)]
    [Unicode(false)]
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

    [Column("Qualification_ID")]
    public int? QualificationId { get; set; }

    [Column("Specialization_ID")]
    public int? SpecializationId { get; set; }

    [Column("Direct_Deposit")]
    public bool? DirectDeposit { get; set; }

    public long? Expr1 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Expr2 { get; set; }

    public long? Expr3 { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? Expr4 { get; set; }

    public bool? Expr5 { get; set; }

    [Column("Account_No")]
    [StringLength(50)]
    public string? AccountNo { get; set; }

    [Column("Bank_ID")]
    public int? BankId { get; set; }

    [Column("IBAN")]
    [StringLength(50)]
    public string? Iban { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal? Salary { get; set; }

    [Column("ID_No")]
    [StringLength(50)]
    public string? IdNo { get; set; }

    [Column("CC_ID")]
    public long? CcId { get; set; }

    [Column("Daily_Working_hours", TypeName = "decimal(18, 2)")]
    public decimal? DailyWorkingHours { get; set; }

    [Column("Bank_Name")]
    [StringLength(250)]
    public string? BankName { get; set; }

    [Column("BRA_NAME")]
    public string? BraName { get; set; }

    [Column("Catagories_ID")]
    public int? CatagoriesId { get; set; }

    [Column("Dept_ID")]
    public int? DeptId { get; set; }

    [Column("Status_name")]
    [StringLength(250)]
    public string? StatusName { get; set; }

    [Column("Exclude_Attend")]
    public bool? ExcludeAttend { get; set; }

    [Column("Vacation_Days_Year")]
    public int? VacationDaysYear { get; set; }

    [Column("Emp_name2")]
    [StringLength(250)]
    public string? EmpName2 { get; set; }

    [Column("Work_No")]
    [StringLength(50)]
    public string? WorkNo { get; set; }

    [Column("Work_Date")]
    [StringLength(10)]
    public string? WorkDate { get; set; }

    [Column("Work_ExpDate")]
    [StringLength(50)]
    public string? WorkExpDate { get; set; }

    [Column("Work_Place")]
    [StringLength(50)]
    public string? WorkPlace { get; set; }

    [Column("Visa_No")]
    [StringLength(50)]
    public string? VisaNo { get; set; }

    [Column("ID_Type")]
    public int? IdType { get; set; }

    [Column("Contarct_Date")]
    [StringLength(10)]
    public string? ContarctDate { get; set; }

    [Column("Cheque_Cash")]
    public int? ChequeCash { get; set; }

    [Column("Entry_Port")]
    [StringLength(50)]
    public string? EntryPort { get; set; }

    [Column("Entry_Date")]
    [StringLength(10)]
    public string? EntryDate { get; set; }

    [Column("Religion_ID")]
    public int? ReligionId { get; set; }

    [Column("Entry_NO")]
    [StringLength(50)]
    public string? EntryNo { get; set; }

    [Column("Account_ID")]
    public long? AccountId { get; set; }

    [Column("Pass_Issuer")]
    [StringLength(50)]
    public string? PassIssuer { get; set; }

    [Column("Account_Code")]
    [StringLength(50)]
    public string? AccountCode { get; set; }

    [Column("DOAppointmentold")]
    [StringLength(50)]
    public string? Doappointmentold { get; set; }

    [Column("Nationality_Name")]
    [StringLength(250)]
    public string? NationalityName { get; set; }

    [Column("Insurance_Category")]
    public int? InsuranceCategory { get; set; }

    public int? Location { get; set; }

    [Column("Insurance_Date_Validity")]
    [StringLength(10)]
    public string? InsuranceDateValidity { get; set; }

    [Column("Insurance_Company")]
    public int? InsuranceCompany { get; set; }

    [Column("Dep_Name")]
    [StringLength(200)]
    public string? DepName { get; set; }

    [Column("Location_Name")]
    [StringLength(200)]
    public string? LocationName { get; set; }

    [Column("Contract_Data")]
    [StringLength(10)]
    public string? ContractData { get; set; }

    [Column("Contract_expiry_Date")]
    [StringLength(10)]
    public string? ContractExpiryDate { get; set; }

    [Column("Note_Contract")]
    public string? NoteContract { get; set; }

    [Column("Emp_Code2")]
    [StringLength(50)]
    public string? EmpCode2 { get; set; }

    [Column("Gosi_Date")]
    [StringLength(10)]
    public string? GosiDate { get; set; }

    [Column("Gosi_Salary", TypeName = "decimal(18, 2)")]
    public decimal? GosiSalary { get; set; }

    [Column("Occupation_ID")]
    [StringLength(50)]
    public string? OccupationId { get; set; }

    [Column("Sponsors_ID")]
    public int? SponsorsId { get; set; }

    [Column("Phone_Country")]
    [StringLength(50)]
    public string? PhoneCountry { get; set; }

    [Column("Address_Country")]
    [StringLength(250)]
    public string? AddressCountry { get; set; }

    [StringLength(250)]
    public string? Address { get; set; }

    [Column("Insurance_Card_No")]
    [StringLength(50)]
    public string? InsuranceCardNo { get; set; }

    [Column("Ticket_to")]
    [StringLength(250)]
    public string? TicketTo { get; set; }

    [Column("Ticket_Type")]
    public int? TicketType { get; set; }

    [Column("Ticket_No_Dependent")]
    [StringLength(50)]
    public string? TicketNoDependent { get; set; }

    [Column("Salary_Group_ID")]
    public long? SalaryGroupId { get; set; }

    [Column("Acc_Account_Name")]
    [StringLength(255)]
    public string? AccAccountName { get; set; }

    [Column("Acc_Account_Code")]
    [StringLength(50)]
    public string? AccAccountCode { get; set; }

    [Column("CostCenter_Code")]
    [StringLength(50)]
    public string? CostCenterCode { get; set; }

    [Column("CostCenter_Name")]
    [StringLength(150)]
    public string? CostCenterName { get; set; }

    [Column("Facility_ID")]
    public int? FacilityId { get; set; }

    [Column("Card_Expiration_Date")]
    [StringLength(10)]
    public string? CardExpirationDate { get; set; }

    [Column("Account_Due_Salary_ID")]
    public long? AccountDueSalaryId { get; set; }

    [Column("IS_Ticket")]
    public bool? IsTicket { get; set; }

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

    [Column("Gois_Subscription_Expiry_Date")]
    [StringLength(10)]
    public string? GoisSubscriptionExpiryDate { get; set; }

    [Column("Gosi_Bisc_Salary", TypeName = "decimal(18, 2)")]
    public decimal? GosiBiscSalary { get; set; }

    [Column("Gosi_House_Allowance", TypeName = "decimal(18, 2)")]
    public decimal? GosiHouseAllowance { get; set; }

    [Column("Gosi_Other_Allowances", TypeName = "decimal(18, 2)")]
    public decimal? GosiOtherAllowances { get; set; }

    [Column("Marital_Status_Name")]
    [StringLength(5)]
    public string MaritalStatusName { get; set; } = null!;

    [Column("Gosi_Allowance_Commission", TypeName = "decimal(18, 2)")]
    public decimal? GosiAllowanceCommission { get; set; }

    [Column("Place_Attendance")]
    public int? PlaceAttendance { get; set; }

    [Column("Attendance_Type")]
    public int? AttendanceType { get; set; }

    [Column("Value_Ticket", TypeName = "decimal(18, 2)")]
    public decimal? ValueTicket { get; set; }

    [Column("Ticket_Entitlement")]
    public int? TicketEntitlement { get; set; }

    [Column("Program_ID")]
    public int? ProgramId { get; set; }

    [Column("Dep_Parent_ID")]
    public long? DepParentId { get; set; }

    [Column("Dep_Parent_Parent_ID")]
    public long? DepParentParentId { get; set; }

    [Column("Manager_ID")]
    public long? ManagerId { get; set; }

    [Column("Others_Requirements")]
    public string? OthersRequirements { get; set; }

    [Column("Have_Bank_Loan")]
    public bool? HaveBankLoan { get; set; }

    [Column("Dep_Parent_Manger_ID")]
    public long? DepParentMangerId { get; set; }

    [Column("Manager_Code")]
    [StringLength(50)]
    public string? ManagerCode { get; set; }

    [Column("Manager_Name")]
    [StringLength(250)]
    public string? ManagerName { get; set; }

    [Column("Manager_Name2")]
    [StringLength(250)]
    public string? ManagerName2 { get; set; }

    [Column("Dep_Manger_ID")]
    public long? DepMangerId { get; set; }

    [Column("By_ID")]
    public long? ById { get; set; }

    [Column("By_Name")]
    [StringLength(50)]
    public string? ByName { get; set; }

    [Column("Is_Sub")]
    public bool? IsSub { get; set; }

    [Column("Parent_ID")]
    public long? ParentId { get; set; }

    [Column("Apply_Salary_ladder")]
    public bool? ApplySalaryLadder { get; set; }

    [Column("Level_ID")]
    public int? LevelId { get; set; }

    [Column("Degree_ID")]
    public int? DegreeId { get; set; }

    [Column("Payment_Type_ID")]
    public int? PaymentTypeId { get; set; }

    [Column("Wages_Protection")]
    public int? WagesProtection { get; set; }

    [Column("Manager2_ID")]
    public long? Manager2Id { get; set; }

    [Column("Manager2_Code")]
    [StringLength(50)]
    public string? Manager2Code { get; set; }

    [Column("Manager2_Name")]
    [StringLength(250)]
    public string? Manager2Name { get; set; }

    [Column("Manager3_ID")]
    public long? Manager3Id { get; set; }

    [Column("Manager3_Code")]
    [StringLength(50)]
    public string? Manager3Code { get; set; }

    [Column("Manager3_Name")]
    [StringLength(250)]
    public string? Manager3Name { get; set; }

    [Column("Manager3_Name2")]
    [StringLength(250)]
    public string? Manager3Name2 { get; set; }

    [Column("Wages_Protection_Name")]
    [StringLength(250)]
    public string? WagesProtectionName { get; set; }

    [Column("Trial_Expiry_Date")]
    [StringLength(10)]
    public string? TrialExpiryDate { get; set; }

    [Column("Trial_Status_ID")]
    public int? TrialStatusId { get; set; }

    [Column("Gosi_Rate_Facility", TypeName = "decimal(18, 2)")]
    public decimal? GosiRateFacility { get; set; }

    [Column("Gosi_Type")]
    public int? GosiType { get; set; }

    [Column("Vacation2_Days_Year", TypeName = "decimal(18, 2)")]
    public decimal? Vacation2DaysYear { get; set; }

    [Column("Location_CC_ID")]
    public long? LocationCcId { get; set; }

    [Column("Grade_Name")]
    [StringLength(50)]
    public string? GradeName { get; set; }

    [Column("Insurance_Category_Name")]
    [StringLength(250)]
    public string? InsuranceCategoryName { get; set; }

    [Column("Location_Name2")]
    [StringLength(200)]
    public string? LocationName2 { get; set; }

    [Column("Dep_Name2")]
    [StringLength(200)]
    public string? DepName2 { get; set; }

    [Column("Cat_name")]
    [StringLength(250)]
    public string? CatName { get; set; }

    [Column("Cat_name2")]
    [StringLength(250)]
    public string? CatName2 { get; set; }

    [Column("Nationality_Name2")]
    [StringLength(250)]
    public string? NationalityName2 { get; set; }

    [Column("Job_ID")]
    public long? JobId { get; set; }

    [Column("Job_No")]
    [StringLength(50)]
    public string? JobNo { get; set; }

    [Column("Location_Manger_ID")]
    public long? LocationMangerId { get; set; }

    [Column("Facility_Name")]
    [StringLength(500)]
    public string? FacilityName { get; set; }

    [Column("Level_Name")]
    [StringLength(250)]
    public string? LevelName { get; set; }

    [Column("Qualification_Name")]
    [StringLength(250)]
    public string? QualificationName { get; set; }

    [Column("Qualification_Name2")]
    [StringLength(250)]
    public string? QualificationName2 { get; set; }

    [Column("Emp_Code")]
    [StringLength(50)]
    public string EmpCode { get; set; } = null!;

    [Column("Emp_ID2")]
    public long EmpId2 { get; set; }

    [Column("Rate_Type")]
    public int? RateType { get; set; }

    [Column("Salary_Basic")]
    public bool? SalaryBasic { get; set; }

    public string? Allowance { get; set; }

    [Column("Calculation_Rate", TypeName = "decimal(18, 2)")]
    public decimal? CalculationRate { get; set; }

    [Column("Calculation_Value", TypeName = "decimal(18, 2)")]
    public decimal? CalculationValue { get; set; }

    public int? Expr6 { get; set; }

    [Column("Type_Nationality")]
    public int? TypeNationality { get; set; }

    [Column("Type_Calculation")]
    public int? TypeCalculation { get; set; }

    [Column("Type_NameEn")]
    [StringLength(500)]
    public string? TypeNameEn { get; set; }

    [Column("Bank_Name2")]
    [StringLength(250)]
    public string? BankName2 { get; set; }

    [Column("Marital_Status_Name2")]
    [StringLength(250)]
    public string? MaritalStatusName2 { get; set; }

    [StringLength(250)]
    public string? Expr7 { get; set; }

    [Column("Status_Name2")]
    [StringLength(250)]
    public string? StatusName2 { get; set; }

    [Column("Facility_Email")]
    [StringLength(50)]
    public string? FacilityEmail { get; set; }

    [Column("BRA_NAME2")]
    public string? BraName2 { get; set; }

    [Column("Facility_Phone")]
    [StringLength(50)]
    public string? FacilityPhone { get; set; }

    [Column("Facility_Address")]
    [StringLength(2000)]
    public string? FacilityAddress { get; set; }

    [Column("Facility_Name2")]
    [StringLength(500)]
    public string? FacilityName2 { get; set; }

    [Column("Reason_Status")]
    public string? ReasonStatus { get; set; }

    [Column("Company_CR")]
    [StringLength(50)]
    public string? CompanyCr { get; set; }

    [Column("No_Labour_Office_File")]
    [StringLength(50)]
    public string? NoLabourOfficeFile { get; set; }

    [Column("Type_Calculation_Name2")]
    [StringLength(250)]
    public string? TypeCalculationName2 { get; set; }

    [Column("Project_ID")]
    public long? ProjectId { get; set; }

    [Column("Project_Code")]
    public long? ProjectCode { get; set; }

    [Column("Project_Name")]
    [StringLength(2500)]
    public string? ProjectName { get; set; }
  }

}

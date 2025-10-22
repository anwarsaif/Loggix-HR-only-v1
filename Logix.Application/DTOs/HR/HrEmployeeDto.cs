using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Logix.Application.DTOs.HR
{

    public class HrEmployeeDto
    {


        [StringLength(50)]

        [Required]
        public string? EmpId { get; set; } = null!;

        [Required]
        [StringLength(250)]
        public string? EmpName { get; set; }

        public bool? Isdel { get; set; }

        public long? UserId { get; set; }

        [Range(1, long.MaxValue)]
        public int? BranchId { get; set; }

        public int? JobType { get; set; }

        [Range(1, long.MaxValue)]
        public int? JobCatagoriesId { get; set; }

        public int? StatusId { get; set; }
        [Column("Job_Description")]
        public string? JobDescription { get; set; }

        [Range(1, long.MaxValue)]
        [Column("Nationality_ID")]
        public int? NationalityId { get; set; }

        [Range(1, long.MaxValue)]
        [Column("Marital_Status")]
        public int? MaritalStatus { get; set; }

        [Range(1, long.MaxValue)]
        public int? Gender { get; set; }

        [Column("Stop_salary")]
        public bool? StopSalary { get; set; }

        [Column("Stop_Date_Salary")]
        [StringLength(12)]
        public string? StopDateSalary { get; set; }

        [Column("Stop_Salary_Code")]
        public int? StopSalaryCode { get; set; }

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
        [Range(1, long.MaxValue)]
        [MinLength(9)]
        [MaxLength(9)]
        public string? Mobile { get; set; }

        [StringLength(50)]
        [EmailAddress]
        public string? Email { get; set; }

        [Column("Emp_Photo")]
        [StringLength(500)]
        public string? EmpPhoto { get; set; }

        [Range(1, long.MaxValue)]
        [Column("Contract_Type_ID")]
        public int? ContractTypeId { get; set; }

        [Range(1, long.MaxValue)]
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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("Account_No")]
        [StringLength(50)]
        public string? AccountNo { get; set; }
        [Column("Bank_ID")]

        [Range(1, long.MaxValue)]
        public int? BankId { get; set; }

        [Required]
        [Column("IBAN")]
        [StringLength(50)]
        public string? Iban { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [Required]
        public decimal? Salary { get; set; }
        [Column("ID_No")]
        [StringLength(50)]
        [Required]
        public string? IdNo { get; set; }

        [Column("Daily_Working_hours", TypeName = "decimal(18, 2)")]
        [Range(1, long.MaxValue)]
        public decimal? DailyWorkingHours { get; set; }
        [Column("CC_ID")]
        public long? CcId { get; set; }

        [Column("Dept_ID")]
        [Range(1, long.MaxValue)]
        public int? DeptId { get; set; }
        [Column("Exclude_Attend")]
        public bool? ExcludeAttend { get; set; }

        [Range(1, long.MaxValue)]
        [Column("Vacation_Days_Year")]
        public int? VacationDaysYear { get; set; }
        [Column("ID")]
        public long Id { get; set; }
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

        [Column("Entry_NO")]
        [StringLength(50)]
        public string? EntryNo { get; set; }

        [Column("Religion_ID")]
        public int? ReligionId { get; set; }
        [Column("Pass_Issuer")]
        [StringLength(50)]
        public string? PassIssuer { get; set; }
        [Column("Account_ID")]
        public long? AccountId { get; set; }
        [Column("Account_Code")]
        [StringLength(50)]
        public string? AccountCode { get; set; }

        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }
        [Column("Insurance_Date_Validity")]
        [StringLength(10)]
        public string? InsuranceDateValidity { get; set; }
        [Column("Insurance_Company")]
        public int? InsuranceCompany { get; set; }
        [Column("Insurance_Category")]
        public int? InsuranceCategory { get; set; }

        [Range(1, long.MaxValue)]
        public int? Location { get; set; }
        [Column("DOAppointmentold")]
        [StringLength(50)]
        public string? Doappointmentold { get; set; }

        [Required]
        [StringLength(10)]
        public string? ContractData { get; set; }


        [Range(1, long.MaxValue)]
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
        [Column("Gosi_No")]
        [StringLength(50)]
        public string? GosiNo { get; set; }
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


        [Range(1, long.MaxValue)]
        public long? SalaryGroupId { get; set; }

        [Column("Facility_ID")]
        [Range(1, long.MaxValue)]
        public int? FacilityId { get; set; }
        [Column("Card_Expiration_Date")]
        [StringLength(10)]
        public string? CardExpirationDate { get; set; }
        [Column("IS_Ticket")]
        public bool? IsTicket { get; set; }

        [Column("Gois_Subscription_Expiry_Date")]
        [StringLength(10)]
        public string? GoisSubscriptionExpiryDate { get; set; }

        [Column("Gosi_Bisc_Salary", TypeName = "decimal(18, 2)")]
        public decimal? GosiBiscSalary { get; set; }

        [Range(1, long.MaxValue)]
        [Column("Gosi_House_Allowance", TypeName = "decimal(18, 2)")]

        public decimal? GosiHouseAllowance { get; set; }

        [Column("Gosi_Allowance_Commission", TypeName = "decimal(18, 2)")]
        public decimal? GosiAllowanceCommission { get; set; }
        [Column("Gosi_Other_Allowances", TypeName = "decimal(18, 2)")]

        public decimal? GosiOtherAllowances { get; set; }
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

        [Column("Manager_ID")]
        public long? ManagerId { get; set; }
        public string? ManagerCode { get; set; }
        [Column("Others_Requirements")]
        public string? OthersRequirements { get; set; }
        [Column("Have_Bank_Loan")]
        public bool? HaveBankLoan { get; set; }
        [Column("By_ID")]
        public long? ById { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
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
        [Column("Manager3_ID")]
        public long? Manager3Id { get; set; }

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

        [Column("Job_ID")]
        public long? JobId { get; set; }
        [Column("Reason_Status")]
        public string? ReasonStatus { get; set; }
        [Column("Key_Check_Device")]
        [StringLength(50)]
        public string? KeyCheckDevice { get; set; }
        [Column("Check_Device")]
        public bool? CheckDevice { get; set; }
        [Column("Check_Device_Active")]
        public bool? CheckDeviceActive { get; set; }
        [StringLength(10)]
        public string? LastIncrementDate { get; set; }
        public int? AnnualIncreaseMethod { get; set; }
        [StringLength(10)]
        public string? LastPromotionDate { get; set; }
        [Column("Share_ContactInfo")]
        public bool? ShareContactInfo { get; set; }
        [Column("Payroll_Type")]
        public int? PayrollType { get; set; }
        [Column("Hour_Cost", TypeName = "decimal(18, 2)")]
        public decimal? HourCost { get; set; }

        //is EmpId automatic ?
        public bool AutoNumbering { get; set; }
        public HrEmployeeDto()
        {
            AutoNumbering = true;
        }

        /// <summary>
        /// ///// new Properties
        /// </summary>

        [Column("IsJoinedGOSIAfterJuly32024")]
        public bool? IsJoinedGosiafterJuly32024 { get; set; }

        [StringLength(50)]
        public string? Email2 { get; set; }

        [Column("First_Name")]
        [StringLength(50)]
        public string? FirstName { get; set; }

        [Column("Father_Name")]
        [StringLength(50)]
        public string? FatherName { get; set; }

        [Column("Grandfather_Name")]
        [StringLength(50)]
        public string? GrandfatherName { get; set; }

        [Column("Last_Name")]
        [StringLength(50)]
        public string? LastName { get; set; }

        [Column("First_Name2")]
        [StringLength(50)]
        public string? FirstName2 { get; set; }

        [Column("Father_Name2")]
        [StringLength(50)]
        public string? FatherName2 { get; set; }

        [Column("Grandfather_Name2")]
        [StringLength(50)]
        public string? GrandfatherName2 { get; set; }

        [Column("Last_Name2")]
        [StringLength(50)]
        public string? LastName2 { get; set; }

        [Column("Contract_Period_Type")]
        public int? ContractPeriodType { get; set; }
    }
    public class HrEmployeeEditDto
    {

        [StringLength(50)]
        public string? EmpId { get; set; } = null!;

        [StringLength(250)]
        public string? EmpName { get; set; }

        public bool? Isdel { get; set; }

        public long? UserId { get; set; }

        public int? BranchId { get; set; }

        public int? JobCatagoriesId { get; set; }

        public int? StatusId { get; set; }
        [Column("Job_Description")]
        public string? JobDescription { get; set; }
        [Column("Nationality_ID")]
        public int? NationalityId { get; set; }
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
        [Column("Emp_Photo")]
        [StringLength(500)]
        public string? EmpPhoto { get; set; }

        [Column("Contract_Type_ID")]
        public int? ContractTypeId { get; set; }

        [Required]

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
        [Column(TypeName = "datetime")]
        public DateTime? CreatedOn { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ModifiedOn { get; set; }
        public bool? IsDeleted { get; set; }
        [Column("Account_No")]
        [StringLength(50)]
        public string? AccountNo { get; set; }


        [Range(1, long.MaxValue)]

        public int? BankId { get; set; }
        [Column("IBAN")]
        [StringLength(50)]
        [Required]
        public string? Iban { get; set; }
        [Column(TypeName = "decimal(18, 2)")]


        public decimal? Salary { get; set; }

        [Column("ID_No")]
        [StringLength(50)]
        [Required]
        public string? IdNo { get; set; }
        [Column("Daily_Working_hours", TypeName = "decimal(18, 2)")]
        public decimal? DailyWorkingHours { get; set; }
        [Column("CC_ID")]
        public long? CcId { get; set; }
        [Column("Dept_ID")]
        public int? DeptId { get; set; }
        [Column("Exclude_Attend")]
        public bool? ExcludeAttend { get; set; }
        [Column("Vacation_Days_Year")]
        public int? VacationDaysYear { get; set; }
        [Column("ID")]
        public long Id { get; set; }
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
        [Column("Entry_NO")]
        [StringLength(50)]
        public string? EntryNo { get; set; }
        [Column("Religion_ID")]
        public int? ReligionId { get; set; }
        [Column("Pass_Issuer")]
        [StringLength(50)]
        public string? PassIssuer { get; set; }
        [Column("Account_ID")]
        public long? AccountId { get; set; }
        [Column("Account_Code")]
        [StringLength(50)]
        public string? AccountCode { get; set; }
        [Column("Emp_name2")]
        [StringLength(250)]
        public string? EmpName2 { get; set; }
        [Column("Insurance_Date_Validity")]
        [StringLength(10)]
        public string? InsuranceDateValidity { get; set; }
        [Column("Insurance_Company")]
        public int? InsuranceCompany { get; set; }
        [Column("Insurance_Category")]
        public int? InsuranceCategory { get; set; }


        public int? Location { get; set; }
        [Column("DOAppointmentold")]
        [StringLength(50)]
        public string? Doappointmentold { get; set; }
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
        [Column("Gosi_No")]
        [StringLength(50)]
        public string? GosiNo { get; set; }
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

        [Column("Facility_ID")]
        public int? FacilityId { get; set; }
        [Column("Card_Expiration_Date")]
        [StringLength(10)]
        public string? CardExpirationDate { get; set; }
        [Column("IS_Ticket")]
        public bool? IsTicket { get; set; }
        [Column("Gois_Subscription_Expiry_Date")]
        [StringLength(10)]
        public string? GoisSubscriptionExpiryDate { get; set; }
        [Column("Gosi_Bisc_Salary", TypeName = "decimal(18, 2)")]
        public decimal? GosiBiscSalary { get; set; }
        [Column("Gosi_House_Allowance", TypeName = "decimal(18, 2)")]
        public decimal? GosiHouseAllowance { get; set; }
        [Column("Gosi_Allowance_Commission", TypeName = "decimal(18, 2)")]
        public decimal? GosiAllowanceCommission { get; set; }
        [Column("Gosi_Other_Allowances", TypeName = "decimal(18, 2)")]
        public decimal? GosiOtherAllowances { get; set; }
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

        [Column("Manager_ID")]

        public long? ManagerId { get; set; }
        [Column("Others_Requirements")]
        public string? OthersRequirements { get; set; }
        [Column("Have_Bank_Loan")]
        public bool? HaveBankLoan { get; set; }
        [Column("By_ID")]
        public long? ById { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
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
        [Column("Manager3_ID")]
        public long? Manager3Id { get; set; }
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
        [Column("Job_ID")]
        public long? JobId { get; set; }
        [Column("Reason_Status")]
        public string? ReasonStatus { get; set; }
        [Column("Key_Check_Device")]
        [StringLength(50)]
        public string? KeyCheckDevice { get; set; }
        [Column("Check_Device")]
        public bool? CheckDevice { get; set; }
        [Column("Check_Device_Active")]
        public bool? CheckDeviceActive { get; set; }
        [StringLength(10)]
        public string? LastIncrementDate { get; set; }
        public int? AnnualIncreaseMethod { get; set; }
        [StringLength(10)]
        public string? LastPromotionDate { get; set; }
        [Column("Share_ContactInfo")]
        public bool? ShareContactInfo { get; set; }
        [Column("Payroll_Type")]
        public int? PayrollType { get; set; }
        [Column("Hour_Cost", TypeName = "decimal(18, 2)")]
        public decimal? HourCost { get; set; }
        /// <summary>
        /// ///// new Properties
        /// </summary>

        [Column("IsJoinedGOSIAfterJuly32024")]
        public bool? IsJoinedGosiafterJuly32024 { get; set; }

        [StringLength(50)]
        public string? Email2 { get; set; }

        [Column("First_Name")]
        [StringLength(50)]
        public string? FirstName { get; set; }

        [Column("Father_Name")]
        [StringLength(50)]
        public string? FatherName { get; set; }

        [Column("Grandfather_Name")]
        [StringLength(50)]
        public string? GrandfatherName { get; set; }

        [Column("Last_Name")]
        [StringLength(50)]
        public string? LastName { get; set; }

        [Column("First_Name2")]
        [StringLength(50)]
        public string? FirstName2 { get; set; }

        [Column("Father_Name2")]
        [StringLength(50)]
        public string? FatherName2 { get; set; }

        [Column("Grandfather_Name2")]
        [StringLength(50)]
        public string? GrandfatherName2 { get; set; }

        [Column("Last_Name2")]
        [StringLength(50)]
        public string? LastName2 { get; set; }

        [Column("Contract_Period_Type")]
        public int? ContractPeriodType { get; set; }
    }

    public class DeductionVM
    {
        // الاي دي في الجدول
        public long Id { get; set; } = 0;
        // رقم الحسم وياتي من الليست
        public int? TypeId { get; set; } = 0;
        public int? AdId { get; set; } = 0;

        public decimal? DeductionRate { get; set; }
        public decimal? DeductionAmount { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class AllowanceVM
    {        // الاي دي في الجدول

        public long Id { get; set; } = 0;
        // رقم البدل وياتي من الليست

        public int? TypeId { get; set; } = 0;
        public int? AdId { get; set; } = 0;

        public decimal? AllowanceRate { get; set; }
        public decimal? AllowanceAmount { get; set; }

        public bool IsDeleted { get; set; }
    }

    public class EmployeePouUpDto
    {
        public int? StatusId { get; set; }
        public int? DeptId { get; set; }
        public int? JobCatagoriesId { get; set; }
        public string? EmpName { get; set; }
        public string? EmpCode { get; set; }
        public string? IdNo { get; set; }

    }

    // الوظائف الإضافية
    public class EmployeeSubDto
    {
        public long? Id { get; set; }
        public string? EmpName { get; set; }
        public string? EmpName2 { get; set; }
        public string? Doappointment { get; set; }
        public int? Gender { get; set; }
        public int? MaritalStatus { get; set; }
        public int? NationalityId { get; set; }
        public int? JobCatagoriesId { get; set; }
        public int? BranchId { get; set; }
        public string EmpCode { get; set; } = null!;
        public string? JobID { get; set; }
        public int? Location { get; set; }
        public decimal? Salary { get; set; }
        public int? ProgramId { get; set; }
        public int? FacilityId { get; set; }
        public int? DeptId { get; set; }
        public long? ParentId { get; set; }
        public string? ContractExpiryDate { get; set; }
        public string? ContractDate { get; set; }
        public string? CostCenterCode { get; set; }
        public long? SalaryGroupId { get; set; }
        public string? Iban { get; set; }
        public string? AccountNo { get; set; }
        public int? BankId { get; set; }
        public long? ManagerId { get; set; }
        public string? ManagerCode { get; set; }
        public decimal? DailyWorkingHours { get; set; }
        public string? IdNo { get; set; }
        public List<DeductionVM>? deduction { get; set; }
        public List<AllowanceVM>? allowance { get; set; }
        public long? ShitID { get; set; }
        public long? AttTimeTableID { get; set; }
        public string? ParentCode { get; set; }
    }

    //  للبحث ونتائج البحث
    public class EmployeeSubFilterDto
    {
        public int? BranchId { get; set; }
        public string? EmpId { get; set; }
        public long Id { get; set; }
        public string? EmpName { get; set; }
        public string? EmpName2 { get; set; }
        public int? JobType { get; set; }
        public int? JobCatagoriesId { get; set; }
        public int? DeptId { get; set; }
        public int? DrpjobTypeId { get; set; }
        public int? StatusId { get; set; }
        public int? NationalityId { get; set; }
        public int? LocationId { get; set; }
        public string? PassportNo { get; set; }
        public string? IdNo { get; set; }
        public string? EntryNo { get; set; }
        public int? SponsorsId { get; set; }
        public int? FacilityId { get; set; }
        public string? BraName { get; set; }
        public string? DepName { get; set; }
        public string? LocName { get; set; }
        public string? CatName { get; set; }
        public string? EmpStatusName { get; set; }

        public string? ManagerId { get; set; }
        public string? Manager2Id { get; set; }
        public string? Manager3Id { get; set; }
        public string? ManagerName { get; set; }
        public string? Manager2Name { get; set; }
        public string? Manager3Name { get; set; }
        public string? CostCenterCode { get; set; }
        public string? CostCenterName { get; set; }
        public long? ParentId { get; set; }
    }
}
